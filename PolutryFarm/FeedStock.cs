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
    public partial class FeedStock : Form
    {
        public FeedStoVO ofs1 = new FeedStoVO();
        public DAL ofs2 = new DAL();
        int pageSize = 25;
        int selectedRow;
        private int AttNam = 0;
        private string constring;
        bool tempbool = true;
        bool tempboolAC = true;
        public FeedStock()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            FeedStockMain.Enabled = false;
            tfsboxid.Visible = false;
            lfsbbc.Visible = false;
            tfsbalfbagcou.Visible = false;
            lfsfpdate.Visible = false;
            tfsfpurdate.Visible = false;
            tfssdate.Text = DateTime.Today.ToString();
            tfsedate.Text = DateTime.Today.ToString();
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
            tfsccbatchno.AutoCompleteCustomSource = MyCollectionc;
            connection.Close();
            connection.Open();
            SqlCommand cmdfeed = new SqlCommand("SELECT CONCAT(f.Fpurno,'','-','',f.Fbrand)  FROM  Feed f", connection);
            SqlDataReader readerf = cmdfeed.ExecuteReader();
            AutoCompleteStringCollection MyCollectionf = new AutoCompleteStringCollection();
            while (readerf.Read())
            {
                MyCollectionf.Add(readerf.GetString(0));
            }
            tfsfpurno.AutoCompleteCustomSource = MyCollectionf;

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


        public BindingList<FeedStoVO> ofsv1 = new BindingList<FeedStoVO>();
        public DAL ofsv2 = null;
        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            ofsv2 = new DAL();
            ofsv1 = ofsv2.ViewFS(pageSize, pageIndex, temp, tempAC, out int recordCount, AttNam);
            datagridfs.DataSource = ofsv1;
            this.datagridfs.AllowUserToAddRows = false;
    
            datagridfs.Columns["FSid"].HeaderText = "Id";
            datagridfs.Columns["FSid"].Visible = false;
            datagridfs.Columns["FSsdate"].HeaderText = "Start Date";
            datagridfs.Columns["FSedate"].HeaderText = "End Date";
           datagridfs.Columns["FSfpurno"].HeaderText = "Feed Purchase No";
            datagridfs.Columns["Fbrand"].HeaderText = "Feed Brand";
            datagridfs.Columns["FSfbn"].HeaderText = "Feed Bag Count";
            datagridfs.Columns["FSbalc"].HeaderText = "Balance Bag count";
            datagridfs.Columns["FScbn"].HeaderText = "Chicks Batch No";
            datagridfs.Columns["FSnotes"].HeaderText = "Notes";
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
            int id = Convert.ToInt32(datagridfs.CurrentRow.Cells[0].Value.ToString());

            int i = ofsv2.DelFS(id, AttNam);

            if (i > 0)
            {

                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagridfs.CurrentCell.RowIndex;
                datagridfs.Rows.RemoveAt(selectedRow);
                Cleardata();
                btndel.Enabled = false;
                if (bfsadd.Text == "Update")
                {
                    bfsadd.Text = "Add";
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
        private void bfsadd_Click(object sender, EventArgs e)
        {
            if (tfsfpurno.Text == "" || tfsccbatchno.Text == "" || tfsfbagno.Text == "")
            {

                if (tfsfpurno.Text == "")
                {
                    MessageBox.Show("Feed purchase no is mandatory");
                    tfsfpurno.Focus();
                }
                else if (tfsccbatchno.Text == "")
                {
                    MessageBox.Show("Chicks batch no is mandatory");
                    tfsccbatchno.Focus();
                }
                    

                else if (tfsfbagno.Text == "")
                {
                    MessageBox.Show("Feed bag count is mandatory");
                    tfsfbagno.Focus();
                }
                    
            }
            else
            {
                ofs1.FSid = tfsboxid.Text == "" ? 0 : Convert.ToInt32(tfsboxid.Text);
                ofs1.FSsdate = Convert.ToDateTime(tfssdate.Text);
                ofs1.FSedate = Convert.ToDateTime(tfsedate.Text);
                ofs1.FSfpurno = tfsfpurno.Text.Substring(0,2);
                ofs1.FSfbn = Convert.ToInt32(tfsfbagno.Text);
                ofs1.FScbn = tfsccbatchno.Text;
                ofs1.FSnotes = tfsnotes.Text;

                if (ofs2.InupFS(ofs1, Login.UserName, out DateTime FSfpurdate, out int FSbalbagcou, AttNam) == 2)
                {

                    tfsfpurdate.Visible = true;
                    tfsbalfbagcou.Visible = true;

                    lfsbbc.Visible = true;
                    lfsfpdate.Visible = true;
                    tfsfpurdate.Text = Convert.ToDateTime(FSfpurdate).ToString("dd/MM/yyyy");
                    tfsbalfbagcou.Text = Convert.ToInt16(FSbalbagcou).ToString();
                    MessageBox.Show("Successfully Added!");                               
                    Cleardata();
                    Bindgrid(1, 1, 1);

                }
                else if (ofs2.InupFS(ofs1, Login.UserName, out DateTime FSfpurdate1, out int FSbalbagcou1, AttNam) == 1)
                {
                    tfsfpurdate.Visible = true;
                    tfsbalfbagcou.Visible = true;
                    lfsbbc.Visible = true;
                    lfsfpdate.Visible = true;
                    tfsfpurdate.Text = Convert.ToDateTime(FSfpurdate1).ToString("dd/MM/yyyy");
                    tfsbalfbagcou.Text = Convert.ToInt16(FSbalbagcou1).ToString();
                    MessageBox.Show("Successfully Updated!");                           
                    Cleardata();
                    Bindgrid(1, 1, 1);
                    if (bfsadd.Text == "Update")
                    {
                        bfsadd.Text = "Add";
                    }

                }
            }
        }


        public void Cleardata()
        {
            tfssdate.Text = DateTime.Today.ToString();
            tfsedate.Text = DateTime.Today.ToString();
            tfsfpurno.Text = null;
            tfsfbagno.Text = null;
            tfsccbatchno.Text = null;
            tfsnotes.Text = null;
            tfsfpurdate.Visible = false;
            tfsbalfbagcou.Visible = false;
            lfsbbc.Visible = false;
            lfsfpdate.Visible = false;
            tfsboxid.Text = null;

        }

        private void bfsuptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (bfsadd.Text == "Update")
            {
                bfsadd.Text = "Add";
            }
        }

        private void tfsfbagno_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tfsfbagno_Leave(object sender, EventArgs e)
        {
            if (tfsfpurno.Text != "" && tfsboxid.Text == "")
            {
                int qty = tfsfbagno.Text == "" ? 0 : Convert.ToInt32(tfsfbagno.Text);
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
                comm.CommandText = "Fqty";
                comm.Parameters.AddWithValue("@Pursno", tfsfpurno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (preqty == -99)
                {
                    MessageBox.Show("Please select the valid feed purchase no from suggestion");
                    tfsfpurno.Text = null;
                    tfsfpurno.Focus();
                }
                else if (qty > preqty)
                {
                    MessageBox.Show("Bag count should not greater than the stock Bag count");
                    tfsfbagno.Text = null;
                    tfsfbagno.Focus();
                }
            }



            else if (tfsfpurno.Text != "" && tfsboxid.Text != "")
            {
                int qty = tfsfbagno.Text == "" ? 0 : Convert.ToInt32(tfsfbagno.Text);
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
                comm.CommandText = "Fqty1";
                comm.Parameters.AddWithValue("@Pursno", tfsfpurno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (qty > preqty)
                {
                    MessageBox.Show("Bag count should not greater than the stock Bag count");
                    tfsfbagno.Text = null;
                    tfsfbagno.Focus();
                }
            }
        }

        private void tfsfpurno_Leave(object sender, EventArgs e)
        {
            if (tfsfpurno.Text != "" && tfsboxid.Text == "")
            {
                tfsfpurno.Text= tfsfpurno.Text.Substring(0, 2);
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
                comm.CommandText = "fdrop";
                comm.Parameters.AddWithValue("@Fpurno", tfsfpurno.Text);
                int fpurno = Convert.ToInt16(comm.ExecuteScalar());
                if (fpurno == 0)
                {
                    MessageBox.Show("Please select the valid feed purchase no from suggestion");
                    tfsfpurno.Text = null;
                    tfsfpurno.Focus();
                }
            }
        }

        private void tfsccbatchno_Leave(object sender, EventArgs e)
        {
            if (tfsccbatchno.Text != "")
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
                comm.Parameters.AddWithValue("@Cbatchno", tfsccbatchno.Text);
                int cbatch = Convert.ToInt16(comm.ExecuteScalar());
                if (cbatch == 0)
                {
                    MessageBox.Show("Please select the valid chicks batch no from suggestion");
                    tfsccbatchno.Text = null;
                    tfsccbatchno.Focus();
                }
            }
        }

        private void datagridfs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = true;
            btndel.Focus();
        }

        private void datagridfs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
      tfsboxid.Text = (Convert.ToInt32(datagridfs.CurrentRow.Cells[0].Value)).ToString();
            DateTime FsDate = DateTime.Parse(datagridfs.CurrentRow.Cells[1].Value.ToString());
      tfssdate.Value = DateTime.Parse(FsDate.ToShortDateString());
            DateTime FeDate = DateTime.Parse(datagridfs.CurrentRow.Cells[2].Value.ToString());
            tfsedate.Value = DateTime.Parse(FeDate.ToShortDateString());
            tfsfpurno.Text = this.datagridfs.CurrentRow.Cells[3].Value.ToString();
         tfsfbagno.Text = (Convert.ToInt32(datagridfs.CurrentRow.Cells[5].Value)).ToString();
         tfsbalfbagcou.Text = (Convert.ToInt32(datagridfs.CurrentRow.Cells[6].Value)).ToString();
          tfsccbatchno.Text = this.datagridfs.CurrentRow.Cells[7].Value.ToString();
            tfsnotes.Text = this.datagridfs.CurrentRow.Cells[8].Value.ToString();
           bfsadd.Text = "Update";
        }

  
    }
}
