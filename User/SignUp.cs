using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace User
{
    public partial class SignUp : Form
    {
        private string connectionString = @"Data Source=HADI2023\SQLEXPRESS;Initial Catalog=vitalitygym1;Integrated Security=True;";
        private SqlConnection connection;

        public SignUp()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox1.Text;
            string city = guna2TextBox2.Text;
            string email = guna2TextBox3.Text;
            string password = guna2TextBox4.Text;
            string selectedPackage = guna2ComboBox1.SelectedItem.ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT packageprice FROM package WHERE packagename = @packageName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@packageName", selectedPackage);

                    int packagePrice = Convert.ToInt32(command.ExecuteScalar());
                    int billAmount = packagePrice;
                    MessageBox.Show($"The bill amount for {selectedPackage} is {billAmount}.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox1.Text;
            string city = guna2TextBox2.Text;
            string email = guna2TextBox3.Text;
            string password = guna2TextBox4.Text;
            string selectedPackage = guna2ComboBox1.SelectedItem.ToString();

            try
            {
                int packagePrice = GetPackagePrice(selectedPackage);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO client (clientpassword, clientname, clientcity, clientemail, clientpackage, clientbillstatus) " +
                                                               "VALUES (@Password, @Name, @City, @Email, @Package, 'Pending'); SELECT SCOPE_IDENTITY();", connection))
                    {
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@City", city);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Package", selectedPackage);

                        int clientId = Convert.ToInt32(command.ExecuteScalar());

                        using (SqlCommand packageCommand = new SqlCommand("INSERT INTO package (packageprice, packagename, clientid) " +
                                                                           "VALUES (@PackagePrice, @PackageName, @ClientId)", connection))
                        {
                            packageCommand.Parameters.AddWithValue("@PackagePrice", packagePrice);
                            packageCommand.Parameters.AddWithValue("@PackageName", selectedPackage);
                            packageCommand.Parameters.AddWithValue("@ClientId", clientId);

                            packageCommand.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Signed up successfully!");

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private int GetPackagePrice(string packageName)
        {
            int packagePrice = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT packageprice FROM package WHERE packagename = @PackageName", connection))
                {
                    command.Parameters.AddWithValue("@PackageName", packageName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            packagePrice = reader.GetInt32(0);
                        }
                    }
                }

                connection.Close();
            }

            return packagePrice;

        }
    }
}
