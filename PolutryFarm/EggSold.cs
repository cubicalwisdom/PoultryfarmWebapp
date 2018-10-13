using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOLayer.cs;
using DALayer;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;

namespace PolutryFarm
{
    public partial class EggSold : Form
    {
        int pageSize = 25;
        int selectedRow;
        bool tempbool = true;
        bool tempboolAC = true;
        private int AttNam = 0;
        private string constring;
        public EggSoldVO oes1 = new EggSoldVO();
        public DAL oes2 = new DAL();
        public EggSold()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            EggSoldMain.Enabled = false;
            tesboxid.Visible = false;
            tesinstock.Visible = false;
            lesinstock.Visible = false;
            tesdate.Text = DateTime.Today.ToString();           
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

        public BindingList<EggSoldVO> oesv1 = new BindingList<EggSoldVO>();
        public DAL oesv2 = null;

        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            oesv2 = new DAL();
            oesv1 = oesv2.ViewES(pageSize, pageIndex, temp, tempAC, out int recordCount, AttNam);
            datagrideggsold.DataSource = oesv1;
            this.datagrideggsold.AllowUserToAddRows = false;
            datagrideggsold.Columns["ESid"].HeaderText = "Id";
            datagrideggsold.Columns["ESid"].Visible = false;
            datagrideggsold.Columns["ESdate"].HeaderText = "Date";
            datagrideggsold.Columns["ESqty"].HeaderText = "Qty";
            datagrideggsold.Columns["ESamt"].HeaderText = "Price/Egg";
            datagrideggsold.Columns["ESinstock"].HeaderText = "Egg Instock";
            datagrideggsold.Columns["ESnotes"].HeaderText = "Notes";
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
            int id = Convert.ToInt32(datagrideggsold.CurrentRow.Cells[0].Value.ToString());

            int i = oesv2.DelES(id, AttNam);

            if (i > 0)
            {

                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagrideggsold.CurrentCell.RowIndex;
                datagrideggsold.Rows.RemoveAt(selectedRow);
                Cleardata();
                btndel.Enabled = false;
                if (besadd.Text == "Update")
                {
                    besadd.Text = "Add";
                }
            }

        }

        private void besadd_Click(object sender, EventArgs e)
        {
            
                if (tesqty.Text == "")
                {

                    MessageBox.Show("Quantity is mandatory");
                    tesqty.Focus();
                }
                else
                {

                    oes1.ESid = tesboxid.Text == "" ? 0 : Convert.ToInt32(tesboxid.Text);
                    oes1.ESdate = Convert.ToDateTime(tesdate.Text);
                    oes1.ESqty = Convert.ToInt32(tesqty.Text);
                    oes1.ESamt = tesamt.Text == "" ? 0 : Convert.ToDecimal(tesamt.Text);
                    oes1.EStotamt = testotamt.Text == "" ? 0 : Convert.ToDecimal(testotamt.Text);
                    oes1.ESnotes = tesnotes.Text;

                    if (oes2.InupES(oes1, Login.UserName, out int Einstock, AttNam) == 2)
                    {
                        tesinstock.Visible = true;
                        lesinstock.Visible = true;
                        tesinstock.Text = Convert.ToInt32(Einstock).ToString();
                        MessageBox.Show("Successfully Added!");                     
                        Cleardata();
                        Bindgrid(1, 1, 1);

                    }
                    else if (oes2.InupES(oes1, Login.UserName, out int Einstock1, AttNam) == 1)
                    {

                        tesinstock.Visible = true;
                        lesinstock.Visible = true;
                        tesinstock.Text = Convert.ToInt32(Einstock1).ToString();
                        MessageBox.Show("Successfully Updated!");

                        if (besadd.Text == "Update")
                        {
                            besadd.Text = "Add";
                        }
                        Cleardata();
                        Bindgrid(1, 1, 1);


                    }

            }

        }

        public void Cleardata()
        {
            tesboxid.Text = null;
            tesdate.Text = DateTime.Today.ToString();
            tesqty.Text = null;
            tesamt.Text = null;
            testotamt.Text = null;
            tesnotes.Text = null;
            tesinstock.Visible = false;
            lesinstock.Visible = false;
            tesqty.Enabled = true;
            testotamt.Enabled = true;
            tesamt.Enabled = true;


        }

