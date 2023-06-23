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

namespace trainer
{
    public partial class addschedule : Form
    {
        public addschedule()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            int tid = Convert.ToInt32(guna2TextBox1.Text);
            int cid = Convert.ToInt32(guna2TextBox2.Text);
            DateTime dateTime = guna2DateTimePicker1.Value;

            if (CheckIfExists(tid, cid))
            {
                Insert(tid, cid, dateTime);

                guna2TextBox1.Text = string.Empty;
                guna2TextBox2.Text = string.Empty;
                guna2DateTimePicker1.Value = DateTime.Now;

                MessageBox.Show("Added Successfully");
            }
            else
            {
                MessageBox.Show("The provided tid or cid does not exist in the database.");
            }
        }

        public bool CheckIfExists(int tid, int cid)
        {
            string path = @"Data Source=.\SQLEXPRESS03;Initial Catalog=vitalitygym1;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(path))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM trainer WHERE tid = @tid AND EXISTS (SELECT 1 FROM client WHERE clientid = @cid)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tid", tid);
                    command.Parameters.AddWithValue("@cid", cid);
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        public void Insert(int tid, int cid, DateTime dateTime)
        {
            string path = @"Data Source=.\SQLEXPRESS03;Initial Catalog=vitalitygym1;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(path))
            {
                connection.Open();

                string query = "INSERT INTO workout (tid, clientid, wtime) VALUES (@tid, @cid, @dateTime)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tid", tid);
                    command.Parameters.AddWithValue("@cid", cid);
                    command.Parameters.AddWithValue("@dateTime", dateTime);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void addschedule_Load(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
