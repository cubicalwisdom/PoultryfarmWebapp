using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DALayer;
using VOLayer.cs;

namespace PolutryFarm
{
    public partial class Feed : Form
    {
        int pageSize = 25;
        int selectedRow;
        bool tempbool = true;
        bool tempboolAC = true;
        private int AttNam = 0;
        private string constring;
        public FeedVO of1 = new FeedVO();
        public DAL of2 = new DAL();
        public Feed()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            FeedMain.Enabled = false;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            cbfw.Items.Add("KG");
            cbfw.Items.Add("Ton");
            cbfw.SelectedIndex = 0;
            cbfw.DropDownStyle = ComboBoxStyle.DropDownList;
            tfboxid.Visible = false;
            tfdate.Text = DateTime.Today.ToString();
            btndel.Enabled = false;
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
        }

        public void Precheck()
        {
            if (AttNam == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNam == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "Feedpre";
            comm.Parameters.Add("@Fp", SqlDbType.NVarChar, 50);
            comm.Parameters["@Fp"].Direction = ParameterDirection.Output;
            comm.ExecuteNonQuery();
            string Prefeed = comm.Parameters["@Fp"].Value.ToString();

            if (Prefeed == "")
            {
                Feedpre.Text = "00";
            }
            else
            {
                Feedpre.Text = Prefeed;
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
        public BindingList<FeedVO> ofv1 = new BindingList<FeedVO>();
        public DAL ofv2 = null;
        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            ofv2 = new DAL();
            ofv1 = ofv2.ViewF(pageSize, pageIndex, temp, tempAC, out int recordCount, AttNam);
            datagridfeed.DataSource = ofv1;
            this.datagridfeed.AllowUserToAddRows = false;
            datagridfeed.Columns["Fid"].HeaderText = "Id";
            datagridfeed.Columns["Fid"].Visible = false;
            datagridfeed.Columns["Fdate"].HeaderText = "Date";
            datagridfeed.Columns["Fpurno"].HeaderText = "Purchase No";
            datagridfeed.Columns["Fbrand"].HeaderText = "Brand";
            datagridfeed.Columns["Fnob"].HeaderText = "No of Bags";
            datagridfeed.Columns["Fwt"].HeaderText = "Tot Weight";
            datagridfeed.Columns["Fwav"].HeaderText = "Avg Weight";
            datagridfeed.Columns["Fwmea"].HeaderText = "Measure";        
            datagridfeed.Columns["Ftotamt"].HeaderText = "Tot Amount";
            datagridfeed.Columns["Favgamt"].HeaderText = "Price/Bag";
            datagridfeed.Columns["Fnotes"].HeaderText = "Notes";

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
            int id = Convert.ToInt32(datagridfeed.CurrentRow.Cells[0].Value.ToString());

            int i = ofv2.DelF(id, AttNam);

            if (i > 0)
            {

                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagridfeed.CurrentCell.RowIndex;

                datagridfeed.Rows.RemoveAt(selectedRow);
                btndel.Enabled = false;
                Cleardata();
                Precheck();
                if (bfadd.Text == "Update")
                {
                    bfadd.Text = "Add";
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
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.IO.File.Exists("sysLL.txt"))
            {
                System.IO.File.Delete("sysLL.txt");
            }
            Application.Exit();
        }

        private void bfadd_Click(object sender, EventArgs e)
        {
            if (tfpurno.Text == "")
            {
                MessageBox.Show("Purchase no is mandatory");
                tfpurno.Focus();
            }        
            else
            {
                of1.Fid = tfboxid.Text == "" ? 0 : Convert.ToInt32(tfboxid.Text);
                of1.Fdate = Convert.ToDateTime(tfdate.Text);
                of1.Fpurno = tfpurno.Text;
                of1.Fbrand = tfbrand.Text;
                of1.Fnob = tfnob.Text == "" ? 0 : Convert.ToInt32(tfnob.Text);

                of1.Fwt= tftw.Text == "" ? 0 : Convert.ToDecimal(tftw.Text);
                var item = this.cbfw.GetItemText(this.cbfw.SelectedItem);
                of1.Fwmea = item;
                of1.Fwav = tfaw.Text == "" ? 0 : Convert.ToDecimal(tfaw.Text);
                of1.Ftotamt = tfta.Text == "" ? 0 : Convert.ToDecimal(tfta.Text);
                of1.Favgamt = tfaa.Text == "" ? 0 : Convert.ToDecimal(tfaa.Text);              
                of1.Fnotes = tfnotes.Text;

                if (of2.InupF(of1, Login.UserName, AttNam) == 2)
                {
                    MessageBox.Show("Successfully Added!");
                    Cleardata();
                    Precheck();
                    Bindgrid(1, 1, 1);
                }
                else if (of2.InupF(of1, Login.UserName, AttNam) == 1)
                {

                    MessageBox.Show("Successfully Updated!");
                    Cleardata();
                    Precheck();
                    if (bfadd.Text == "Update")
                    {
                        bfadd.Text = "Add";
                    }
                    Bindgrid(1, 1, 1);
                }
            }

        }
        public void Cleardata()
        {
            tfdate.Text = DateTime.Today.ToString();
            tfbrand.Text = null;
            tfpurno.Text = null;
            tfnob.Text = null;
            tftw.Text = null;
            tfaw.Text = null;
            tfta.Text = null;
            tfaa.Text = null;
            tfnotes.Text = null;
            tfnob.Enabled = true;
            tftw.Enabled = true;
            tfaw.Enabled = true;
            tfta.Enabled = true;
            tfaa.Enabled = true;
            tfboxid.Text = null;
        }
        private void bfuptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (bfadd.Text == "Update")
            {
                bfadd.Text = "Add";
            }
        }
        private void tftw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tftw.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void tfaw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tfaw.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void tfta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tfta.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void tfpurno_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tfnob_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tfaa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tfaa.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }

        }

