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
using HealthCarePlus.view;
using HealthCarePlus.service;

namespace HealthCarePlus
{
    
    public partial class login : Form
    {
        string con;
        string role;
        MySqlConnection connection;
        Auth auth;
        public login()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            auth = new Auth();
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
                if (role=="ADMIN") { 
                DashBoard dashBoard = new DashBoard();
                if (dashBoard == null)
                {
                    dashBoard.Parent = this;
                }
                dashBoard.Show();
                this.Hide();
            }
                else if (role=="STAFF")
            {
                    DashBoardStaff dashBoard = new DashBoardStaff();
                    if (dashBoard == null)
                    {
                        dashBoard.Parent = this;
                    }
                    dashBoard.Show();
                    this.Hide();
             }
                else
             {
                    MessageBox.Show("This user haven't portal yet..");
             }
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

                string hashedPassword = auth.HashPassword(password);

                //userNametxt.Text = hashedPassword;

                string query = "SELECT COUNT(*),role FROM User WHERE email = @username AND Password = @password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", hashedPassword);
                int count = 0;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        count = Convert.ToInt32(reader[0]);
                        string roles = reader["role"].ToString();

                        connection.Close();

                        if (count > 0)
                        {
                            if (roles == "ADMIN")
                            {
                                role= "ADMIN";
                            }
                            else if (roles == "STAFF")
                            {
                                role = "STAFF";
                            }
                        }
                    }
                }


                //int count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return count > 0;
                
            }
        }
       
    }
}
