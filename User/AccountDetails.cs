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
    public partial class AccountDetails : Form
    {
        private string loggedInUserID;

        public AccountDetails(string userID)
        {
            InitializeComponent();
            loggedInUserID = userID;
        }
        private void RetrieveClientData()
        {
            string path = @"Data Source=HADI2023\SQLEXPRESS;Initial Catalog=vitalitygym1;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            string query = "SELECT clientname, clientcity, clientemail, clientpassword, clientpackage, clientbillstatus FROM client WHERE clientid = @clientId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@clientId", loggedInUserID);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string name = reader["clientname"].ToString();
                string email = reader["clientemail"].ToString();
                string city = reader["clientcity"].ToString();
                string packages = reader["clientpassword"].ToString();
                string password = reader["clientpackage"].ToString();
                string billstatus = reader["clientbillstatus"].ToString();
                guna2TextBox1.Text = name;
                guna2TextBox2.Text = email;
                guna2TextBox3.Text = city;
                guna2TextBox4.Text = packages;
                guna2TextBox5.Text = password;
                guna2TextBox6.Text = billstatus;
            }

            reader.Close();
            connection.Close();
        }

        private void AccountDetails_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            RetrieveClientData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            UserDashboard user = new UserDashboard();
            user.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            UserDashboard user = new UserDashboard();
            user.Show();
            this.Hide();
        }
    }
}
