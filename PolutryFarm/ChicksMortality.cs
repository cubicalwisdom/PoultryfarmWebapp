using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALayer;
using VOLayer.cs;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace PolutryFarm
{
    public partial class ChicksMortality : Form
    {
        public ChicksMotVO ocm1 = new ChicksMotVO();
        public DAL ocm2 = new DAL();
        int pageSize = 25;
        int selectedRow;
        bool tempbool = true;
        bool tempboolAC = true;
        private int AttNam = 0;
        private string constring;
        public ChicksMortality()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            ChicksMortalityMain.Enabled = false;
            tcmboxid.Visible = false;
            tcmbdate.Visible = false;
            tcmbqty.Visible = false;
            tcmaid.Visible = false;
            lcmbdate.Visible = false;
            lcmbq.Visible = false;
            lcmaid.Visible = false;
            btndel.Enabled = false;
            tcmdate.Text = DateTime.Today.ToString();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
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
           SqlCommand cmd = new SqlCommand("SELECT  Cbatchno FROM Chicks", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                MyCollection.Add(reader.GetString(0));
            }
            tcmbatchno.AutoCompleteCustomSource = MyCollection;

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

      private void bcmadd_Click(object sender, EventArgs e)
        {

            if (tcmbatchno.Text == "")
            {
                MessageBox.Show("Batch no are mandatory");
                tcmbatchno.Focus();

            }
            else if (tcmqty.Text == "")
            {
                MessageBox.Show("Quantity is mandatory");
                tcmqty.Focus();

            }
            else
            {
                ocm1.CMid = tcmboxid.Text == "" ? 0 : Convert.ToInt32(tcmboxid.Text);
                ocm1.CMdate = Convert.ToDateTime(tcmdate.Text);
                ocm1.CMqty = Convert.ToInt32(tcmqty.Text);
                ocm1.CMbatchno = tcmbatchno.Text;
                ocm1.CMnotes = tcmnotes.Text;

                if (ocm2.InupCM(ocm1, Login.UserName, out DateTime CMbdate, out int CMBalqty, out int CMaid, AttNam) == 2)
                {
                    tcmbdate.Visible = true;
                    tcmbqty.Visible = true;
                    tcmaid.Visible = true;
                    lcmbdate.Visible = true;
                    tcmbdate.ReadOnly = true;
                    tcmbqty.ReadOnly = true;
                    tcmaid.ReadOnly = true;
                    lcmbdate.Visible = true;
                    lcmbq.Visible = true;
                    lcmaid.Visible = true;
                    tcmbdate.Text = Convert.ToDateTime(CMbdate).ToString("dd/MM/yyyy");
                    tcmbqty.Text = Convert.ToInt16(CMBalqty).ToString();
                    tcmaid.Text = Convert.ToInt16(CMaid).ToString();
                   MessageBox.Show("Successfully Added!");
                    Bindgrid(1, 1, 1);
                    Cleardata();
                }
                else if (ocm2.InupCM(ocm1, Login.UserName, out DateTime CMbdate1, out int CMBalqty1,
                             out int CMaid1, AttNam) == 1)
                {
                    tcmbdate.Visible = true;
                    tcmbqty.Visible = true;
                    tcmaid.Visible = true;
                    lcmbdate.Visible = true;
                    tcmbdate.ReadOnly = true;
                    tcmbqty.ReadOnly = true;
                    tcmaid.ReadOnly = true;
                    lcmbdate.Visible = true;
                    lcmbq.Visible = true;
                    lcmaid.Visible = true;
                    tcmbdate.Text = Convert.ToDateTime(CMbdate1).ToString("dd/MM/yyyy");
                    tcmbqty.Text = Convert.ToInt16(CMBalqty1).ToString();
                    tcmaid.Text = Convert.ToInt16(CMaid1).ToString();
                    MessageBox.Show("Successfully Updated!");
                    Bindgrid(1, 1, 1);
                    if (bcmadd.Text == "Update")
                    {
                        bcmadd.Text = "Add";
                    }
                    Cleardata();


                }
            }
        }
        public void Cleardata()
        {
            tcmdate.Text = DateTime.Today.ToString();
            tcmqty.Text = null;
            tcmbatchno.Text = null;
            tcmbatchno.Enabled = true;
            tcmnotes.Text = null;
            tcmbdate.Visible = false;
            tcmbqty.Visible = false;
            tcmaid.Visible = false;
            lcmbdate.Visible = false;
            lcmbq.Visible = false;
            lcmaid.Visible = false;
            tcmboxid.Text = null;

        }

        private void bcmuptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (bcmadd.Text == "Update")
            {
                bcmadd.Text = "Add";
            }
        }

        private void tcmqty_KeyPress(object sender, KeyPressEventArgs e)
        {

            char ch = e.KeyChar;
            if (ch == 46)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
        private void tcmbatchno_Leave(object sender, EventArgs e)
        {


            if (tcmbatchno.Text != ""  && tcmboxid.Text == "")
            {
                int qty = tcmqty.Text == "" ? 0 : Convert.ToInt32(tcmqty.Text);
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

                SqlCommand comm = new SqlCommand();
                connection.Open();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Cqty";
                comm.Parameters.AddWithValue("@Cbatchno", tcmbatchno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (preqty == -99)
                {
                    MessageBox.Show("Please select the valid batchno from suggestion");
                    tcmbatchno.Text = null;
                    tcmbatchno.Focus();
                }
                else if (qty > preqty)
                {
                    MessageBox.Show("Quantity should not greater than the actual chicks qty");
                    tcmqty.Text = null;
                    tcmqty.Focus();
                }
            }

            else if (tcmbatchno.Text != ""  && tcmboxid.Text != "")
            {
                int qty = tcmqty.Text == "" ? 0 : Convert.ToInt32(tcmqty.Text);

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

                SqlCommand comm = new SqlCommand();
                connection.Open();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Cqty1";
                comm.Parameters.AddWithValue("@Cbatchno", tcmbatchno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (qty > preqty)
                {
                    MessageBox.Show("Quantity should not greater than the actual chicks qty");
                    tcmqty.Text = null;
                    tcmqty.Focus();
                }
            }
        }

        private void tcmqty_Leave(object sender, EventArgs e)
        {

            if (tcmbatchno.Text != ""  && tcmboxid.Text == "")
            {
                int qty = tcmqty.Text == "" ? 0 : Convert.ToInt32(tcmqty.Text);
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

                SqlCommand comm = new SqlCommand();
                connection.Open();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Cqty";
                comm.Parameters.AddWithValue("@Cbatchno", tcmbatchno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (preqty == -99)
                {
                    MessageBox.Show("Please select the valid batchno from suggestion");
                    tcmbatchno.Text = null;
                    tcmbatchno.Focus();
                }
                else if (qty > preqty)
                {
                    MessageBox.Show("Quantity should not greater than the actual chicks qty");
                    tcmqty.Text = null;
                    tcmqty.Focus();
                }
            }



            else if (tcmbatchno.Text != ""  && tcmboxid.Text != "")
            {
                int qty = tcmqty.Text == "" ? 0 : Convert.ToInt32(tcmqty.Text);
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

                SqlCommand comm = new SqlCommand();
                connection.Open();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Cqty1";
                comm.Parameters.AddWithValue("@Cbatchno", tcmbatchno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (qty > preqty)
                {
                    MessageBox.Show("Quantity should not greater than the actual chicks qty");
                    tcmqty.Text = null;
                    tcmqty.Focus();
                }
            }
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

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public BindingList<ChicksMotVO> ocmv1 = new BindingList<ChicksMotVO>();
        public DAL ocmv2 = null;

        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            ocmv2 = new DAL();
            ocmv1 = ocmv2.ViewCM(pageSize, pageIndex, temp, tempAC, out int recordCount, AttNam);
            datagridcm.DataSource = ocmv1;
            this.datagridcm.AllowUserToAddRows = false;
            datagridcm.Columns["CMid"].HeaderText = "Id";
            datagridcm.Columns["CMid"].Visible = false;
            datagridcm.Columns["CMdate"].HeaderText = "Date";
            datagridcm.Columns["CMbatchno"].HeaderText = "Batch No";
            datagridcm.Columns["CMqty"].HeaderText = "QTY";
            datagridcm.Columns["CMBalqty"].HeaderText = "Balance QTY";
            datagridcm.Columns["CMaid"].HeaderText = "Age";
            datagridcm.Columns["CMnotes"].HeaderText = "Notes";
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

      


        private void btndel_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(datagridcm.CurrentRow.Cells[0].Value.ToString());

            int i = ocmv2.DelCM(id, AttNam);

            if (i > 0)
            {

                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagridcm.CurrentCell.RowIndex;
                datagridcm.Rows.RemoveAt(selectedRow);
                Cleardata();
                btndel.Enabled = false;
                if (bcmadd.Text == "Update")
                {
                    bcmadd.Text = "Add";
                }

            }

        }

        private void ChicksSold_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChicksSold cs = new ChicksSold();
            cs.Show();
        }

        private void Chicks_Click(object sender, EventArgs e)
        {
            this.Hide();

            Chicks c = new Chicks();
            c.Show();
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

        private void datagridcm_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
           tcmboxid.Text = (Convert.ToInt32(datagridcm.CurrentRow.Cells[0].Value)).ToString();
            DateTime CSDate = DateTime.Parse(datagridcm.CurrentRow.Cells[1].Value.ToString());
            tcmdate.Value = DateTime.Parse(CSDate.ToShortDateString());
            tcmbatchno.Text = this.datagridcm.CurrentRow.Cells[2].Value.ToString();
            tcmqty.Text = this.datagridcm.CurrentRow.Cells[3].Value.ToString();        
            tcmnotes.Text = this.datagridcm.CurrentRow.Cells[6].Value.ToString();        
            bcmadd.Text = "Update";
            tcmbatchno.Enabled = false;



        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.IO.File.Exists("sysLL.txt"))
            {
                System.IO.File.Delete("sysLL.txt");
            }
            Application.Exit();
        }

        private void datagridcm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = true;
            btndel.Focus();
        }
    }
}
