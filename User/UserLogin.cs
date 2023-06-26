using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User
{
    public partial class UserLogin : Form
    {
         string loggedInUserID; // Added variable to store logged-in user ID

         bool Verify()
        {
            string path = @"Data Source=HADI2023\SQLEXPRESS;Initial Catalog=vitalitygym1;Integrated Security=True";
            SqlConnection Connection = new SqlConnection(path);
            Connection.Open();

            string query = "SELECT * FROM  client WHERE clientid = '" + guna2TextBox1.Text + "' AND clientpassword = '" + guna2TextBox2.Text + "'";
            SqlCommand command = new SqlCommand(query, Connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows == true)
            {
                loggedInUserID = guna2TextBox1.Text; 
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserLogin()
        {
            InitializeComponent();
        }

        void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (Verify() == true)
            {

                UserDashboard user = new UserDashboard();
                user.LoggedInUserID = loggedInUserID;
                user.Show();
                this.Hide();

   
            }
            else if (Verify() == null)
            {
                MessageBox.Show("Please enter your ID and Password");
            }
            else
            {
                MessageBox.Show("No entry found.");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp sign = new SignUp();
            sign.Show();
            this.Hide();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
