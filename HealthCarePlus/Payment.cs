using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HealthCarePlus
{
    public partial class Payment : Form
    {
        string con;
        MySqlConnection connection;
        public Payment()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            table_load();
            cmbType.Items.Add("APPOINTMNET");
            cmbType.Items.Add("THEATER");
            cmbType.Items.Add("MEDICATION");
            cmbType.Items.Add("OTHER");
           
            cmbType.Text = "Select Type";

            cmbStatus.Items.Add("PAID");
            cmbStatus.Items.Add("PENDING");
            cmbStatus.Items.Add("CANSEL");

            cmbStatus.Text = "Select Status";
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
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbType.SelectedItem.ToString())
                || string.IsNullOrEmpty(txtPDate.Text) || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString())
                || string.IsNullOrEmpty(txtPName.Text) || string.IsNullOrEmpty(txtPrice.Text)
                || string.IsNullOrEmpty(txtPId.Text) || string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();

            // Construct the SQL UPDATE statement
            string updateQuery = "UPDATE payment " +
                                 "SET patientId = @PatientId, typeId = @TypeId, paymentDate = @PaymentDate, " +
                                 "price = @Price, type = @Type, status = @Status, patientName = @PatientName " +
                                 "WHERE id = @PaymentIdToUpdate";

            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
            {
                updateCommand.Parameters.AddWithValue("@PaymentIdToUpdate", txtId.Text);
                updateCommand.Parameters.AddWithValue("@PatientId", txtPId.Text);
                updateCommand.Parameters.AddWithValue("@TypeId", 0); 
                updateCommand.Parameters.AddWithValue("@PaymentDate", txtPDate.Value);
                updateCommand.Parameters.AddWithValue("@Price", txtPrice.Text);
                updateCommand.Parameters.AddWithValue("@Type", cmbType.SelectedItem.ToString());
                updateCommand.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                updateCommand.Parameters.AddWithValue("@PatientName", txtPName.Text);

                int rowsAffected = updateCommand.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Payment record updated successfully.");
                    // Clear input fields or perform other actions as needed.
                    table_load();
                }
                else
                {
                    MessageBox.Show("Update operation failed. Payment ID not found.");
                }
            }
            }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbType.SelectedItem.ToString()) 
                 || string.IsNullOrEmpty(txtPDate.Text) || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString())
                 || string.IsNullOrEmpty(txtPName.Text) || string.IsNullOrEmpty(txtPrice.Text)
                 || string.IsNullOrEmpty(txtPId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            // Construct the SQL INSERT statement
            string insertQuery = "INSERT INTO payment (patientId, typeId, paymentDate, price, type, status, patientName) " +
                                 "VALUES (@PatientId, @TypeId, @PaymentDate, @Price, @Type, @Status, @PatientName)";

            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@PatientId", txtPId.Text);
                //insertCommand.Parameters.AddWithValue("@AppointmentId", null);
                //insertCommand.Parameters.AddWithValue("@TheaterDetailsId", null);
                //insertCommand.Parameters.AddWithValue("@MedicationId", null);
                insertCommand.Parameters.AddWithValue("@TypeId", 0);
                insertCommand.Parameters.AddWithValue("@PaymentDate", txtPDate.Value);
                insertCommand.Parameters.AddWithValue("@Price", txtPrice.Text);
                insertCommand.Parameters.AddWithValue("@Type", cmbType.SelectedItem.ToString());
                insertCommand.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                insertCommand.Parameters.AddWithValue("@PatientName", txtPName.Text);


                int rowsAffected = insertCommand.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Payment record inserted successfully.");
                    // Clear input fields or perform other actions as needed.
                    table_load();
                }
                else
                {
                    MessageBox.Show("Insertion failed.");
                }
            }
        }

        private void table_load()
        {
            try
            {
                connection.Open();

                
                string selectQuery = "SELECT * FROM payment";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                   
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        
                        dataGridView1.DataSource = dataTable;
                    }
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void clearText()
        {
            txtId.Text = "";
            txtPId.Text = "";
            txtPName.Text = "";
            txtPrice.Text = "";
            cmbStatus.Text = "Select Status";
        
            txtPDate.Text = "";
            cmbType.Text = "Select Type";
        }

        private void btnPSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM patient WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtPId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string name = reader["name"].ToString();
                    
                        txtPName.Text = name;

                       
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
                table_load2(txtPId.Text);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            table_load();
            clearText();
        }

        private void table_load2(string id)
        {

            try
            {
                connection.Open();

                // SQL query to select data from the "report" table
                string selectQuery = "SELECT * FROM payment WHERE patientId=@Id";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM payment WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string pId = reader["patientId"].ToString();
                        string type = reader["type"].ToString();
                        string status = reader["status"].ToString();
                        string patientName = reader["patientName"].ToString();
                        string price = reader["price"].ToString();
                        string paymentDate = reader["paymentDate"].ToString();
                       
                        txtPId.Text =pId ;
                        txtPName.Text =patientName ;
                        txtPrice.Text =price ;
                        cmbStatus.Text =status ;
                        
                        txtPDate.Text = paymentDate;
                        cmbType.Text = type;



                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Payment record not found.");
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

        private void txtPName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            Bill bill = new Bill(txtPId.Text);
            if (bill == null)
            {
                bill.Parent = this;
            }
            bill.Show();
            //this.Hide();
        }
    }
}
