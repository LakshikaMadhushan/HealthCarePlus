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

namespace HealthCarePlus.view
{
    public partial class PatientStaff : Form
    {
        string con;
        MySqlConnection connection;
        public PatientStaff()
        {
            InitializeComponent();
            // Add "Male" and "Female" items to the ComboBox
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            // Set a default or placeholder text
            cmbGender.Text = "Select Gender";
            //Database connection
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            table_load();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            DashBoardStaff dashBoard = new DashBoardStaff();
            if (dashBoard == null)
            {
                dashBoard.Parent = this;
            }
            dashBoard.Show();
            this.Hide();
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            AppointmentStaff appointment = new AppointmentStaff();
            if (appointment == null)
            {
                appointment.Parent = this;
            }
            appointment.Show();
            this.Hide();
        }

        private void btnTheaters_Click(object sender, EventArgs e)
        {
            TheaterStaff theater = new TheaterStaff();
            if (theater == null)
            {
                theater.Parent = this;
            }
            theater.Show();
            this.Hide();
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            PatientStaff patient = new PatientStaff();
            if (patient == null)
            {
                patient.Parent = this;
            }
            patient.Show();
            this.Hide();
        }

        private void BtnMedication_Click(object sender, EventArgs e)
        {
            MedicationStaff medication = new MedicationStaff();
            if (medication == null)
            {
                medication.Parent = this;
            }
            medication.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM patient WHERE id = @Id;";


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
                        string gender = reader["gender"].ToString();
                        string contact = reader["contactNo"].ToString();
                        string nic = reader["nic"].ToString();
                        string dob = reader["dateOfBirth"].ToString();
                        string address = reader["address"].ToString();


                        txtName.Text = name;
                        txtEmail.Text = email;
                        txtAddress.Text = address;
                        cmbGender.Text = gender;
                        txtNic.Text = nic;
                        txtContact.Text = contact;
                        dateBirth.Text = dob;



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

        private void btnPatientReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            PatientPop patientPop = new PatientPop(txtId.Text, txtName.Text);
            if (patientPop == null)
            {
                patientPop.Parent = this;
            }
            patientPop.Show();
            //this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text)
               || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(cmbGender.SelectedItem.ToString())
               || string.IsNullOrEmpty(txtNic.Text) || string.IsNullOrEmpty(txtContact.Text)
               || string.IsNullOrEmpty(dateBirth.Text) || string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            string insertQuery = "UPDATE patient\r\nSET \r\n    name = @Name,\r\n    email = @Email,\r\n    address = @Address,\r\n    gender = @Gender,\r\n    nic = @Nic,\r\n    contactNo = @ContactNo,\r\n    dateOfBirth = @DateOfBirth\r\nWHERE id = @Id;";


            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            {
                command.Parameters.AddWithValue("@Name", txtName.Text);
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@Address", txtAddress.Text);
                command.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                command.Parameters.AddWithValue("@Nic", txtNic.Text);
                command.Parameters.AddWithValue("@ContactNo", txtContact.Text);
                command.Parameters.AddWithValue("@DateOfBirth", dateBirth.Value);
                command.Parameters.AddWithValue("@Id", txtId.Text);


                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Patient record updated successfully.");
                    clearText();
                    // Clear input fields or perform other actions as needed.
                    table_load();
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(cmbGender.SelectedItem.ToString())
                || string.IsNullOrEmpty(txtNic.Text) || string.IsNullOrEmpty(txtContact.Text)
                || string.IsNullOrEmpty(dateBirth.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            string insertQuery = "INSERT INTO patient (name, email, address, gender, nic, contactNo, dateOfBirth) " +
                                 "VALUES (@Name, @Email, @Address, @Gender, @Nic, @ContactNo, @DateOfBirth)";


            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            {
                command.Parameters.AddWithValue("@Name", txtName.Text);
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@Address", txtAddress.Text);
                command.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                command.Parameters.AddWithValue("@Nic", txtNic.Text);
                command.Parameters.AddWithValue("@ContactNo", txtContact.Text);
                command.Parameters.AddWithValue("@DateOfBirth", dateBirth.Text);


                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Patient record inserted successfully.");
                    // Clear input fields or perform other actions as needed.
                    table_load();
                }
                else
                {
                    MessageBox.Show("Insertion failed.");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void table_load()
        {
            try
            {
                // Create a connection to the database

                connection.Open();

                // Define the SQL query to select data
                string selectQuery = "SELECT id AS Id,name AS Name ,email AS Email,dateOfBirth AS DOB,address AS Address, gender AS Gender,nic AS NIC,contactNo AS Contact FROM patient"; // Replace with your table name

                // Create a data adapter to execute the query and fill a DataSet
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "patient"); // Replace with your table name


                    // Bind the DataGridView to a specific DataTable within the DataSet
                    dataGridView1.DataSource = dataSet.Tables["patient"]; // Replace with your table name
                }

                // Close the connection
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void clearText()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "Select Gender";
            txtNic.Text = "";
            txtContact.Text = "";
            dateBirth.Text = "";
        }
    }
}
