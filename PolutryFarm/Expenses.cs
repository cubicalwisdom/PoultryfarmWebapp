using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VOLayer.cs;
using DALayer;


namespace PolutryFarm
{
    public partial class Expenses : Form
    {
        public InfraVO oin1 = new InfraVO();
        public DAL oin2 = new DAL();
        int pageSize = 25;
        int selectedRow;
        private int AttNam = 0;
        private string constring;
        bool tempbool = true;
        bool tempboolAC = true;
        public Expenses()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            ExpenseMain.Enabled = false;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            cbinexpensetype.Items.Add("Labour");
            cbinexpensetype.Items.Add("EB Bill");
            cbinexpensetype.Items.Add("Others");
            cbinexpensetype.SelectedIndex = 0;
            cbinexpensetype.DropDownStyle = ComboBoxStyle.DropDownList;
            tinboxid.Visible = false;
            tinexpensetype.Visible = false;
            tindate.Text = DateTime.Today.ToString();

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


        public BindingList<InfraVO> oinv1 = new BindingList<InfraVO>();
        public DAL oinv2 = null;
        private void Bindgrid(int pageIndex, int temp, int tempAC)
        {
            oinv2 = new DAL();
            oinv1 = oinv2.ViewI(pageSize, pageIndex, temp, tempAC, out int recordCount, AttNam);
            datagridinfra.DataSource = oinv1;
            this.datagridinfra.AllowUserToAddRows = false;
            datagridinfra.Columns["Iid"].HeaderText = "Id";
            datagridinfra.Columns["Iid"].Visible = false;
            datagridinfra.Columns["Idate"].HeaderText = "Date";
            datagridinfra.Columns["Iexpensetype"].HeaderText = "Expense Type";
            datagridinfra.Columns["Iamount"].HeaderText = "Amount";
            datagridinfra.Columns["Inotes"].HeaderText = "Notes";
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
            int id = Convert.ToInt32(datagridinfra.CurrentRow.Cells[0].Value.ToString());

            int i = oinv2.DelI(id, AttNam);

            if (i > 0)
            {

                MessageBox.Show("Successfully Deleted!");
                selectedRow = datagridinfra.CurrentCell.RowIndex;
                datagridinfra.Rows.RemoveAt(selectedRow);           
                btndel.Enabled = false;
                Cleardata();
                if (binadd.Text == "Update")
                {
                    binadd.Text = "Add";
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
        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void binadd_Click(object sender, EventArgs e)
        {
            oin1.Iid = tinboxid.Text == "" ? 0 : Convert.ToInt32(tinboxid.Text);
            oin1.Idate = Convert.ToDateTime(tindate.Text);
            if (tinexpensetype.Visible == false)
            {
                var item1 = this.cbinexpensetype.GetItemText(this.cbinexpensetype.SelectedItem);
                oin1.Iexpensetype = item1;
            }
            else
            {
                oin1.Iexpensetype = tinexpensetype.Text;
            }
            oin1.Iamount = tinamt.Text == "" ? 0 : Convert.ToDecimal(tinamt.Text);
            oin1.Inotes = tinnotes.Text;

            if (oin2.InupI(oin1,Login.UserName, AttNam) == 2)
            {
                MessageBox.Show("Successfully Added!");
                Cleardata();
                Bindgrid(1, 1, 1);

            }
            else if (oin2.InupI(oin1, Login.UserName, AttNam) == 1)
            {

                MessageBox.Show("Successfully Updated!");
                if (binadd.Text == "Update")
                {
                    binadd.Text = "Add";
                }
                Cleardata();
                Bindgrid(1, 1, 1);
            }
        }
        public void Cleardata()
        {
            tindate.Text = DateTime.Today.ToString();
            tinexpensetype.Text = null;
            tinamt.Text = null;
            tinnotes.Text = null;
            tinboxid.Text = null;

        }
        private void binuptcan_Click(object sender, EventArgs e)
        {
            Cleardata();
            if (binadd.Text == "Update")
            {
                binadd.Text = "Add";
            }
        }

       
        private void cbinexpensetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            int item = Convert.ToInt16(this.cbinexpensetype.GetItemText(this.cbinexpensetype.SelectedIndex));
            if (item == 2)
            {
                tinexpensetype.Visible = true;
            }
            else
            {
                tinexpensetype.Visible = false;
            }

        }
        private void datagridinfra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        
      tinboxid.Text = (Convert.ToInt32(datagridinfra.CurrentRow.Cells[0].Value)).ToString();
            DateTime inDate = DateTime.Parse(datagridinfra.CurrentRow.Cells[1].Value.ToString());
         tindate.Value = DateTime.Parse(inDate.ToShortDateString());
            var expense = this.datagridinfra.CurrentRow.Cells[2].Value.ToString();

            if (expense == "Labour" || expense == "EB Bill")
            {
               tinexpensetype.Visible = false;
                cbinexpensetype.Text = this.datagridinfra.CurrentRow.Cells[2].Value.ToString();
            }
            else
            {
               tinexpensetype.Visible = true;
              tinexpensetype.Text = this.datagridinfra.CurrentRow.Cells[2].Value.ToString();
               cbinexpensetype.SelectedIndex = 2;
            }
           tinamt.Text = (Convert.ToDecimal(datagridinfra.CurrentRow.Cells[3].Value)).ToString();
            tinnotes.Text = this.datagridinfra.CurrentRow.Cells[4].Value.ToString();
           binadd.Text = "Update";
           
        }
        private void datagridinfra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btndel.Enabled = true;
            btndel.Focus();

        }

        private void tinamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (Regex.IsMatch(tinamt.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }
    }
}
