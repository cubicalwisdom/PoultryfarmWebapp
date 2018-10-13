using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DALayer;
using VOLayer.cs;

namespace PolutryFarm
{
    public partial class EggProduction : Form
    {
        int pageSize = 25;
        int selectedRow;
        bool tempbool = true;
        bool tempboolAC = true;
        private int AttNam = 0;
        private string constring;
        public EggVO oe1 = new EggVO();
        public DAL oe2 = new DAL();

        public EggProduction()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            EggProductionmain.Enabled = false;
            teboxid.Visible = false;
            teinstock.Visible = false;
            leinstock.Visible = false;
            tedate.Text = DateTime.Today.ToString();
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
            tecbatchno.AutoCompleteCustomSource = MyCollectionc;
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
        public BindingList<EggVO> oev1 = new BindingList<EggVO>();
        public DAL oev2 = null;

        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            oev2 = new DAL();
            oev1 = oev2.ViewE(pageSize, pageIndex, temp, tempAC, out int recordCount, AttNam);
            datagridegg.DataSource = oev1;
            this.datagridegg.AllowUserToAddRows = false;
            datagridegg.Columns["Eid"].HeaderText = "Id";
            datagridegg.Columns["Eid"].Visible = false;
            datagridegg.Columns["Edate"].HeaderText = "Date";
            datagridegg.Columns["Ecbatchno"].HeaderText = "Chicks Batct No";
            datagridegg.Columns["Eqty"].HeaderText = "QTY";
            datagridegg.Columns["Einstock"].HeaderText = "Egg in Stock";
            datagridegg.Columns["Enotes"].HeaderText = "Notes";
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
            int id = Convert.ToInt32(datagridegg.CurrentRow.Cells[0].Value.ToString());

            int i = oev2.DelE(id, AttNam);

            if (i > 0)
            {

                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagridegg.CurrentCell.RowIndex;
                datagridegg.Rows.RemoveAt(selectedRow);
                Cleardata();
                btndel.Enabled = false;
                if (beadd.Text == "Update")
                {
                    beadd.Text = "Add";
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

        private void beadd_Click(object sender, EventArgs e)
        {
            {

                if (tecbatchno.Text == "" || teqty.Text == "")
                {
                    if (tecbatchno.Text == "")
                    {
                        MessageBox.Show("Chicks batch no is mandatory");
                        tecbatchno.Focus();
                    }
                    else if (teqty.Text == "")
                    {
                        MessageBox.Show("Quantity is mandatory");
                        teqty.Focus();
                    }

                }
                else
                {
                    oe1.Eid = teboxid.Text == "" ? 0 : Convert.ToInt32(teboxid.Text);
                    oe1.Edate = Convert.ToDateTime(tedate.Text);
                    oe1.Ecbatchno = tecbatchno.Text;
                    oe1.Eqty = Convert.ToInt32(teqty.Text);
                    oe1.Enotes = tenotes.Text;

                    if (oe2.InupE(oe1, Login.UserName, out int Einstock, AttNam) == 2)
                    {
                        teinstock.Visible = true;
                        leinstock.Visible = true;
                        teinstock.Text = Convert.ToInt32(Einstock).ToString();
                        MessageBox.Show("Successfully Added!");
                        Cleardata();
                        Bindgrid(1, 1, 1);
                       
                    }
                    else if (oe2.InupE(oe1, Login.UserName, out int Einstock1, AttNam) == 1)
                    {

                        teinstock.Visible = true;
                        leinstock.Visible = true;
                        teinstock.Text = Convert.ToInt32(Einstock1).ToString();
                        MessageBox.Show("Successfully Updated!");
                        Cleardata();
                        Bindgrid(1, 1, 1);
                        if (beadd.Text == "Update")
                        {
                            beadd.Text = "Add";
                        }


                    }
                }
            }

        }

        public void Cleardata()
        {
            teboxid.Text = null;
            tedate.Text = DateTime.Today.ToString();
            tecbatchno.Text = null;
            teqty.Text = null;
            tenotes.Text = null;
            teinstock.Visible = false;
            leinstock.Visible = false;
          


        }
        private void beuptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (beadd.Text == "Update")
            {
                beadd.Text = "Add";
            }
        }

        private void tecbatchno_Leave(object sender, EventArgs e)
        {
            if (tecbatchno.Text != "")
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
                comm.Parameters.AddWithValue("@Cbatchno", tecbatchno.Text);
                int cbatch = Convert.ToInt16(comm.ExecuteScalar());
                if (cbatch == 0)
                {
                    MessageBox.Show("Please select the valid Chicks batch no from suggestion");
                    tecbatchno.Text = null;
                    tecbatchno.Focus();
                }
            }
        }

        private void teqty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void datagridegg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = true;
            btndel.Focus();
        }

   

        private void datagridegg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            teboxid.Text = (Convert.ToInt32(datagridegg.CurrentRow.Cells[0].Value)).ToString();
            DateTime EDate = DateTime.Parse(datagridegg.CurrentRow.Cells[1].Value.ToString());
            tedate.Value = DateTime.Parse(EDate.ToShortDateString());
            tecbatchno.Text = this.datagridegg.CurrentRow.Cells[2].Value.ToString();
            teqty.Text = this.datagridegg.CurrentRow.Cells[3].Value.ToString();
            tenotes.Text = this.datagridegg.CurrentRow.Cells[5].Value.ToString();
            beadd.Text = "Update";

        }
    }
}
