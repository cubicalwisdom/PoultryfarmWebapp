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
    public partial class Login : Form
    {
        public static string UserName = "";
        public static string Priority = "";
        public static string result = "";
        public Login()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

            if (un.Text == "" || un.Text == null)
            {
                MessageBox.Show("Please enter the username");
                un.Focus();
            }
            else if (pw.Text == "" || pw.Text == null)
            {
                MessageBox.Show("Please enter the password");
                pw.Focus();
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
                comm.CommandText = "sp_Userlogin";
                comm.Parameters.AddWithValue("@usern", un.Text);
                comm.Parameters.AddWithValue("@passw", pw.Text);
                 result = Convert.ToString(comm.ExecuteScalar());
                connection.Close();
                if (result == "NO")
                {
                    MessageBox.Show("Username or Password is incorrect");
                    un.Text = null;
                    pw.Text = null;
                    un.Focus();
                }
                else
                {
                    int index = result.IndexOf('_');
                    UserName = result.Substring(0, index);
                    Priority = result.Substring(index+1, 1);
                    using (StreamWriter writetext = new StreamWriter("sysLL.txt"))
                    {
                        writetext.WriteLine("1");
                    }
                 
                    this.Hide();
                    Main m= new Main();
                    m.Show();

                }
             
            }
         
        }
    }
}
