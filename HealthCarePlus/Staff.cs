using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace HealthCarePlus
{
    public partial class Staff : Form
    {
        string con;
        MySqlConnection connection;
        public Staff()
        {
            InitializeComponent();
            
            cmdRole.Items.Add("ADMIN");
            cmdRole.Items.Add("STAFF");
            cmdRole.Items.Add("DOCTOR");
            cmdStatus.Items.Add("ACTIVE");
            cmdStatus.Items.Add("INACTIVE");
            // Set a default or placeholder text
            cmdRole.Text = "Select Role";
            cmdStatus.Text = "Select Status";
            //Database connection
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            table_load();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            if (report == null)
            {
                report.Parent = this;
            }
            report.Show();
            this.Hide();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            Payment payment = new Payment();
            if (payment == null)
            {
                payment.Parent = this;
            }
            payment.Show();
            this.Hide();
        }

        private void BtnMedication_Click(object sender, EventArgs e)
        {
            Medication medication = new Medication();
            if (medication == null)
            {
                medication.Parent = this;
            }
            medication.Show();
            this.Hide();
        }

        private void btnResource_Click(object sender, EventArgs e)
        {
            Resource resource = new Resource();
            if (resource == null)
            {
                resource.Parent = this;
            }
            resource.Show();
            this.Hide();
        }

        private void btnTheaters_Click(object sender, EventArgs e)
        {
            Theater theater = new Theater();
            if (theater == null)
            {
                theater.Parent = this;
            }
            theater.Show();
            this.Hide();
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            Schedule schedule = new Schedule();
            if (schedule == null)
            {
                schedule.Parent = this;
            }
            schedule.Show();
            this.Hide();
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment();
            if (appointment == null)
            {
                appointment.Parent = this;
            }
            appointment.Show();
            this.Hide();
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            if (patient == null)
            {
                patient.Parent = this;
            }
            patient.Show();
            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            if (dashBoard == null)
            {
                dashBoard.Parent = this;
            }
            dashBoard.Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text)
                 || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(cmdStatus.SelectedItem.ToString())
                 || string.IsNullOrEmpty(txtNic.Text) || string.IsNullOrEmpty(txtContact.Text)
                 || string.IsNullOrEmpty(cmdRole.SelectedItem.ToString()) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            string insertQuery = "INSERT INTO user (name, email, address, qualification, status, nic, contact, password, role) " +
                                 "VALUES (@Name, @Email, @Address,@Qualification, @Status, @Nic, @ContactNo,@Password, @Role)";


            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            {
                command.Parameters.AddWithValue("@Name", txtName.Text);
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@Address", txtAddress.Text);
                command.Parameters.AddWithValue("@Status", cmdStatus.SelectedItem.ToString());
                command.Parameters.AddWithValue("@Nic", txtNic.Text);
                command.Parameters.AddWithValue("@ContactNo", txtContact.Text);
                command.Parameters.AddWithValue("@Role", cmdRole.SelectedItem.ToString());
                command.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text));
                command.Parameters.AddWithValue("@Qualification", txtQualification.Text);


                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Staff record inserted successfully.");
                    connection.Close();
                    // Clear input fields or perform other actions as needed.
                    table_load();
                }
                else
                {
                    MessageBox.Show("Insertion failed.");
                    connection.Close();
                }
               
            }
        }
        //password encypt
        public  string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
        private void table_load()
        {
            try
            {
                // Create a connection to the database

                connection.Open();

                // Define the SQL query to select data
                string selectQuery = "SELECT id AS Id,name AS Name ,email AS Email,status AS Status,address AS Address, role AS Role,nic AS NIC,contact AS Contact FROM user"; // Replace with your table name

                // Create a data adapter to execute the query and fill a DataSet
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "user"); // Replace with your table name


                    // Bind the DataGridView to a specific DataTable within the DataSet
                    dataGridView1.DataSource = dataSet.Tables["user"]; // Replace with your table name
                }

                // Close the connection
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM user WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string name = reader["name"].ToString();
                        string email = reader["email"].ToString();
                        //string password = reader["password"].ToString();
                        string contact = reader["contact"].ToString();
                        string nic = reader["nic"].ToString();
                        string qualification = reader["qualification"].ToString();
                        string address = reader["address"].ToString();
                        string role = reader["role"].ToString();
                        string status = reader["status"].ToString();


                        txtName.Text = name;
                        txtEmail.Text = email;
                        txtAddress.Text = address;
                        cmdStatus.Text = status;
                        txtNic.Text = nic;
                        txtContact.Text = contact;
                        cmdRole.Text = role;
                       
                        txtQualification.Text = qualification;



                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Patient record not found.");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || (string.IsNullOrEmpty(txtId.Text)) || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(cmdStatus.SelectedItem.ToString())
                || string.IsNullOrEmpty(txtNic.Text) || string.IsNullOrEmpty(txtContact.Text)
                || string.IsNullOrEmpty(cmdRole.SelectedItem.ToString()))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            string insertQuery=null;
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
            insertQuery = "UPDATE user\r\nSET name = @Name,\r\n    email = @Email,\r\n    address = @Address,\r\n    qualification = @Qualification,\r\n    status = @Status,\r\n    nic = @Nic,\r\n    contact = @ContactNo,\r\n    role = @Role\r\nWHERE id = @Id;";
            }
            else
            {
            insertQuery = "UPDATE user\r\nSET name = @Name,\r\n    email = @Email,\r\n    address = @Address,\r\n    qualification = @Qualification,\r\n    status = @Status,\r\n    nic = @Nic,\r\n    contact = @ContactNo,\r\n    password = @Password,\r\n    role = @Role\r\nWHERE id = @Id;";
            }

            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            {
                command.Parameters.AddWithValue("@Name", txtName.Text);
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@Address", txtAddress.Text);
                command.Parameters.AddWithValue("@Status", cmdStatus.SelectedItem.ToString());
                command.Parameters.AddWithValue("@Nic", txtNic.Text);
                command.Parameters.AddWithValue("@ContactNo", txtContact.Text);
                command.Parameters.AddWithValue("@Role", cmdRole.SelectedItem.ToString());
                if (string.IsNullOrEmpty(txtPassword.Text)) { 
                command.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text));
                 }
                command.Parameters.AddWithValue("@Qualification", txtQualification.Text);
                command.Parameters.AddWithValue("@Id", txtId.Text);


                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Staff record updated successfully.");
                    connection.Close();
                    // Clear input fields or perform other actions as needed.
                    table_load();
                }
                else
                {
                    MessageBox.Show("Update failed.");
                    connection.Close();
                }
              
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearText();
        }
        private void clearText()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            cmdStatus.Text = "";
            txtNic.Text = "";
            txtContact.Text = "";
            cmdRole.Text = "";
            txtPassword.Text = "";
            txtQualification.Text = "";
            txtId.Text = "";
        }
    }
}
