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
using System.IO;
using System.Text.RegularExpressions;

namespace PolutryFarm
{
    public partial class Chicks : Form
    {
        int pageSize = 25;
        int selectedRow;
        bool tempbool = true;
        bool tempboolAC = true;
        public ChicksVO oc1 = new ChicksVO();
        public DAL oc2 = new DAL();

        private int AttNam = 0;
        private string constring;
        public Chicks()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            ChicksMain.Enabled = false;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            cbcw.Items.Add("KG");
            cbcw.Items.Add("Ton");
            cbcw.SelectedIndex = 0;
            cbcw.DropDownStyle = ComboBoxStyle.DropDownList;
            tcboxid.Visible = false;
            tcdate.Text = DateTime.Today.ToString();
            tcbdate.Text = DateTime.Today.ToString();
            Precheck();

            if (Main.Loc == 1)
            {
                label2.Text = "Attur";

            }
            else if (Main.Loc == 2)
            {
                label2.Text = "Namakkal";
            }
            Bindgrid(1, 1, 1);
            btndel.Enabled = false;
        }

        public void Precheck()
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
            comm.CommandText = "Chickspre";
            comm.Parameters.Add("@Cp", SqlDbType.NVarChar, 50);
            comm.Parameters["@Cp"].Direction = ParameterDirection.Output;
            comm.ExecuteNonQuery();
            string Prechicks = comm.Parameters["@Cp"].Value.ToString();

            if (Prechicks == "")
            {
                Chickspre.Text = "00";
            }
            else
            {
                Chickspre.Text = Prechicks;
            }
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


        public BindingList<ChicksVO> ocv1 = new BindingList<ChicksVO>();
        public DAL ocv2 = null;

        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            ocv2 = new DAL();
            ocv1 = ocv2.ViewC(pageSize, pageIndex, temp, tempAC, out int recordCount,AttNam);
            datagridchicks.DataSource = ocv1;
            this.datagridchicks.AllowUserToAddRows = false;
            datagridchicks.Columns["Cid"].HeaderText = "Id";
            datagridchicks.Columns["Cid"].Visible = false;
            datagridchicks.Columns["Cdate"].HeaderText = "Date";
            datagridchicks.Columns["Cbatchno"].HeaderText = "Batch No";
            datagridchicks.Columns["Cbreed"].HeaderText = "Breed";
            datagridchicks.Columns["Cqty"].HeaderText = "Qty";
            datagridchicks.Columns["Ctotalwt"].HeaderText = "Tot Weight";
            datagridchicks.Columns["Cavgwt"].HeaderText = "Avg Weight";
            datagridchicks.Columns["Cwmea"].HeaderText = "Measure";
            datagridchicks.Columns["Ctotalamt"].HeaderText = "Tot Amount";
            datagridchicks.Columns["Cavgamt"].HeaderText = "Price/Chicks";
            datagridchicks.Columns["Cbate"].HeaderText = "Birth Date";
            datagridchicks.Columns["Caid"].HeaderText = "Age";
            datagridchicks.Columns["Cnotes"].HeaderText = "Notes";
            PopulatePager(recordCount, pageIndex);
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            BindingList<Page> pages = new BindingList<Page>();
            int startIndex, endIndex;
            int pagerSpan = 2;
           
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

    
                if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

        
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

           
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

         
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

          
            pnlPager.Controls.Clear();

       
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
            int id = Convert.ToInt32(datagridchicks.CurrentRow.Cells[0].Value.ToString());

            int i = ocv2.DelC(id, AttNam);

