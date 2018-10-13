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
    public partial class ChicksSold : Form
    {
        int pageSize = 25;
        int selectedRow;
        bool tempbool = true;
        bool tempboolAC = true;
        private int AttNam = 0;
        private string constring;
        public ChicksSoldVO ocs1 = new ChicksSoldVO();
        public DAL ocs2 = new DAL();

        public ChicksSold()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            ChicksSoldMain.Enabled = false;
            tcsboxid.Visible = false;
            tcsbqty.Visible = false;
            lcsbq.Visible = false;
            tcsdate.Text = DateTime.Today.ToString();
            cbcsw.Items.Add("KG");
            cbcsw.Items.Add("Ton");
           cbcsw.SelectedIndex = 0;
            btndel.Enabled = false;
            cbcsw.DropDownStyle = ComboBoxStyle.DropDownList;
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
            tcsbatchno.AutoCompleteCustomSource = MyCollection;

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
                    m.Result = (IntPtr) 2;
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr) 17;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void bcsadd_Click(object sender, EventArgs e)
        {
            if (tcsbatchno.Text == "")
            {
                MessageBox.Show("BatchNo are mandatory");
                tcsbatchno.Focus();

            }
            else if (tcsqty.Text == "")
            {
                MessageBox.Show("Quantity is mandatory");
                tcsqty.Focus();
            }
            else
            {
                ocs1.CSid = tcsboxid.Text == "" ? 0 : Convert.ToInt32(tcsboxid.Text);
                ocs1.CSdate = Convert.ToDateTime(tcsdate.Text);
                ocs1.CSqty = Convert.ToInt32(tcsqty.Text);
                ocs1.CSbatchno = tcsbatchno.Text;
                var item = this.cbcsw.GetItemText(this.cbcsw.SelectedItem);
                ocs1.CSwmea = item;
                ocs1.CStotalwt = tcsaw.Text == "" ? 0 : Convert.ToDecimal(tcstw.Text);
                ocs1.CSavgwt = tcsaw.Text == "" ? 0 : Convert.ToDecimal(tcsaw.Text);
                ocs1.CStotalamt = tcsta.Text == "" ? 0 : Convert.ToDecimal(tcsta.Text);
                ocs1.CSavgamt = tcsaa.Text == "" ? 0 : Convert.ToDecimal(tcsaa.Text);


                ocs1.CSnotes = tcsnotes.Text;

                if (ocs2.InupCS(ocs1, Login.UserName ,out int CSBalqty,AttNam) == 2)
                {
                    tcsbqty.Visible = true;
                    lcsbq.Visible = true;
                    tcsbqty.Text = Convert.ToInt16(CSBalqty).ToString();
                    MessageBox.Show("Successfully Added!");
                    Bindgrid(1, 1, 1);
                    Cleardata();


                }
                else if (ocs2.InupCS(ocs1, Login.UserName, out int CSBalqty1, AttNam) == 1)
                {

                    tcsbqty.Visible = true;
                    lcsbq.Visible = true;
                    tcsbqty.Text = Convert.ToInt16(CSBalqty1).ToString();
                    MessageBox.Show("Successfully Updated!");
                    if (bcsadd.Text == "Update")
                    {
                        bcsadd.Text = "Add";
                    }
                    Cleardata();
                    Bindgrid(1, 1, 1);
                }
            }
     


      
    }

        public void Cleardata()
        {
            tcsdate.Text = DateTime.Today.ToString();
            tcsqty.Text = null;
            tcsbatchno.Text = null;
            tcstw.Text = null;
            tcsaw.Text = null;
            tcsta.Text = null;
            tcsaa.Text = null;
            tcsnotes.Text = null;        
            tcsbqty.Visible = false;
            lcsbq.Visible = false;
            tcsqty.Enabled = true;
            tcstw.Enabled = true;
            tcsaw.Enabled = true;
            tcsta.Enabled = true;
            tcsaa.Enabled = true;
            tcsboxid.Text = null;
        }

        private void bcsuptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (bcsadd.Text == "Update")
            {
                bcsadd.Text = "Add";
            }
        }

        private void tcsqty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tcstw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tcstw.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void tcsaw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tcsaw.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }

        }

        private void tcsta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tcsta.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void tcsaa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tcsaa.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }



        private void tcsbatchno_Leave(object sender, EventArgs e)
        {
            

           

            if (tcsbatchno.Text != "" && tcsboxid.Text == "")
            {
                int qty = tcsqty.Text == "" ? 0 : Convert.ToInt32(tcsqty.Text);
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
                comm.Parameters.AddWithValue("@Cbatchno", tcsbatchno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (preqty == -99)
                {
                    MessageBox.Show("Please select the valid batchno from suggestion");
                    tcsbatchno.Text = null;
                    tcsbatchno.Focus();
                }
                else if (qty > preqty)
                {
                    MessageBox.Show("Quantity should not greater than the actual chicks qty");
                    tcsqty.Text = null;
                    tcsqty.Focus();
                }
            }

            else if (tcsbatchno.Text != "" && tcsboxid.Text != "")
            {
                int qty = tcsqty.Text == "" ? 0 : Convert.ToInt32(tcsqty.Text);

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
                comm.Parameters.AddWithValue("@Cbatchno", tcsbatchno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (qty > preqty)
                {
                    MessageBox.Show("Quantity should not greater than the actual chicks qty");
                    tcsqty.Text = null;
                    tcsqty.Focus();
                }
            }
        }

      



     
        private void Chicks_Click(object sender, EventArgs e)
        {
            this.Hide();
            Chicks c = new Chicks();
            c.Show();
        }

        private void ChicksMortality_Click(object sender, EventArgs e)
        {
           this.Hide();
            ChicksMortality cm = new ChicksMortality();
            cm.Show();
        }



        private void tcsqty_Leave(object sender, EventArgs e)
        {
            if (tcsqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcsqty.Text = null;
                tcsqty.Focus();
            }

            if (tcsbatchno.Text != "" && tcsboxid.Text == "")
            {
                int qty = tcsqty.Text == "" ? 0 : Convert.ToInt32(tcsqty.Text);
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
                comm.Parameters.AddWithValue("@Cbatchno", tcsbatchno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (preqty == -99)
                {
                    MessageBox.Show("Please select the valid batchno from suggestion");
                    tcsbatchno.Text = null;
                    tcsbatchno.Focus();
                }
                else if (qty > preqty)
                {
                    MessageBox.Show("Quantity should not greater than the actual chicks qty");
                    tcsqty.Text = null;
                    tcsqty.Focus();
                }
            }



            else if (tcsbatchno.Text != "" && tcsboxid.Text != "")
            {
                int qty = tcsqty.Text == "" ? 0 : Convert.ToInt32(tcsqty.Text);
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
                comm.Parameters.AddWithValue("@Cbatchno", tcsbatchno.Text);
                int preqty = Convert.ToInt16(comm.ExecuteScalar());
                if (qty > preqty)
                {
                    MessageBox.Show("Quantity should not greater than the actual chicks qty");
                    tcsqty.Text = null;
                    tcsqty.Focus();
                }
            }



            if (tcsqty.Text != "" && tcsqty.Text != "0")
            {


                if (tcsta.Text != "" && tcsta.Enabled == true)
                {
                    tcsaa.Text = Convert.ToDecimal((Convert.ToDecimal(tcsta.Text) / Convert.ToDecimal(tcsqty.Text)))
                        .ToString("#,0.00");
                    if (tcsboxid.Text == "")
                        tcsaa.Enabled = false;
                    if (tcsaa.Text.Length > 15)
                    {
                        MessageBox.Show("Amount length is not valid ");
                        tcsta.Text = null;
                        tcsaa.Text = null;
                        tcsta.Enabled = true;
                        tcsaa.Enabled = true;
                        tcsta.Focus();
                    }


                }
                else if (tcsaa.Text != "" && tcsaa.Enabled == true)
                {
                    tcsta.Text = Convert.ToDecimal((Convert.ToDecimal(tcsaa.Text) * Convert.ToDecimal(tcsqty.Text)))
                        .ToString("#,0.00");
                    if (tcsboxid.Text == "")
                        tcsta.Enabled = false;
                    if (tcsta.Text.Length > 15)
                    {
                        MessageBox.Show("Amount length is not valid ");
                        tcsta.Text = null;
                        tcsaa.Text = null;
                        tcsta.Enabled = true;
                        tcsaa.Enabled = true;
                        tcsta.Focus();
                    }

                }


                if (tcstw.Text != "" && tcstw.Enabled == true)
                {
                    tcsaw.Text = Convert.ToDecimal((Convert.ToDecimal(tcstw.Text) / Convert.ToDecimal(tcsqty.Text)))
                        .ToString("#,0.00");
                    if (tcsboxid.Text == "")
                        tcsaw.Enabled = false;
                    if (tcsaw.Text.Length > 15)
                    {
                        MessageBox.Show("Weight length is not valid ");
                        tcstw.Text = null;
                        tcsaw.Text = null;
                        tcstw.Enabled = true;
                        tcsaw.Enabled = true;
                        tcstw.Focus();
                    }


                }
                else if (tcsaw.Text != "" && tcsaw.Enabled == true)
                {
                    tcstw.Text = Convert.ToDecimal((Convert.ToDecimal(tcsaw.Text) * Convert.ToDecimal(tcsqty.Text)))
                        .ToString("#,0.00");
                    if (tcsboxid.Text == "")
                        tcstw.Enabled = false;
                    if (tcstw.Text.Length > 15)
                    {
                        MessageBox.Show("Weight length is not valid ");
                        tcstw.Text = null;
                        tcsaw.Text = null;
                        tcstw.Enabled = true;
                        tcsaw.Enabled = true;
                        tcstw.Focus();
                    }

                }
            }
        }



        private void tcstw_Leave(object sender, EventArgs e)
        {


            if (tcsqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcsqty.Text = null;
                tcsqty.Focus();
            }
            if (tcsqty.Text != "" && tcstw.Text != "" && tcsqty.Text != "0" && tcstw.Enabled == true)
            {

                tcsaw.Text = Convert.ToDecimal((Convert.ToDecimal(tcstw.Text) / Convert.ToDecimal(tcsqty.Text)))
                    .ToString("#,0.00");
                if (tcsboxid.Text == "")
                    tcsaw.Enabled = false;
                if (tcsaw.Text.Length > 15)
                {
                    MessageBox.Show("Weight length is not valid ");
                    tcstw.Text = null;
                    tcsaw.Text = null;
                    tcstw.Enabled = true;
                    tcsaw.Enabled = true;
                    tcstw.Focus();
                }

            }

        }

        private void tcsaw_Leave(object sender, EventArgs e)
        {
            if (tcsqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcsqty.Text = null;
                tcsqty.Focus();
            }
            if (tcsqty.Text != "" && tcsaw.Text != "" && tcsqty.Text != "0" && tcsaw.Enabled == true)
            {

                tcstw.Text = Convert.ToDecimal((Convert.ToDecimal(tcsaw.Text) * Convert.ToDecimal(tcsqty.Text)))
                    .ToString("#,0.00");
                if (tcsboxid.Text == "")
                    tcstw.Enabled = false;
                if (tcstw.Text.Length > 15)
                {
                    MessageBox.Show("Weight length is not valid ");
                    tcstw.Text = null;
                    tcsaw.Text = null;
                    tcstw.Enabled = true;
                    tcsaw.Enabled = true;
                    tcsaw.Focus();
                }

            }

        }

        private void tcsta_Leave(object sender, EventArgs e)
        {
            if (tcsqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcsqty.Text = null;
                tcsqty.Focus();
            }
            if (tcsqty.Text != "" && tcsta.Text != "" && tcsqty.Text != "0" && tcsta.Enabled == true)
            {

                tcsaa.Text = Convert.ToDecimal((Convert.ToDecimal(tcsta.Text) / Convert.ToDecimal(tcsqty.Text)))
                    .ToString("#,0.00");
                if (tcsboxid.Text == "")
                    tcstw.Enabled = false;
                if (tcsaa.Text.Length > 15)
                {
                    MessageBox.Show("Amount length is not valid ");
                    tcsta.Text = null;
                    tcsaa.Text = null;
                    tcsta.Enabled = true;
                    tcsaa.Enabled = true;
                    tcsta.Focus();
                }

            }
        }
        private void tcsaa_Leave(object sender, EventArgs e)
        {
            if (tcsqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcsqty.Text = null;
                tcsqty.Focus();
            }
            if (tcsqty.Text != "" && tcsaa.Text != "" && tcsqty.Text != "0" && tcsaa.Enabled == true)
            {

                tcsta.Text = Convert.ToDecimal((Convert.ToDecimal(tcsaa.Text) * Convert.ToDecimal(tcsqty.Text)))
                    .ToString("#,0.00");
                if (tcsboxid.Text == "")
                    tcsta.Enabled = false;
                if (tcsta.Text.Length > 15)
                {
                    MessageBox.Show("Amount length is not valid ");
                    tcsta.Text = null;
                    tcsaa.Text = null;
                    tcsta.Enabled = true;
                    tcsaa.Enabled = true;
                    tcsaa.Focus();
                }

            }
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


        public BindingList<ChicksSoldVO> ocsv1 = new BindingList<ChicksSoldVO>();
        public DAL ocsv2 = null;

        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            ocsv2 = new DAL();
            ocsv1 = ocsv2.ViewCS(pageSize, pageIndex, temp, tempAC, out int recordCount, AttNam);
            datagridcs.DataSource = ocsv1;
            this.datagridcs.AllowUserToAddRows = false;
            datagridcs.Columns["CSid"].HeaderText = "Id";
            datagridcs.Columns["CSid"].Visible = false;
            datagridcs.Columns["CSdate"].HeaderText = "Date";
            datagridcs.Columns["CSbatchno"].HeaderText = "Batch No";
            datagridcs.Columns["CSqty"].HeaderText = "QTY";
            datagridcs.Columns["CSBalqty"].HeaderText = "Balance QTY";
            datagridcs.Columns["CStotalwt"].HeaderText = "Tot Weight";
            datagridcs.Columns["CSavgwt"].HeaderText = "Avg Weight";
            datagridcs.Columns["CSwmea"].HeaderText = "Measure";
            datagridcs.Columns["CStotalamt"].HeaderText = "Tot Amount";
            datagridcs.Columns["CSavgamt"].HeaderText = "Price/Chicks";
            datagridcs.Columns["CSnotes"].HeaderText = "Notes";
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
            int id = Convert.ToInt32(datagridcs.CurrentRow.Cells[0].Value.ToString());

            int i = ocsv2.DelCS(id, AttNam);

            if (i > 0)
            {

                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagridcs.CurrentCell.RowIndex;
                Cleardata();
                datagridcs.Rows.RemoveAt(selectedRow);
                btndel.Enabled = false;
                if (bcsadd.Text == "Update")
                {
                    bcsadd.Text = "Add";
                }
            }

        }

        private void datagridcs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
          tcsboxid.Text = (Convert.ToInt32(datagridcs.CurrentRow.Cells[0].Value)).ToString();
            DateTime CDate = DateTime.Parse(datagridcs.CurrentRow.Cells[1].Value.ToString());
           tcsdate.Value = DateTime.Parse(CDate.ToShortDateString());
          tcsbatchno.Text = this.datagridcs.CurrentRow.Cells[2].Value.ToString();
            tcsqty.Text = this.datagridcs.CurrentRow.Cells[3].Value.ToString();
           tcstw.Text = (Convert.ToDecimal(datagridcs.CurrentRow.Cells[5].Value)).ToString();
          tcsaw.Text = (Convert.ToDecimal(datagridcs.CurrentRow.Cells[6].Value)).ToString();
          cbcsw.Text = this.datagridcs.CurrentRow.Cells[7].Value.ToString();
          tcsta.Text = this.datagridcs.CurrentRow.Cells[8].Value.ToString();
            tcsaa.Text = (Convert.ToDecimal(datagridcs.CurrentRow.Cells[9].Value)).ToString();
          tcsnotes.Text = this.datagridcs.CurrentRow.Cells[10].Value.ToString();     
          bcsadd.Text = "Update";    
            tcsbatchno.Enabled = false;
            tcsqty.Enabled = true;
            tcstw.Enabled = true;
            tcsaw.Enabled = true;
            tcsta.Enabled = true;
            tcsaa.Enabled = true;

        }

        private void datagridcs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = true;
            btndel.Focus();
        }
    }
}
