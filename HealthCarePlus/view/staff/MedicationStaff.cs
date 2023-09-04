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
    public partial class MedicationStaff : Form
    {
        string con;
        MySqlConnection connection;
        public MedicationStaff()
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

            table2_load();
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

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNames.Text))
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
                        txtName.Text = name;

                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Patient record not found.");
                    }
                }






                // Create the SQL SELECT query with a WHERE clause
                string selectQuery = "SELECT * FROM medication WHERE patientId = @PatientId";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@PatientId", txtId.Text);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Display the results, e.g., in a DataGridView or ListView
                        dataGridView1.DataSource = dataTable;
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

        private void btnMSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM medication WHERE id = @Id";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtMId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string pId = reader["patientId"].ToString();
                        string MId = reader["medicineId"].ToString();
                        string PName = reader["patientName"].ToString();
                        string MName = reader["medicineName"].ToString();
                        string noOfDays = reader["noOfDays"].ToString();
                        string dose = reader["dose"].ToString();
                        string date = reader["date"].ToString();




                        txtNames.Text = MName;
                        txtName.Text = PName;
                        txtId.Text = pId;
                        txtIds.Text = MId;
                        txtDate.Text = date;
                        txtCount.Text = noOfDays;
                        cmbDose.Text = dose;
                        //txtMId.Text = name;

                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Medication record not found.");
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
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtNames.Text) || string.IsNullOrEmpty(txtId.Text)
                   || string.IsNullOrEmpty(txtIds.Text) || string.IsNullOrEmpty(cmbDose.SelectedItem.ToString())
                   || string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtCount.Text) || string.IsNullOrEmpty(txtMId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            string insertQuery = " UPDATE medication " +
                    "SET date = @Date, noOfDays = @NoOfDays, " +
                    "dose = @Dose, medicineId = @MedicineId, " +
                    "patientName = @PatientName, medicineName = @MedicineName " +
                    "WHERE id = @Id";


            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            {
                command.Parameters.AddWithValue("@Date", txtDate.Value);
                command.Parameters.AddWithValue("@PatientId", txtId.Text);
                command.Parameters.AddWithValue("@NoOfDays", txtCount.Text);
                command.Parameters.AddWithValue("@Dose", cmbDose.Text);
                command.Parameters.AddWithValue("@MedicineId", txtIds.Text);
                command.Parameters.AddWithValue("@PatientName", txtName.Text);
                command.Parameters.AddWithValue("@MedicineName", txtNames.Text);
                command.Parameters.AddWithValue("@Id", txtMId.Text);


                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Medication record updated successfully.");
                    clear_text();
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
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtNames.Text) || string.IsNullOrEmpty(txtPrice.Text)
                    || string.IsNullOrEmpty(txtIds.Text) || string.IsNullOrEmpty(cmbDose.SelectedItem.ToString())
                    || string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtCount.Text))
                {
                    MessageBox.Show("Please Fill All Required Field.");
                    return;
                }
                connection.Open();

                // Construct the INSERT query for Medication
                string insertMedicationQuery = "INSERT INTO medication (date, patientId, noOfDays, dose, medicineId, patientName, medicineName) " +
                                             "VALUES (@Date, @PatientId, @NoOfDays, @Dose, @MedicineId, @PatientName, @MedicineName)";

                // Create a MySqlCommand with the INSERT query for Medication and connection
                using (MySqlCommand medicationCommand = new MySqlCommand(insertMedicationQuery, connection))
                {
                    medicationCommand.Parameters.AddWithValue("@Date", txtDate.Value);
                    medicationCommand.Parameters.AddWithValue("@PatientId", txtId.Text);
                    medicationCommand.Parameters.AddWithValue("@NoOfDays", txtCount.Text);
                    medicationCommand.Parameters.AddWithValue("@Dose", cmbDose.Text);
                    medicationCommand.Parameters.AddWithValue("@MedicineId", txtIds.Text);
                    medicationCommand.Parameters.AddWithValue("@PatientName", txtName.Text);
                    medicationCommand.Parameters.AddWithValue("@MedicineName", txtNames.Text);

                    // Execute the INSERT query for Medication
                    int rowsAffectedMedication = medicationCommand.ExecuteNonQuery();

                    if (rowsAffectedMedication > 0)
                    {
                        MessageBox.Show("Medication data saved successfully.");

                        // Retrieve the ID of the last inserted Medication
                        long lastInsertedMedicationId = medicationCommand.LastInsertedId; // Use the correct method to get the last inserted ID (e.g., LastInsertedId or SELECT MAX(id) as lastId)

                        // Now, you can save the Payment information linked to this Medication and Patient
                        string insertPaymentQuery = "INSERT INTO payment (medicationId, patientId, paymentDate, price, type, status, patientName) " +
                                                    "VALUES (@MedicationId, @PatientId, @PaymentDate, @Price, @Type, @Status, @PatientName)";

                        using (MySqlCommand paymentCommand = new MySqlCommand(insertPaymentQuery, connection))
                        {
                            // Set parameters for the Payment query
                            paymentCommand.Parameters.AddWithValue("@MedicationId", lastInsertedMedicationId);
                            paymentCommand.Parameters.AddWithValue("@PatientId", txtId.Text);
                            paymentCommand.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                            paymentCommand.Parameters.AddWithValue("@Price", txtPrice.Text);
                            paymentCommand.Parameters.AddWithValue("@Type", "MEDICATION");
                            paymentCommand.Parameters.AddWithValue("@Status", "PENDING");
                            paymentCommand.Parameters.AddWithValue("@PatientName", txtName.Text);

                            // Execute the INSERT query for Payment
                            int paymentRowsAffected = paymentCommand.ExecuteNonQuery();

                            if (paymentRowsAffected > 0)
                            {
                                MessageBox.Show("Payment information saved successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Failed to save payment information.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to save medication data.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                table2_load();
            }
        }
        private void clear_text()
        {
            txtId.Text = "";
            txtIds.Text = "";
            txtMId.Text = "";
            txtName.Text = "";
            txtNames.Text = "";
            txtDate.Text = "";
            cmbDose.Text = "Select Dose";
            txtCount.Text = "";
            table2_load();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear_text();
        }
        private void table_load()
        {
            try
            {
                connection.Open();

                // Define the SQL query to retrieve resource data
                string query = "SELECT * FROM medicine  ORDER BY Id DESC";

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


        private void table2_load()
        {
            try
            {
                connection.Open();

                // Define the SQL query to retrieve resource data
                string query = "SELECT * FROM medication ORDER BY Id DESC";

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
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle connection or database-related errors here
                Console.WriteLine("Error: " + ex.Message);
            }
            finally { connection.Close(); }
        }
    }
}

       