            if (i > 0)
            {

                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagridchicks.CurrentCell.RowIndex;

                datagridchicks.Rows.RemoveAt(selectedRow);
                Cleardata();
                Precheck();
                btndel.Enabled = false;
                if (bcadd.Text == "Update")
                {
                    bcadd.Text = "Add";
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

        private void bcadd_Click(object sender, EventArgs e)
        {
            if (tcbatchno.Text == "")
            {
                MessageBox.Show("Batch no is mandatory");

                tcbatchno.Focus();

            }
            else if (tcqty.Text == "")
            {
                MessageBox.Show("Quantity is mandatory");
                tcqty.Focus();

            }
            else
            {
                oc1.Cid = tcboxid.Text == "" ? 0 : Convert.ToInt32(tcboxid.Text);
                oc1.Cdate = Convert.ToDateTime(tcdate.Text);
                oc1.Cbatchno = tcbatchno.Text;
                oc1.Cbreed = tcbreed.Text;
                oc1.Cqty = Convert.ToInt32(tcqty.Text);
                oc1.Ctotalwt = tctw.Text == "" ? 0 : Convert.ToDecimal(tctw.Text);
                var item = this.cbcw.GetItemText(this.cbcw.SelectedItem);
                oc1.Cwmea = item;
                oc1.Cavgwt = tcaw.Text == "" ? 0 : Convert.ToDecimal(tcaw.Text);
                oc1.Ctotalamt = tcta.Text == "" ? 0 : Convert.ToDecimal(tcta.Text);
                oc1.Cavgamt = tcaa.Text == "" ? 0 : Convert.ToDecimal(tcaa.Text);
                oc1.Cbate = Convert.ToDateTime(tcbdate.Text);
                oc1.Caid = tcaid.Text == "" ? 0 : Convert.ToInt32(tcaid.Text);
                oc1.Cnotes = tcnotes.Text;

                if (oc2.InupC(oc1,Login.UserName, AttNam) == 2)
                {
                    MessageBox.Show("Successfully Added!");
                    Cleardata();
                    Precheck();
                    Bindgrid(1, 1, 1);

                }
                else if (oc2.InupC(oc1, Login.UserName, AttNam) == 1)
                {
                    MessageBox.Show("Successfully Updated!");
                    Cleardata();
                    if (bcadd.Text == "Update")
                    {
                        bcadd.Text = "Add";
                    }
                    Precheck();
                    Bindgrid(1, 1, 1);


                }
            }
        }

        public void Cleardata()
        {
            tcdate.Text = DateTime.Today.ToString();
            tcbatchno.Text = null;
            tcbreed.Text = null;
            tcqty.Text = null;
            tctw.Text = null;
            tcaw.Text = null;
            tcta.Text = null;
            tcaa.Text = null;
            tcbdate.Text = DateTime.Today.ToString();
            tcaid.Text = null;
            tcnotes.Text = null;
            tcqty.Enabled = true;
            tctw.Enabled = true;
            tcaw.Enabled = true;
            tcta.Enabled = true;
            tcaa.Enabled = true;
            tcboxid.Text = null;

        }

        private void bcuptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (bcadd.Text == "Update")
            {
                bcadd.Text = "Add";
            }
        }

        private void tcbatchno_Leave(object sender, EventArgs e)
        {
            if (tcbatchno.Text.Length < 2 && tcbatchno.Text != "")
            {

                MessageBox.Show("Batch no should not be less than 2");
                tcbatchno.Text = null;
                tcbatchno.Focus();
            }
            if (tcboxid.Text == "")
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
                comm.CommandText = "Cbatchno";
                comm.Parameters.AddWithValue("@Cbatchno", tcbatchno.Text);
                int res = Convert.ToInt16(comm.ExecuteScalar());
                if (res == 0)
                {
                    MessageBox.Show("This batch no is already present .Please enter the different one");
                    tcbatchno.Text = null;
                    tcbatchno.Focus();
                }
            }

            else if (tcboxid.Text != "")
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
                comm.CommandText = "Cbatchno1";
                comm.Parameters.AddWithValue("@Cid1", tcboxid.Text);
                comm.Parameters.AddWithValue("@Cbatchno1", tcbatchno.Text);
                int res1 = Convert.ToInt16(comm.ExecuteScalar());
                if (res1 == 0)
                {
                    MessageBox.Show("This batch no is already present .Please enter the different one");
                    tcbatchno.Text = null;
                    tcbatchno.Focus();
                }

            }
        }

        private void tcbatchno_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tcqty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tctw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tctw.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void tcaw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tcaw.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void tcta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tcta.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }

        }

        private void tcaa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tcaa.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void tcaid_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tcqty_Leave(object sender, EventArgs e)
        {


            if (tcboxid.Text != "")
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
                comm.CommandText = "CQtycheck";
                comm.Parameters.AddWithValue("@Cqty1", tcqty.Text);
                comm.Parameters.AddWithValue("@Cbatchno1", tcbatchno.Text);
                int res1 = Convert.ToInt16(comm.ExecuteScalar());
                if (res1 == 0)
                {
                    MessageBox.Show("Quantity should not less than the Mortality and Sold Quantity ");
                    tcqty.Text = null;
                    tcqty.Focus();
                }
            }
            if (tcqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcqty.Text = null;
                tcqty.Focus();
            }

            if (tcqty.Text != "" && tcqty.Text != "0")
            {


                if (tcta.Text != "" && tcta.Enabled == true)
                {
                    tcaa.Text = Convert.ToDecimal((Convert.ToDecimal(tcta.Text) / Convert.ToDecimal(tcqty.Text)))
                        .ToString("#,0.00");
                    if (tcboxid.Text == "")
                        tcaa.Enabled = false;
                    if (tcaa.Text.Length > 15)
                    {
                        MessageBox.Show("Amount length is not valid ");
                        tcta.Text = null;
                        tcaa.Text = null;
                        tcta.Enabled = true;
                        tcaa.Enabled = true;
                        tcta.Focus();
                    }


                }
                else if (tcaa.Text != "" && tcaa.Enabled == true)
                {
                    tcta.Text = Convert.ToDecimal((Convert.ToDecimal(tcaa.Text) * Convert.ToDecimal(tcqty.Text)))
                        .ToString("#,0.00");
                    if (tcboxid.Text == "")
                        tcta.Enabled = false;
                    if (tcta.Text.Length > 15)
                    {
                        MessageBox.Show("Amount length is not valid ");
                        tcta.Text = null;
                        tcaa.Text = null;
                        tcta.Enabled = true;
                        tcaa.Enabled = true;
                        tcta.Focus();
                    }

                }


                if (tctw.Text != "" && tctw.Enabled == true)
                {
                    tcaw.Text = Convert.ToDecimal((Convert.ToDecimal(tctw.Text) / Convert.ToDecimal(tcqty.Text)))
                        .ToString("#,0.00");
                    if (tcboxid.Text == "")
                        tcaw.Enabled = false;
                    if (tcaw.Text.Length > 15)
                    {
                        MessageBox.Show("Weight length is not valid ");
                        tctw.Text = null;
                        tcaw.Text = null;
                        tctw.Enabled = true;
                        tcaw.Enabled = true;
                        tctw.Focus();
                    }


                }
                else if (tcaw.Text != "" && tcaw.Enabled == true)
                {
                    tctw.Text = Convert.ToDecimal((Convert.ToDecimal(tcaw.Text) * Convert.ToDecimal(tcqty.Text)))
                        .ToString("#,0.00");
                    if (tcboxid.Text == "")
                        tctw.Enabled = false;
                    if (tctw.Text.Length > 15)
                    {
                        MessageBox.Show("Weight length is not valid ");
                        tctw.Text = null;
                        tcaw.Text = null;
                        tctw.Enabled = true;
                        tcaw.Enabled = true;
                        tctw.Focus();
                    }

               }
            }
        }

        private void tctw_Leave(object sender, EventArgs e)
        {
         

            if (tcqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcqty.Text = null;
                tcqty.Focus();
            }
            if (tcqty.Text != "" && tctw.Text != "" && tcqty.Text != "0" && tctw.Enabled == true)
            {

                tcaw.Text = Convert.ToDecimal((Convert.ToDecimal(tctw.Text) / Convert.ToDecimal(tcqty.Text)))
                    .ToString("#,0.00");
                if (tcboxid.Text == "")
                    tcaw.Enabled = false;
                if (tcaw.Text.Length > 15)
                {
                    MessageBox.Show("Weight length is not valid ");
                    tctw.Text = null;
                    tcaw.Text = null;
                    tctw.Enabled = true;
                    tcaw.Enabled = true;
                    tctw.Focus();
                }
              
            }

        }

        private void tcaw_Leave(object sender, EventArgs e)
        {
            if (tcqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcqty.Text = null;
                tcqty.Focus();
            }
            if (tcqty.Text != "" && tcaw.Text != "" && tcqty.Text != "0" && tcaw.Enabled == true)
            {

                tctw.Text = Convert.ToDecimal((Convert.ToDecimal(tcaw.Text) * Convert.ToDecimal(tcqty.Text)))
                    .ToString("#,0.00");
                if (tcboxid.Text == "")
                    tctw.Enabled = false;
                if (tctw.Text.Length > 15)
                {
                    MessageBox.Show("Weight length is not valid ");
                    tctw.Text = null;
                    tcaw.Text = null;
                    tctw.Enabled = true;
                    tcaw.Enabled = true;
                    tcaw.Focus();
                }
               
            }

        }

        private void tcta_Leave(object sender, EventArgs e)
        {
            if (tcqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcqty.Text = null;
                tcqty.Focus();
            }
            if (tcqty.Text != "" && tcta.Text != "" && tcqty.Text != "0" && tcta.Enabled == true)
            {

                tcaa.Text = Convert.ToDecimal((Convert.ToDecimal(tcta.Text) / Convert.ToDecimal(tcqty.Text)))
                    .ToString("#,0.00");
                if (tcboxid.Text == "")
                    tctw.Enabled = false;
                if (tcaa.Text.Length > 15)
                {
                    MessageBox.Show("Amount length is not valid ");
                    tcta.Text = null;
                    tcaa.Text = null;
                    tcta.Enabled = true;
                    tcaa.Enabled = true;          
                    tcta.Focus();
                }
            
            }
        }
        private void tcaa_Leave(object sender, EventArgs e)
        {  
            if (tcqty.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tcqty.Text = null;
                tcqty.Focus();
            }
            if (tcqty.Text != "" && tcaa.Text != "" && tcqty.Text != "0" && tcaa.Enabled == true)
            {

                tcta.Text = Convert.ToDecimal((Convert.ToDecimal(tcaa.Text) * Convert.ToDecimal(tcqty.Text)))
                    .ToString("#,0.00");
                if (tcboxid.Text == "")
                    tcta.Enabled = false;
                if (tcta.Text.Length > 15)
                {
                    MessageBox.Show("Amount length is not valid ");
                    tcta.Text = null;
                    tcaa.Text = null;
                    tcta.Enabled = true;
                    tcaa.Enabled = true;
                    tcaa.Focus();
                }
               
            }
        }

        private void tcbdate_Leave(object sender, EventArgs e)
        {
            DateTime date = tcdate.Value.Date;
            DateTime bdate = tcbdate.Value.Date;
            TimeSpan diff = date.Subtract(bdate);
            int NoOfDays = diff.Days;
            tcaid.Text = NoOfDays.ToString();

        }

        private void tcdate_Leave(object sender, EventArgs e)
        {
            DateTime date = tcdate.Value.Date;
            DateTime bdate = tcbdate.Value.Date;
            TimeSpan diff = date.Subtract(bdate);
            int NoOfDays = diff.Days;
            tcaid.Text = NoOfDays.ToString();
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

            if (label2.Text ==  "Attur")
            {

                Main.Loc = 1;

            }
            else if (label2.Text == "Namakkal")
            {
                Main.Loc = 2;

            }
            Main m= new Main();
            m.Show();
        }

        private void ChicksMortality_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChicksMortality cm = new ChicksMortality();

            cm.Show();
        }

        private void ChicksSold_Click(object sender, EventArgs e)
        {
            this.Hide();

            ChicksSold cs = new ChicksSold();

            cs.Show();
        }

        private void datagridchicks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
         
        tcboxid.Text = (Convert.ToInt32(datagridchicks.CurrentRow.Cells[0].Value)).ToString();
            DateTime CDate = DateTime.Parse(datagridchicks.CurrentRow.Cells[1].Value.ToString());
           tcdate.Value = DateTime.Parse(CDate.ToShortDateString());
           tcbatchno.Text = this.datagridchicks.CurrentRow.Cells[2].Value.ToString();
           tcbreed.Text = this.datagridchicks.CurrentRow.Cells[3].Value.ToString();
           tcqty.Text = (Convert.ToInt32(datagridchicks.CurrentRow.Cells[4].Value)).ToString();
           tctw.Text = (Convert.ToDecimal(datagridchicks.CurrentRow.Cells[5].Value)).ToString();
           tcaw.Text = (Convert.ToDecimal(datagridchicks.CurrentRow.Cells[6].Value)).ToString();
            cbcw.Text = this.datagridchicks.CurrentRow.Cells[7].Value.ToString();
            tcta.Text = this.datagridchicks.CurrentRow.Cells[8].Value.ToString();
           tcaa.Text = (Convert.ToDecimal(datagridchicks.CurrentRow.Cells[9].Value)).ToString();
            DateTime BDate = DateTime.Parse(datagridchicks.CurrentRow.Cells[10].Value.ToString());
           tcbdate.Value = DateTime.Parse(BDate.ToShortDateString());
           tcaid.Text = (Convert.ToInt32(datagridchicks.CurrentRow.Cells[11].Value)).ToString();
           tcnotes.Text = this.datagridchicks.CurrentRow.Cells[12].Value.ToString();
            bcadd.Text = "Update";
            tcqty.Enabled = true;
            tctw.Enabled = true;
            tcaw.Enabled = true;
            tcta.Enabled = true;
            tcaa.Enabled = true;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists("sysLL.txt"))
            {
                File.Delete("sysLL.txt");
            }
            Application.Exit();
        }

        private void datagridchicks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = true;
            btndel.Focus();
        }

    }

}