        private void besuptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (besadd.Text == "Update")
            {
                besadd.Text = "Add";
            }
        }

        private void tesqty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tesamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tesamt.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }

        }

        private void testotamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(testotamt.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }

        }

        private void tesqty_Leave(object sender, EventArgs e)
        {
            if (tesqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tesqty.Text = null;
                tesqty.Focus();
            }
            if (tesqty.Text != "" && tesqty.Text != "0")
            {
                int qty = tesqty.Text == "" ? 0 : Convert.ToInt32(tesqty.Text);

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
                comm.CommandText = "Eqty";
                comm.Parameters.AddWithValue("@Eqty", qty);
                int value = Convert.ToInt16(comm.ExecuteScalar());
                if (value == 0)
                {
                    MessageBox.Show("Quantity should not greater than the actual egg stock");
                    tesqty.Text = null;
                }
                else
                {
                    if (testotamt.Text != "" && testotamt.Enabled == true)
                    {
                        tesamt.Text = Convert.ToDecimal((Convert.ToDecimal(testotamt.Text) / Convert.ToDecimal(tesqty.Text)))
                            .ToString("#,0.00");
                        if (tesboxid.Text == "")
                            tesamt.Enabled = false;
                        if (tesamt.Text.Length > 15)
                        {
                            MessageBox.Show("Amount length is not valid ");
                            testotamt.Text = null;
                            tesamt.Text = null;
                            testotamt.Enabled = true;
                            tesamt.Enabled = true;
                            testotamt.Focus();
                        }
                        
                         
                    }
                   else if (tesamt.Text != "" && tesamt.Enabled == true)
                    {
                        testotamt.Text = Convert.ToDecimal((Convert.ToDecimal(tesamt.Text) * Convert.ToDecimal(tesqty.Text)))
                            .ToString("#,0.00");
                        if (tesboxid.Text == "")
                            testotamt.Enabled = false;
                        if (testotamt.Text.Length > 15)
                        {
                            MessageBox.Show("Amount length is not valid ");
                            testotamt.Text = null;
                            tesamt.Text = null;
                            testotamt.Enabled = true;
                            tesamt.Enabled = true;
                            testotamt.Focus();
                        }
                       
                          
                    }
                }
                      
            }
        }

        private void tesamt_Leave(object sender, EventArgs e)
        {
            if (tesqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tesqty.Text = null;
                tesqty.Focus();
            }
            if (tesqty.Text != "" && tesamt.Text != "" && tesqty.Text != "0" && tesamt.Enabled == true)
            {

                testotamt.Text = Convert.ToDecimal((Convert.ToDecimal(tesamt.Text) * Convert.ToDecimal(tesqty.Text)))
                    .ToString("#,0.00");
                if (tesboxid.Text == "")
                    testotamt.Enabled = false;
                if (testotamt.Text.Length > 15)
                {
                    MessageBox.Show("Amount length is not valid ");
                    testotamt.Text = null;
                    tesamt.Text = null;
                    testotamt.Enabled = true;
                    tesamt.Enabled = true;
                    tesamt.Focus();
                }
               
            }
        }

        private void testotamt_Leave(object sender, EventArgs e)
        {

            if (tesqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tesqty.Text = null;
                tesqty.Focus();
            }
            if (tesqty.Text != "" && testotamt.Text != "" && tesqty.Text != "0" && testotamt.Enabled == true)
            {

                tesamt.Text = Convert.ToDecimal((Convert.ToDecimal(testotamt.Text) / Convert.ToDecimal(tesqty.Text)))
                    .ToString("#,0.00");
                if (tesboxid.Text == "")
                    tesamt.Enabled = false;
                if (tesamt.Text.Length > 15)
                {
                    MessageBox.Show("Amount length is not valid ");
                    testotamt.Text = null;
                    tesamt.Text = null;
                    testotamt.Enabled = true;
                    tesamt.Enabled = true;
                    testotamt.Focus();
                }
              
            }
        }

        private void datagrideggsold_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = true;
            btndel.Focus();

        }

        private void datagrideggsold_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tesboxid.Text = (Convert.ToInt32(datagrideggsold.CurrentRow.Cells[0].Value)).ToString();
            DateTime EsDate = DateTime.Parse(datagrideggsold.CurrentRow.Cells[1].Value.ToString());
            tesdate.Value = DateTime.Parse(EsDate.ToShortDateString());
            tesqty.Text = this.datagrideggsold.CurrentRow.Cells[2].Value.ToString();
            tesamt.Text = this.datagrideggsold.CurrentRow.Cells[3].Value.ToString();
            testotamt.Text = this.datagrideggsold.CurrentRow.Cells[4].Value.ToString();

            besadd.Text = "Update";
           
        }
    }
}
