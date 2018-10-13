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
using System.Text.RegularExpressions;

namespace PolutryFarm
{
    public partial class Medicine : Form
    {
        public MedicineVO om1 = new MedicineVO();
        public DAL om2 = new DAL();
        int pageSize = 25;
        int selectedRow;
        bool tempbool = true;
        bool tempboolAC = true;
        private int AttNam = 0;
        private string constring;
        public Medicine()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            MedicineMain.Enabled = false;
            this.SetStyle(ControlStyles.ResizeRedraw, true);       
            tmboxid.Visible = false;
            tmdate.Text = DateTime.Today.ToString();
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
            comm.CommandText = "Medpre";
            comm.Parameters.Add("@Mp", SqlDbType.NVarChar, 50);
            comm.Parameters["@Mp"].Direction = ParameterDirection.Output;
            comm.ExecuteNonQuery();
            string Prechicks = comm.Parameters["@Mp"].Value.ToString();

            if (Prechicks == "")
            {
                Medpre.Text = "00";
            }
            else
            {
                Medpre.Text = Prechicks;
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



        private void btndel_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(datagridmedicine.CurrentRow.Cells[0].Value.ToString());

            int i = omv2.DelM(id, AttNam);

            if (i > 0)
            {

                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagridmedicine.CurrentCell.RowIndex;
                datagridmedicine.Rows.RemoveAt(selectedRow);
                btndel.Enabled = false;
                Precheck();
                Cleardata();
                if (bmadd.Text == "Update")
                {
                    bmadd.Text = "Add";
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
        public BindingList<MedicineVO> omv1 = new BindingList<MedicineVO>();
        public DAL omv2 = null;
        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            omv2 = new DAL();
            omv1 = omv2.ViewM(pageSize, pageIndex, temp, tempAC, out int recordCount, AttNam);
            datagridmedicine.DataSource = omv1;
            this.datagridmedicine.AllowUserToAddRows = false;

            datagridmedicine.Columns["Mid"].HeaderText = "Id";
            datagridmedicine.Columns["Mid"].Visible = false;
            datagridmedicine.Columns["Mdate"].HeaderText = "Date";
            datagridmedicine.Columns["Mpurno"].HeaderText = "Purchase No";
            datagridmedicine.Columns["Mbrand"].HeaderText = "Name";
            datagridmedicine.Columns["Msupplier"].HeaderText = "Supplier";
            datagridmedicine.Columns["Mnature"].HeaderText = "Nature";
            datagridmedicine.Columns["Mqty"].HeaderText = "Qty";
            datagridmedicine.Columns["Mamt"].HeaderText = "Amount";
            datagridmedicine.Columns["Mnotes"].HeaderText = "Notes";
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
        private void bmadd_Click(object sender, EventArgs e)
        {

            if (tmpurno.Text == "")
            {
                MessageBox.Show("Purchase no is mandatory");
                tmpurno.Focus();

            }
            else
            {
                om1.Mid = tmboxid.Text == "" ? 0 : Convert.ToInt32(tmboxid.Text);
                om1.Mdate = Convert.ToDateTime(tmdate.Text);
                om1.Mpurno = tmpurno.Text;
                om1.Mbrand = tmbrand.Text;
                om1.Msupplier = tmsupplier.Text;
                om1.Mnature = tmnature.Text;
                om1.Mqty = tmqty.Text == "" ? 0 : Convert.ToInt32(tmqty.Text);
                om1.Mamt = tma.Text == "" ? 0 : Convert.ToDecimal(tma.Text);
                om1.Mnotes = tmnotes.Text;

                if (om2.InupM(om1, Login.UserName, AttNam) == 2)
                {

                    MessageBox.Show("Successfully Added!");
                    Cleardata();                  
                    Precheck();
                    Bindgrid(1, 1, 1);

                }
                else if (om2.InupM(om1, Login.UserName, AttNam) == 1)
                {
                    MessageBox.Show("Successfully Updated!");
                    Cleardata();
                    if (bmadd.Text == "Update")
                    {
                        bmadd.Text = "Add";
                    }
                    Precheck();
                    Bindgrid(1, 1, 1);
                }
            }

        }
        private void bmuptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (bmadd.Text == "Update")
            {
                bmadd.Text = "Add";
            }
        }
        public void Cleardata()
        {
            tmdate.Text = DateTime.Today.ToString();
            tmpurno.Text = null;
            tmbrand.Text = null;
            tmsupplier.Text = null;
            tmnature.Text = null;
            tmqty.Text = null;
            tma.Text = null;
            tmnotes.Text = null;
            tmboxid.Text = null;
        }

        private void tmqty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tma_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tma.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void tmpurno_Leave(object sender, EventArgs e)
        {
            if (tmpurno.Text.Length < 2 && tmpurno.Text != "")
            {

                MessageBox.Show("Purchase no should not be less than 2");
                tmpurno.Text = null;
                tmpurno.Focus();
            }
            else
            {
                if (tmboxid.Text == "")
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
                    comm.CommandText = "Mpurno";
                    comm.Parameters.AddWithValue("@Mpurno", tmpurno.Text);
                    int res = Convert.ToInt16(comm.ExecuteScalar());
                    if (res == 0)
                    {
                        MessageBox.Show("This purchase no is already present .Please enter the different one");
                        tmpurno.Text = null;
                        tmpurno.Focus();
                    }
                }

                else if (tmboxid.Text != "")
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
                    comm.CommandText = "Mpurno1";
                    comm.Parameters.AddWithValue("@Mid1", tmboxid.Text);
                    comm.Parameters.AddWithValue("@Mpurno1", tmpurno.Text);
                    int res1 = Convert.ToInt16(comm.ExecuteScalar());
                    if (res1 == 0)
                    {
                        MessageBox.Show("This purchase no is already present .Please enter the different one");
                        tmpurno.Text = null;
                        tmpurno.Focus();
                    }

                }
            }
         
        }      
        private void datagridmedicine_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            tmboxid.Text = (Convert.ToInt32(datagridmedicine.CurrentRow.Cells[0].Value)).ToString();
            DateTime MDate = DateTime.Parse(datagridmedicine.CurrentRow.Cells[1].Value.ToString());
            tmdate.Value = DateTime.Parse(MDate.ToShortDateString());
            tmpurno.Text = this.datagridmedicine.CurrentRow.Cells[2].Value.ToString();
            tmbrand.Text = this.datagridmedicine.CurrentRow.Cells[3].Value.ToString();
            tmsupplier.Text = this.datagridmedicine.CurrentRow.Cells[4].Value.ToString();
            tmnature.Text = this.datagridmedicine.CurrentRow.Cells[5].Value.ToString();
            tmqty.Text = (Convert.ToInt32(datagridmedicine.CurrentRow.Cells[6].Value)).ToString();
            tma.Text = (Convert.ToDecimal(datagridmedicine.CurrentRow.Cells[7].Value)).ToString();
            tmnotes.Text = this.datagridmedicine.CurrentRow.Cells[8].Value.ToString();
            bmadd.Text = "Update";
        }

        private void datagridmedicine_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            btndel.Enabled = true;
            btndel.Focus();
        }
    }
}
