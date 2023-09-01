using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCarePlus
{
    public partial class Medication : Form
    {
        string con;
        MySqlConnection connection;
        public Medication()
        {
            InitializeComponent();
              con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            //con = "Server=localhost;Database=mydatabase;Uid=root;Pwd='';";
            connection = new MySqlConnection(con);
            //cmb status
            cmbDose.Items.Add("One Time");
            cmbDose.Items.Add("Two Time");
            cmbDose.Items.Add("Three TIme");
            // Set a default or placeholder text
            cmbDose.Text = "Select Dose";


            table_load();

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void btnPayment_Click(object sender, EventArgs e)
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

        private void btnPayment_Click_1(object sender, EventArgs e)
        {
            Payment payment = new Payment();
            if (payment == null)
            {
                payment.Parent = this;
            }
            payment.Show();
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

        private void btnStaff_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            if (staff == null)
            {
                staff.Parent = this;
            }
            staff.Show();
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
            Registration registration = new Registration();
            if (registration == null)
            {
                registration.Parent = this;
            }
            registration.Show();
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

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNames.Text) )
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            string insertQuery = "INSERT INTO medicine (name) VALUES (@Name)";

            using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", txtNames.Text); // Get the medicine name from a TextBox
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Medicine added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add medicine.");
                }

                connection.Close();
                table_load();
            }
        }
        private void table_load()
        {
            try
            {
                connection.Open();

                // Define the SQL query to retrieve resource data
                string query = "SELECT * FROM medicine";

                // Create a MySqlCommand with the query and connection
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Create a DataTable to hold the retrieved data
                    DataTable dataTable = new DataTable();

                    // Create a MySqlDataAdapter to fill the DataTable
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    // Bind the DataTable to the DataGridView
                    dataGridView2.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle connection or database-related errors here
                Console.WriteLine("Error: " + ex.Message);
            }
            finally { connection.Close(); }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM medicine WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtIds.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string name = reader["name"].ToString();
                       


                        txtNames.Text = name;
                      

                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Resource record not found.");
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

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNames.Text) || string.IsNullOrEmpty(txtIds.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            string updateQuery = "UPDATE medicine SET name = @Name WHERE id = @Ids";


            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            {
                // Set parameters
                command.Parameters.AddWithValue("@Name", txtNames.Text);
                command.Parameters.AddWithValue("@Ids", txtIds.Text);


                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Medicine record updated successfully.");
                    //clearText();
                    // Clear input fields or perform other actions as needed.
                    table_load();
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtIds.Text = "";
            txtName.Text = "";
            txtNames.Text = "";
            txtDate.Text = "";
            cmbDose.Text = "Select Dose";
            txtCount.Text = "";
        }
    }
}
