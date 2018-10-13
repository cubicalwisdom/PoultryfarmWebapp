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
    public partial class OverView : Form
    {
        private int AttNam = 0;
        private string constring;
        public OverView()
        {
            InitializeComponent();
            AttNam = Main.Loc;
            label1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            cb2.Visible = false;
            cm2.Visible = false;
            cs2.Visible = false;

            label7.Visible = false;
            label12.Visible = false;
            ep2.Visible = false;

            label5.Visible = false;
            label6.Visible = false;
            label14.Visible = false;
            label15.Visible = true;
            fb2.Visible = false;
            fs2.Visible = false;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            if (Main.Loc == 1)
            {
                label2.Text = "Attur";

            }
            else if (Main.Loc == 2)
            {
                label2.Text = "Namakkal";
            }
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
            SqlCommand cmdchicks = new SqlCommand("SELECT Cbatchno FROM Chicks", connection);
            SqlDataReader readerc = cmdchicks.ExecuteReader();
           
            while (readerc.Read())
            {
                for (int i = 0; i < readerc.FieldCount; i++)
                {
                    tcsbatchno.Items.Add(readerc.GetString(i));
                    tcbatchno1.Items.Add(readerc.GetString(i));

                }
            }
        
            tcsbatchno.Text = "-Batch No-";
         
            tcbatchno1.Text = "-Batch No-";
            readerc.Close();
            connection.Close();
            connection.Open();
            SqlCommand cmdfeed = new SqlCommand("SELECT Fpurno FROM  Feed ", connection);
            SqlDataReader readerf = cmdfeed.ExecuteReader();

            while (readerf.Read())
            {
                for (int i = 0; i < readerf.FieldCount; i++)
                {
                    tfpurno.Items.Add(readerf.GetString(i));
                 

                }
            }
 
            tfpurno.Text = "-Purchase No-";
            readerf.Close();
            connection.Close();

          
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "Overview";
            comm.Parameters.Add("@C", SqlDbType.VarChar, 10);
            comm.Parameters["@C"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@E", SqlDbType.VarChar, 10);
            comm.Parameters["@E"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@F", SqlDbType.VarChar, 10);
            comm.Parameters["@F"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@CB", SqlDbType.VarChar, 10);
            comm.Parameters["@CB"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@CM", SqlDbType.VarChar, 10);
            comm.Parameters["@CM"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@CS", SqlDbType.VarChar, 10);
            comm.Parameters["@CS"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@FB", SqlDbType.VarChar, 10);
            comm.Parameters["@FB"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@FS", SqlDbType.VarChar, 10);
            comm.Parameters["@FS"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@EP", SqlDbType.VarChar, 10);
            comm.Parameters["@EP"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@ES", SqlDbType.VarChar, 10);
            comm.Parameters["@ES"].Direction = ParameterDirection.Output;

            comm.ExecuteNonQuery();
            cfull.Text = Convert.ToString(comm.Parameters["@C"].Value);          
            efull.Text = Convert.ToString(comm.Parameters["@E"].Value);
            ffull.Text = Convert.ToString(comm.Parameters["@F"].Value);
            cb1.Text = Convert.ToString(comm.Parameters["@CB"].Value);
            cm1.Text = Convert.ToString(comm.Parameters["@CM"].Value);
            cs1.Text = Convert.ToString(comm.Parameters["@CS"].Value);
            fb1.Text = Convert.ToString(comm.Parameters["@FB"].Value);
            fs1.Text = Convert.ToString(comm.Parameters["@FS"].Value);
            ep1.Text = Convert.ToString(comm.Parameters["@EP"].Value);
            es1.Text = Convert.ToString(comm.Parameters["@ES"].Value);

            connection.Close();
            if (cfull.Text == "")
            {
                cfull.Text = "0";
            }
            if (efull.Text == "")
            {
                efull.Text = "0";
            }
            if (ffull.Text == "")
            {
                ffull.Text = "0";
            }
            if (cb1.Text == "")
            {
                cb1.Text = "0";
            }
            if (cm1.Text == "")
            {
                cm1.Text = "0";
            }
            if (cs1.Text == "")
            {
                cs1.Text = "0";
            }

            if (fb1.Text == "")
            {
                fb1.Text = "0";
            }

            if (fs1.Text == "")
            {
                fs1.Text = "0";
            }

            if (ep1.Text == "")
            {
                ep1.Text = "0";
            }

            if (es1.Text == "")
            {
                es1.Text = "0";
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

        private void tcsbatchno_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            cb2.Visible = true;
            cm2.Visible = true;
            cs2.Visible = true;

      
            string item = tcsbatchno.SelectedItem.ToString();
      
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
            comm.CommandText = "OverviewChicks";
            comm.Parameters.AddWithValue("@Cbnum  ", item);

            comm.Parameters.Add("@CB", SqlDbType.VarChar, 10);
            comm.Parameters["@CB"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@CM", SqlDbType.VarChar, 10);
            comm.Parameters["@CM"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@CS", SqlDbType.VarChar, 10);
            comm.Parameters["@CS"].Direction = ParameterDirection.Output;

            comm.ExecuteNonQuery();
            cb2.Text = Convert.ToString(comm.Parameters["@CB"].Value);
            cm2.Text = Convert.ToString(comm.Parameters["@CM"].Value);
            cs2.Text = Convert.ToString(comm.Parameters["@CS"].Value);
            if (cb2.Text == "")
            {
                cb2.Text = "0";
            }
            if (cm2.Text == "")
            {
                cm2.Text = "0";
            }
            if (cs2.Text == "")
            {
                cs2.Text = "0";
            }

            connection.Close();

        }

        private void tfpurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Visible = true;
            label6.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            fb2.Visible = true;
            fs2.Visible = true;
            string item = tcsbatchno.SelectedItem.ToString();

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
            comm.CommandText = "OverviewFeed";
            comm.Parameters.AddWithValue("@Fpnum", item);

            comm.Parameters.Add("@FB", SqlDbType.VarChar, 10);
            comm.Parameters["@FB"].Direction = ParameterDirection.Output;
            comm.Parameters.Add("@FS", SqlDbType.VarChar, 10);
            comm.Parameters["@FS"].Direction = ParameterDirection.Output;


            comm.ExecuteNonQuery();
            fb2.Text = Convert.ToString(comm.Parameters["@FB"].Value);
            fs2.Text = Convert.ToString(comm.Parameters["@FS"].Value);

            if (fb2.Text == "")
            {
                fb2.Text = "0";
            }
            if (fs2.Text == "")
            {
                fs2.Text = "0";
            }

            connection.Close();
        }

        private void tcbatchno1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Visible = true;
            label12.Visible = true;
            ep2.Visible = true;
            string item = tcsbatchno.SelectedItem.ToString();

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
            comm.CommandText = "OverviewEgg";
            comm.Parameters.AddWithValue("@Ebnum", item);

            comm.Parameters.Add("@EP", SqlDbType.VarChar, 10);
            comm.Parameters["@EP"].Direction = ParameterDirection.Output;

            comm.ExecuteNonQuery();
            ep2.Text = Convert.ToString(comm.Parameters["@EP"].Value);


            if (ep2.Text == "")
            {
                ep2.Text = "0";
            }
 
            connection.Close();
        }


        private void label8_Click(object sender, EventArgs e)
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

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
