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
    public partial class PayementForm : Form
    {
        private string loggedInUserID;

        public PayementForm(string userID)
        {
            InitializeComponent();
            loggedInUserID = userID;
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string creditCardNumber = guna2TextBox1.Text;
            string cvv = guna2TextBox2.Text;
            if (creditCardNumber.Length != 15 || !creditCardNumber.All(char.IsDigit))
            {
                MessageBox.Show("Invalid credit card number. Please enter a 15-digit numeric value.");
                return;
            }
            if (cvv.Length != 5 || !cvv.All(char.IsDigit))
            {
                MessageBox.Show("Invalid CVV. Please enter a 5-digit numeric value.");
                return;
            }
            string updateClientQuery = "UPDATE client SET clientbillstatus = 'Paid' WHERE clientid = @clientId";

            using (SqlConnection connection = new SqlConnection(@"Data Source=HADI2023\SQLEXPRESS;Initial Catalog=vitalitygym1;Integrated Security=True;"))
            {
                using (SqlCommand updateClientCommand = new SqlCommand(updateClientQuery, connection))
                {
                    updateClientCommand.Parameters.Add("@clientId", SqlDbType.NVarChar, 50).Value = loggedInUserID; // Assuming loggedInUserID is accessible

                    connection.Open();
                    updateClientCommand.ExecuteNonQuery();
                }
            }
            string insertBillingQuery = "INSERT INTO bills (clientid, billdate, billstatus) VALUES (@clientId, @billDate, 'pending')";

            using (SqlConnection connection = new SqlConnection(@"Data Source=HADI2023\SQLEXPRESS;Initial Catalog=vitalitygym1;Integrated Security=True;"))
            {
                using (SqlCommand insertBillingCommand = new SqlCommand(insertBillingQuery, connection))
                {
                    insertBillingCommand.Parameters.Add("@clientId", SqlDbType.NVarChar, 50).Value = loggedInUserID; // Assuming loggedInUserID is accessible
                    insertBillingCommand.Parameters.Add("@billDate", SqlDbType.DateTime).Value = DateTime.Now;

                    connection.Open();
                    insertBillingCommand.ExecuteNonQuery();
                }
            }
            string updateBillingQuery = "UPDATE bills SET billstatus = 'Paid' WHERE clientid = @clientId";

            using (SqlConnection connection = new SqlConnection(@"Data Source=HADI2023\SQLEXPRESS;Initial Catalog=vitalitygym1;Integrated Security=True;"))
            {
                using (SqlCommand updateBillingCommand = new SqlCommand(updateBillingQuery, connection))
                {
                    updateBillingCommand.Parameters.Add("@clientId", SqlDbType.NVarChar, 50).Value = loggedInUserID; // Assuming loggedInUserID is accessible

                    connection.Open();
                    updateBillingCommand.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Payment successful. Your bill status has been updated.");
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";

        }

        private void PayementForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            UserDashboard user = new UserDashboard();
            user.Show();
            this.Hide();
        }
    }
}
