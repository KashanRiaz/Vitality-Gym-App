using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User
{
    public partial class UserDashboard : Form

    {
        public string LoggedInUserID { get; set; }

        private string connectionString = @"Data Source=HADI2023\SQLEXPRESS;Initial Catalog=vitalitygym1;Integrated Security=True;";
        private SqlConnection connection;
        public UserDashboard()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            PayementForm pay = new PayementForm(LoggedInUserID); 
            pay.Show();
            this.Hide();
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        { 
                AccountDetails details = new AccountDetails(LoggedInUserID);
                details.Show();
                this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            UserLogin login = new UserLogin();
            login.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