        private void tfpurno_Leave(object sender, EventArgs e)
        {
            if (tfpurno.Text.Length < 2 && tfpurno.Text != "")
            {

                MessageBox.Show("Purchase no should not be less than 2");
                tfpurno.Text = null;
                tfpurno.Focus();
            }
            if (tfboxid.Text == "")
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
                comm.CommandText = "Fpurno";
                comm.Parameters.AddWithValue("@Fpurno", tfpurno.Text);
                int res = Convert.ToInt16(comm.ExecuteScalar());
                if (res == 0)
                {
                    MessageBox.Show("This batch no is already present .Please enter the different one");
                    tfpurno.Text = null;
                    tfpurno.Focus();
                }
            }

            else if (tfboxid.Text != "")
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
                comm.CommandText = "Fpurno1";
                comm.Parameters.AddWithValue("@Fid1", tfboxid.Text);
                comm.Parameters.AddWithValue("@Fpurno1", tfpurno.Text);
                int res1 = Convert.ToInt16(comm.ExecuteScalar());
                if (res1 == 0)
                {
                    MessageBox.Show("This purchase no  is already present .Please enter the different one");
                    tfpurno.Text = null;
                    tfpurno.Focus();
                }

            }
        }

        private void tfnob_Leave(object sender, EventArgs e)
            {
            if (tfboxid.Text != "")
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
                comm.CommandText = "FQtycheck";
                comm.Parameters.AddWithValue("@Fqty1", tfnob.Text);
                comm.Parameters.AddWithValue("@Fpurno1", tfpurno.Text);
                int res1 = Convert.ToInt16(comm.ExecuteScalar());
                if (res1 == 0)
                {
                    MessageBox.Show("Bag count should not be less than the stock qty ");
                    tfnob.Text = null;
                    tfnob.Focus();
                }
            }

            if (tfnob.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tfnob.Text = null;
                tfnob.Focus();
            }

            if (tfnob.Text != "" && tfnob.Text != "0")
            {
             
              
                    if (tfta.Text != "" && tfta.Enabled == true)
                    {
                        tfaa.Text = Convert.ToDecimal((Convert.ToDecimal(tfta.Text) / Convert.ToDecimal(tfnob.Text)))
                            .ToString("#,0.00");
                        if (tfboxid.Text == "")
                            tfaa.Enabled = false;
                        if (tfaa.Text.Length > 15)
                        {
                            MessageBox.Show("Amount length is not valid ");
                            tfta.Text = null;
                            tfaa.Text = null;
                            tfta.Enabled = true;
                            tfaa.Enabled = true;
                            tfta.Focus();
                        }


                    }
                    else if (tfaa.Text != "" && tfaa.Enabled == true)
                    {
                        tfta.Text = Convert.ToDecimal((Convert.ToDecimal(tfaa.Text) * Convert.ToDecimal(tfnob.Text)))
                            .ToString("#,0.00");
                        if (tfboxid.Text == "")
                            tfta.Enabled = false;
                        if (tfta.Text.Length > 15)
                        {
                            MessageBox.Show("Amount length is not valid ");
                            tfta.Text = null;
                            tfaa.Text = null;
                            tfta.Enabled = true;
                            tfaa.Enabled = true;
                            tfta.Focus();
                        }

                }


                if (tftw.Text != "" && tftw.Enabled == true)
                {
                    tfaw.Text = Convert.ToDecimal((Convert.ToDecimal(tftw.Text) / Convert.ToDecimal(tfnob.Text)))
                        .ToString("#,0.00");
                    if (tfboxid.Text == "")
                        tfaw.Enabled = false;
                    if (tfaw.Text.Length > 15)
                    {
                        MessageBox.Show("Weight length is not valid ");
                        tftw.Text = null;
                        tfaw.Text = null;
                        tftw.Enabled = true;
                        tfaw.Enabled = true;
                        tftw.Focus();
                    }


                }
                else if (tfaw.Text != "" && tfaw.Enabled == true)
                {
                    tftw.Text = Convert.ToDecimal((Convert.ToDecimal(tfaw.Text) * Convert.ToDecimal(tfnob.Text)))
                        .ToString("#,0.00");
                    if (tfboxid.Text == "")
                        tftw.Enabled = false;
                    if (tftw.Text.Length > 15)
                    {
                        MessageBox.Show("Weight length is not valid ");
                        tftw.Text = null;
                        tfaw.Text = null;
                        tftw.Enabled = true;
                        tfaw.Enabled = true;
                        tftw.Focus();
                    }

                }

            }
           
        }

        private void tftw_Leave(object sender, EventArgs e)
        {
            if (tfnob.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tfnob.Text = null;
                tfnob.Focus();
            }
            if (tfnob.Text != "" && tftw.Text != "" && tfnob.Text != "0" && tftw.Enabled == true)
            {

                tfaw.Text = Convert.ToDecimal((Convert.ToDecimal(tftw.Text) / Convert.ToDecimal(tfnob.Text)))
                    .ToString("#,0.00");
                if (tfboxid.Text == "")
                    tfaw.Enabled = false;
                if (tfaw.Text.Length > 15)
                {
                    MessageBox.Show("Weight length is not valid ");
                    tftw.Text = null;
                    tfaw.Text = null;
                    tftw.Enabled = true;
                    tfaw.Enabled = true;
                    tftw.Focus();
                }
               
            }

        }
        private void tfaw_Leave(object sender, EventArgs e)
        {
            if (tfnob.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tfnob.Text = null;
                tfnob.Focus();
            }
         
            if (tfnob.Text != "" && tfaw.Text != "" && tfnob.Text != "0" && tfaw.Enabled == true)
            {

                tftw.Text = Convert.ToDecimal((Convert.ToDecimal(tfaw.Text) * Convert.ToDecimal(tfnob.Text)))
                    .ToString("#,0.00");
                if (tfboxid.Text == "")
                    tftw.Enabled = false;
                if (tftw.Text.Length > 15)
                {
                    MessageBox.Show("Weight length is not valid ");
                    tftw.Text = null;
                    tfaw.Text = null;
                    tftw.Enabled = true;
                    tfaw.Enabled = true;
                    tfaw.Focus();
                }
               
            }
        }
        private void tfta_Leave(object sender, EventArgs e)
        {

            if (tfnob.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tfnob.Text = null;
                tfnob.Focus();
            }
            if (tfnob.Text != "" && tfta.Text != "" && tfnob.Text != "0" && tfta.Enabled == true)
            {

                tfaa.Text = Convert.ToDecimal((Convert.ToDecimal(tfta.Text) / Convert.ToDecimal(tfnob.Text)))
                    .ToString("#,0.00");
                if (tfboxid.Text == "")
                    tfaa.Enabled = false;
                if (tfaa.Text.Length > 15)
                {
                    MessageBox.Show("Amount length is not valid ");
                    tfta.Text = null;
                    tfaa.Text = null;
                    tfta.Enabled = true;
                    tfaa.Enabled = true;
                    tfta.Focus();
                }
               
            }
        }
        private void tfaa_Leave(object sender, EventArgs e)
        {
          
            if (tfnob.Text == "0")
            {
                MessageBox.Show("Please Enter Valid QTY");
                tfnob.Text = null;
                tfnob.Focus();
            }
            if (tfnob.Text != "" && tfaa.Text != "" && tfnob.Text != "0" && tfaa.Enabled == true)
            {

                tfta.Text = Convert.ToDecimal((Convert.ToDecimal(tfaa.Text) * Convert.ToDecimal(tfnob.Text)))
                    .ToString("#,0.00");
                if (tfboxid.Text == "")
                    tfta.Enabled = false;
                if (tfta.Text.Length > 15)
                {
                    MessageBox.Show("Amount length is not valid ");
                    tfta.Text = null;
                    tfaa.Text = null;
                    tfta.Enabled = true;
                    tfaa.Enabled = true;
                    tfaa.Focus();
                }
             
            }
        }
        private void datagridfeed_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tfboxid.Text = (Convert.ToInt32(datagridfeed.CurrentRow.Cells[0].Value)).ToString();
            DateTime CDate = DateTime.Parse(datagridfeed.CurrentRow.Cells[1].Value.ToString());
            tfdate.Value = DateTime.Parse(CDate.ToShortDateString());
            tfpurno.Text = this.datagridfeed.CurrentRow.Cells[2].Value.ToString();
            tfbrand.Text = this.datagridfeed.CurrentRow.Cells[3].Value.ToString();
            tfnob.Text = (Convert.ToInt32(datagridfeed.CurrentRow.Cells[4].Value)).ToString();
            tftw.Text = (Convert.ToDecimal(datagridfeed.CurrentRow.Cells[5].Value)).ToString();
            tfaw.Text = (Convert.ToDecimal(datagridfeed.CurrentRow.Cells[6].Value)).ToString();
            cbfw.Text = this.datagridfeed.CurrentRow.Cells[7].Value.ToString();
            tfta.Text = (Convert.ToDecimal(datagridfeed.CurrentRow.Cells[8].Value)).ToString();
            tfaa.Text = (Convert.ToDecimal(datagridfeed.CurrentRow.Cells[9].Value)).ToString();
            tfnotes.Text = this.datagridfeed.CurrentRow.Cells[10].Value.ToString();
            bfadd.Text = "Update";
            tfnob.Enabled = true;
            tftw.Enabled = true;
            tfaw.Enabled = true;
            tfta.Enabled = true;
            tfaa.Enabled = true;

        }
        private void datagridfeed_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = true;
            btndel.Focus();
        }
    }
}
