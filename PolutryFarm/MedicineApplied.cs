using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALayer;
using VOLayer.cs;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace PolutryFarm
{
    public partial class MedicineApplied : Form
    {
        public MedicineAppVO oma1 = new MedicineAppVO();
        public DAL oma2 = new DAL();
        int pageSize = 25;
        int selectedRow;
        bool tempbool = true;
        bool tempboolAC = true;
        private int AttNam = 0;
        private string constring;
        public MedicineApplied()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            MedicineAppliedMain.Enabled = false;
            tmaboxid.Visible = false;
            tmapurdate.Visible = false;
            lmampdate.Visible = false;
            tmamedname.Visible = false;
            lmamedname.Visible = false;
            tmasdate.Text = DateTime.Today.ToString();
            tmaedate.Text = DateTime.Today.ToString();
          
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            btndel.Enabled = false;

            if (Main.Loc == 1)
            {
                label2.Text = "Attur";

            }
            else if (Main.Loc == 2)
            {
                label2.Text = "Namakkal";
            }
            Bindgrid(1, 1, 1);
            if (AttNam == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNam == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand cmdchicks = new SqlCommand("SELECT Cbatchno FROM Chicks", connection);
            SqlDataReader readerc = cmdchicks.ExecuteReader();
            AutoCompleteStringCollection MyCollectionc = new AutoCompleteStringCollection();
            while (readerc.Read())
            {
                MyCollectionc.Add(readerc.GetString(0));
            }
            tmaccbatchno.AutoCompleteCustomSource = MyCollectionc;
            connection.Close();
            connection.Open();
            SqlCommand cmdfeed = new SqlCommand("SELECT CONCAT(m.Mpurno,'','-','',m.Mbrand)  FROM  Medicine m", connection);
            SqlDataReader readerf = cmdfeed.ExecuteReader();
            AutoCompleteStringCollection MyCollectionf = new AutoCompleteStringCollection();
            while (readerf.Read())
            {
                MyCollectionf.Add(readerf.GetString(0));
            }
            tmapurno.AutoCompleteCustomSource = MyCollectionf;

            connection.Close();
        }
        private const int cGrip = 16;
        private const int cCaption = 32;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        public BindingList<MedicineAppVO> omav1 = new BindingList<MedicineAppVO>();
        public DAL omav2 = null;

        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            omav2 = new DAL();
            omav1 = omav2.ViewMA(pageSize, pageIndex, temp, tempAC, out int recordCount, AttNam);
            datagridma.DataSource = omav1;
            this.datagridma.AllowUserToAddRows = false;
            datagridma.Columns["MAid"].HeaderText = "Id";
            datagridma.Columns["MAid"].Visible = false;
            datagridma.Columns["MAsdate"].HeaderText = "Start Date";
            datagridma.Columns["MAedate"].HeaderText = "End Date";
            datagridma.Columns["MApurno"].HeaderText = "Medicine Purchase No";
            datagridma.Columns["MAbrand"].HeaderText = "Medicine Name";
            datagridma.Columns["MAcbatchno"].HeaderText = "Chicks Batch No";
            datagridma.Columns["MAnotes"].HeaderText = "Notes";
            PopulatePager(recordCount, pageIndex);
        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            BindingList<Page> pages = new BindingList<Page>();
            int startIndex, endIndex;
            int pagerSpan = 2;
            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            pnlPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(38 * count, 5);
                btnPage.Size = new System.Drawing.Size(50, 50);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                pnlPager.Controls.Add(btnPage);
                count++;
            }
        }
        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            if (tempbool == true)
            {
                if (tempboolAC == false)
                    this.Bindgrid(int.Parse(btnPager.Name), 1, 2);
                else if (tempboolAC == true)
                    this.Bindgrid(int.Parse(btnPager.Name), 1, 1);
            }
            else if (tempbool == false)
            {
                if (tempboolAC == false)
                    this.Bindgrid(int.Parse(btnPager.Name), 2, 2);
                else if (tempboolAC == true)
                    this.Bindgrid(int.Parse(btnPager.Name), 2, 1);
            }
        }
        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }

      
        private void btndel_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(datagridma.CurrentRow.Cells[0].Value.ToString());
            int i = omav2.DelMA(id, AttNam);
            if (i > 0)
            {
                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagridma.CurrentCell.RowIndex;
                datagridma.Rows.RemoveAt(selectedRow);          
                btndel.Enabled = false;
                Cleardata();
                if (bmaadd.Text == "Update")
                {
                    bmaadd.Text = "Add";
                }
            }
        }

        private void DateAC_Click(object sender, EventArgs e)
        {
            tempbool = true;
            if (DateAC.Text == "Date Asc")
            {

                Bindgrid(1, 1, 2);
                DateAC.Text = "Date Desc";
                tempboolAC = false;
            }
            else
            {
                Bindgrid(1, 1, 1);
                DateAC.Text = "Date Asc";
                tempboolAC = true;
            }

        }

        private void BatchAC_Click(object sender, EventArgs e)
        {
            tempbool = false;
            if (BatchAC.Text == "Batch Asc")
            {
                Bindgrid(1, 2, 2);
                BatchAC.Text = "Batch Desc";
                tempboolAC = false;
            }
            else
            {
                Bindgrid(1, 2, 1);
                BatchAC.Text = "Batch Asc";
                tempboolAC = true;
            }
        }
        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (WindowState.ToString() == "Normal")
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.IO.File.Exists("sysLL.txt"))
            {
                System.IO.File.Delete("sysLL.txt");
            }
            Application.Exit();

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label2.Text == "Attur")
            {

                Main.Loc = 1;

            }
            else if (label2.Text == "Namakkal")
            {
                Main.Loc = 2;

            }
            Main m = new Main();
            m.Show();
        }

        private void bmaadd_Click(object sender, EventArgs e)
        {
            if (tmapurno.Text == "" || tmaccbatchno.Text == "")
            {
                if (tmapurno.Text == "")
                {
                    MessageBox.Show("Medicine purchase no is mandatory");
                    tmapurno.Focus();

                }                 
                else if (tmaccbatchno.Text == "")
                {
                    MessageBox.Show("Chicks batch no is mandatory");
                    tmaccbatchno.Focus();
                }                  

            }
            else
            {


                oma1.MAid = tmaboxid.Text == "" ? 0 : Convert.ToInt32(tmaboxid.Text);
                oma1.MAsdate = Convert.ToDateTime(tmasdate.Text);
                oma1.MAedate = Convert.ToDateTime(tmaedate.Text);
                oma1.MApurno = tmapurno.Text.Substring(0, 2);
                oma1.MAcbatchno = tmaccbatchno.Text;
                oma1.MAnotes = tmanotes.Text;

                if (oma2.InupMA(oma1, Login.UserName, out DateTime MApurdate, out string MAbrand, AttNam) == 2)
                {
                    tmapurdate.Visible = true;
                    lmampdate.Visible = true;
                    tmamedname.Visible = true;
                    lmamedname.Visible = true;
                    tmapurdate.Text = Convert.ToDateTime(MApurdate).ToString("dd/MM/yyyy");
                    tmamedname.Text = MAbrand;
                    MessageBox.Show("Successfully Added!");                 
                    Cleardata();
                    Bindgrid(1, 1, 1);                   

                }
                else if (oma2.InupMA(oma1, Login.UserName, out DateTime MApurdate1, out string MAbrand1, AttNam) == 1)
                {

                    tmapurdate.Visible = true;
                    lmampdate.Visible = true;
                    tmamedname.Visible = true;
                    lmamedname.Visible = true;
                    tmapurdate.Text = Convert.ToDateTime(MApurdate1).ToString("dd/MM/yyyy");
                    tmamedname.Text = MAbrand1;
                    MessageBox.Show("Successfully Updated!");                   
                    Cleardata();
                    Bindgrid(1, 1, 1);
                    if (bmaadd.Text == "Update")
                    {
                        bmaadd.Text = "Add";
                    }


                }
            }
        }
        public void Cleardata()
        {
            tmasdate.Text = DateTime.Today.ToString();
            tmaedate.Text = DateTime.Today.ToString();
            tmapurno.Text = null;
            tmamedname.Text = null;
            tmaccbatchno.Text = null;
            tmanotes.Text = null;
            tmapurdate.Visible = false;
            lmampdate.Visible = false;
            tmamedname.Visible = false;
            lmamedname.Visible = false;
            tmaboxid.Text = null;


        }


        private void bmauptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (bmaadd.Text == "Update")
            {
                bmaadd.Text = "Add";
                
            }
        }
        private void tmapurno_Leave(object sender, EventArgs e)
        {
            if (tmapurno.Text != "" )
            {
                tmapurno.Text = tmapurno.Text.Substring(0, 2);
                if (AttNam == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNam == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "mdrop";
                comm.Parameters.AddWithValue("@Mpurno", tmapurno.Text);
                int fpurno = Convert.ToInt16(comm.ExecuteScalar());
                if (fpurno == 0)
                {
                    MessageBox.Show("Please select the valid feed purchase no from suggestion");
                    tmapurno.Text = null;
                    tmapurno.Focus();
                }
            }
        }

        private void tmaccbatchno_Leave(object sender, EventArgs e)
        {
            if (tmaccbatchno.Text != "")
            {

                if (AttNam == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNam == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "cdrop";
                comm.Parameters.AddWithValue("@Cbatchno", tmaccbatchno.Text);
                int cbatch = Convert.ToInt16(comm.ExecuteScalar());
                if (cbatch == 0)
                {
                    MessageBox.Show("Please select the valid chicks batch no from suggestion");
                    tmaccbatchno.Text = null;
                    tmaccbatchno.Focus();
                }
            }
        }

        private void datagridma_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = true;
            btndel.Focus();
        }

        private void datagridma_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
     tmaboxid.Text = (Convert.ToInt32(datagridma.CurrentRow.Cells[0].Value)).ToString();
            DateTime msDate = DateTime.Parse(datagridma.CurrentRow.Cells[1].Value.ToString());
         tmasdate.Value = DateTime.Parse(msDate.ToShortDateString());
            DateTime meDate = DateTime.Parse(datagridma.CurrentRow.Cells[2].Value.ToString());
         tmaedate.Value = DateTime.Parse(meDate.ToShortDateString());
          tmapurno.Text = this.datagridma.CurrentRow.Cells[3].Value.ToString();
        tmamedname.Text = this.datagridma.CurrentRow.Cells[4].Value.ToString();
       tmaccbatchno.Text = this.datagridma.CurrentRow.Cells[5].Value.ToString();
       tmanotes.Text = this.datagridma.CurrentRow.Cells[6].Value.ToString();
         bmaadd.Text = "Update";

        }
    }
}
