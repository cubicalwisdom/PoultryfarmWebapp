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

namespace PolutryFarm
{
    public partial class Changepw : Form
    {
        public Changepw()
        {
            InitializeComponent();
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

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (pw.Text != cpw.Text)
            {
                MessageBox.Show("Both Password should match");
                cpw.Focus();
                cpw.Clear();
            }
            else
            {
                string constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "sp_ChangePassword";
                comm.Parameters.AddWithValue("@usern", Login.result);
                comm.Parameters.AddWithValue("@passw", pw.Text);
                comm.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Password Changed Successfully!");
                this.Hide();
                Main m = new Main();
                m.Show();
            }
          
        }
    }
}
