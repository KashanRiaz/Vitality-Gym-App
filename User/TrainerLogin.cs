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
    public partial class TrainerLogin : Form
    {
        bool Verify()
        {
            string path = @"Data Source=HADI2023\SQLEXPRESS;Initial Catalog=vitalitygym1;Integrated Security=True";
            SqlConnection Connection = new SqlConnection(path);
            Connection.Open();
            string query = "SELECT * FROM  trainer WHERE tid = '" + guna2TextBox1.Text + "' AND tpassword = '" + guna2TextBox2.Text + "'";
            SqlCommand command = new SqlCommand(query, Connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TrainerLogin()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (Verify() == true)
            {

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
    }
}
