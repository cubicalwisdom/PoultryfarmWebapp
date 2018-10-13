using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolutryFarm
{   
    public partial class Main : Form
    {
     
        public static int Loc = 0;
        public Main()
        {
            InitializeComponent();
            label2.Visible = false;
          

            label5.Text = Login.UserName;
            string pri = Login.Priority;

            if (pri == "1")
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }

            else if( pri == "2")
            {
                button1.Enabled = true;
                button2.Enabled = false;
            }

            else if (pri == "3")
            {
                button1.Enabled = false;
                button2.Enabled = true;
            }

            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton4.Visible = false;
            bunifuFlatButton5.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Feed.Visible = false;
            FeedStock.Visible = false;
            Medicine.Visible = false;
            MedicineApp.Visible = false;
         EggPro.Visible = false;
            EggSold.Visible = false;
            if (Loc == 1)
            {
                Loc = 1;
                label2.Visible = true;
                label2.Text = "Attur";
                bunifuFlatButton1.Visible = true;
                bunifuFlatButton2.Visible = true;
                bunifuFlatButton3.Visible = true;
                bunifuFlatButton4.Visible = true;
                bunifuFlatButton5.Visible = true;
                bunifuFlatButton6.Visible = true;
                bunifuFlatButton7.Visible = true;
                bunifuFlatButton8.Visible = true;
                bunifuFlatButton9.Visible = true;
            }
            if (Loc == 2)
            {
                Loc = 2;
                label2.Visible = true;
                label2.Text = "Namakkal";
                bunifuFlatButton1.Visible = true;
                bunifuFlatButton2.Visible = true;
                bunifuFlatButton3.Visible = true;
                bunifuFlatButton4.Visible = true;
                bunifuFlatButton5.Visible = true;
                bunifuFlatButton6.Visible = true;
                bunifuFlatButton7.Visible = true;
                bunifuFlatButton8.Visible = true;
                bunifuFlatButton9.Visible = true;

            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Loc = 1;
            label2.Visible = true;
            label2.Text = "Attur";
            bunifuFlatButton1.Visible = true;
            bunifuFlatButton2.Visible = true;
            bunifuFlatButton3.Visible = true;
            bunifuFlatButton4.Visible = true;
            bunifuFlatButton5.Visible = true;
            bunifuFlatButton6.Visible = true;
            bunifuFlatButton7.Visible = true;
            bunifuFlatButton8.Visible = true;
            bunifuFlatButton9.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Loc = 2;
            label2.Visible = true;
            label2.Text = "Namakkal";
            bunifuFlatButton1.Visible = true;
            bunifuFlatButton2.Visible = true;
            bunifuFlatButton3.Visible = true;
            bunifuFlatButton4.Visible = true;
            bunifuFlatButton5.Visible = true;
            bunifuFlatButton6.Visible = true;
            bunifuFlatButton7.Visible = true;
            bunifuFlatButton8.Visible = true;
            bunifuFlatButton9.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Chicks.Visible = true;
            ChicksMor.Visible = true;
            ChicksSold.Visible = true;
            Feed.Visible = false;
            FeedStock.Visible = false;
            Medicine.Visible = false;
            MedicineApp.Visible = false;
         
            EggPro.Visible = false;
            EggSold.Visible = false;

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Feed.Visible = true;
            FeedStock.Visible = true;
            Medicine.Visible = false;
            MedicineApp.Visible = false;
     
            EggPro.Visible = false;
            EggSold.Visible = false;

        }

        private void Chicks_Click(object sender, EventArgs e)
        {
            this.Hide();
            Chicks c = new Chicks();

            c.Show();
        }

  
      private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Feed.Visible = false;
            FeedStock.Visible = false;
            Medicine.Visible = true;
            MedicineApp.Visible = true;    
            EggPro.Visible = false;
            EggSold.Visible = false;

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Feed.Visible = false;
            FeedStock.Visible = false;
            Medicine.Visible = false;
            MedicineApp.Visible = false;
         
            EggPro.Visible = false;
            EggSold.Visible = false;


            this.Hide();
            Expenses ex = new Expenses();

            ex.Show();

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Feed.Visible = false;
            FeedStock.Visible = false;
            Medicine.Visible = false;
            MedicineApp.Visible = false;
          
            EggPro.Visible = true;
            EggSold.Visible = true;
        }

        private void ChicksMor_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChicksMortality cm = new ChicksMortality();

            cm.Show();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Feed.Visible = false;
            FeedStock.Visible = false;
            Medicine.Visible = false;
            MedicineApp.Visible = false;         
            EggPro.Visible = false;
            EggSold.Visible = false;
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Feed.Visible = false;
            FeedStock.Visible = false;
            Medicine.Visible = false;
            MedicineApp.Visible = false;         
            EggPro.Visible = false;
            EggSold.Visible = false;
            this.Hide();
            OverView o = new OverView();
            o.Show();

        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Feed.Visible = false;
            FeedStock.Visible = false;
            Medicine.Visible = false;
            MedicineApp.Visible = false;         
            EggPro.Visible = false;
            EggSold.Visible = false;

        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            Chicks.Visible = false;
            ChicksMor.Visible = false;
            ChicksSold.Visible = false;
            Feed.Visible = false;
            FeedStock.Visible = false;
            Medicine.Visible = false;
            MedicineApp.Visible = false;       
            EggPro.Visible = false;
            EggSold.Visible = false;
        }

        private void ChicksSold_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChicksSold cs = new ChicksSold();
            cs.Show();
        }

        private void Feed_Click(object sender, EventArgs e)
        {
            this.Hide();
            Feed f = new Feed();
            f.Show();
        }

        private void FeedStock_Click(object sender, EventArgs e)
        {
            this.Hide();
            FeedStock fs = new FeedStock();
            fs.Show();
        }

        private void Medicine_Click(object sender, EventArgs e)
        {
            this.Hide();
            Medicine m = new Medicine();
            m.Show();
        }

        private void MedicineApp_Click(object sender, EventArgs e)
        {
            this.Hide();
            MedicineApplied ma = new MedicineApplied();
            ma.Show();
        }

        private void EggPro_Click(object sender, EventArgs e)
        {
            this.Hide();
            EggProduction ep = new EggProduction();
            ep.Show();

        }

        private void EggSold_Click(object sender, EventArgs e)
        {
            this.Hide();
            EggSold es = new EggSold();
            es.Show();
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

            this.Hide();
            Changepw c = new Changepw();
            c.Show();

        }

      

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.IO.File.Exists("sysLL.txt"))
            {
                System.IO.File.Delete("sysLL.txt");
            }
            Application.Exit();
        }
    }
}
