using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HealthCarePlus
{
    
    public partial class login : Form
    {
        string con;
        MySqlConnection connection;
        public login()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            //con = "Server=localhost;Database=mydatabase;Uid=root;Pwd='';";
            connection = new MySqlConnection(con);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void passwordtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            txtLoginValidation.Text = "";
            lblErrorMessage.Text = "";

            string username = userNametxt.Text;
            string password = passwordtxt.Text;
           
            if (ValidateUser(username, password))
            {
                MessageBox.Show("Login successful!");
                // You can open your main application form or perform other actions here.
                DashBoard dashBoard = new DashBoard();
                if (dashBoard == null)
                {
                    dashBoard.Parent = this;
                }
                dashBoard.Show();
                this.Hide();
            }
            else
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    txtLoginValidation.Text = "Please fill in both username and password fields.";
                }
                else
                {
                    lblErrorMessage.Text = "Invalid username or password. Please try again.";
                }
            }
        }

        // Validate user credentials
        public bool ValidateUser(string username, string password)
        {
           
            {
                connection.Open();

                string hashedPassword = HashPassword(password);

                //userNametxt.Text = hashedPassword;

                string query = "SELECT COUNT(*) FROM User WHERE email = @username AND Password = @password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", hashedPassword);

                int count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return count > 0;
                
            }
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
