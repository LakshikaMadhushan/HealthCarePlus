using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCarePlus.controller
{
    public class MedicationController
    {
        MySqlConnection connection;
        public MedicationController(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void SearchMedicineById(TextBox txtIds, TextBox txtNames)
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
                        MessageBox.Show("Medicine record not found.");
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
            }
        }


        public void RegisterMedicine(string medicineName)
        {
            try
            {
                connection.Open();
                string insertQuery = "INSERT INTO medicine (name) VALUES (@Name)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", medicineName);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Medicine added successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to add medicine.");
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
            }
        }



        public  void UpdateMedicine(string medicineName, string medicineId)
        {
            try
            {
                connection.Open();
                string updateQuery = "UPDATE medicine SET name = @Name WHERE id = @Ids";

                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", medicineName);
                    command.Parameters.AddWithValue("@Ids", medicineId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Medicine record updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Update failed.");
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
            }
        }


        public void LoadMedicineData(DataGridView dataGridView)
        {
            try
            {
                connection.Open();

                // Define the SQL query to retrieve resource data
                string query = "SELECT * FROM medicine ORDER BY Id DESC";

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
                    dataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle connection or database-related errors here
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //=================================================================================

        public void SearchPatientAndMedication(string patientId, TextBox txtName, DataGridView dataGridView1)
        {
            try
            {
                connection.Open();

                // Part 1: Search for patient information
                string searchQuery = "SELECT * FROM patient WHERE id = @Id;";
                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);
                cmd.Parameters.AddWithValue("@Id", patientId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given patient ID
                        string name = reader["name"].ToString();
                        txtName.Text = name;
                    }
                    else
                    {
                        // No patient data found for the given ID
                        MessageBox.Show("Patient record not found.");
                    }
                }

                // Part 2: Retrieve medication records related to the patient
                string selectQuery = "SELECT * FROM medication WHERE patientId = @PatientId";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@PatientId", patientId);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Display the medication records in a DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., display an error message
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                connection.Close();
            }
        }



        public void SearchMedication(string medicationId, TextBox txtNames, TextBox txtName, TextBox txtId, TextBox txtIds, DateTimePicker txtDate, TextBox txtCount, ComboBox cmbDose)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM medication WHERE id = @Id";

                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);
                cmd.Parameters.AddWithValue("@Id", medicationId);

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
                // Handle exceptions, e.g., display an error message
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                connection.Close();
            }
        }



        public void UpdateMedicationRecord(
        string id, string patientId, string medicineId, string medicineName,
        string patientName, string dose, string noOfDays, DateTime date)
        {
            try
            {
                connection.Open();
                string updateQuery = "UPDATE medication " +
                                    "SET date = @Date, noOfDays = @NoOfDays, " +
                                    "dose = @Dose, medicineId = @MedicineId, " +
                                    "patientName = @PatientName, medicineName = @MedicineName " +
                                    "WHERE id = @Id";

                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                {
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@NoOfDays", noOfDays);
                    command.Parameters.AddWithValue("@Dose", dose);
                    command.Parameters.AddWithValue("@MedicineId", medicineId);
                    command.Parameters.AddWithValue("@PatientName", patientName);
                    command.Parameters.AddWithValue("@MedicineName", medicineName);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@PatientId", patientId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Medication record updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Update failed.");
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
            }
        }



        public void RegisterMedicationAndPayment(
        string patientId, string medicineId, string medicineName,
        string patientName, string dose, string noOfDays, decimal price)
        {
            try
            {
                connection.Open();

                // Construct the INSERT query for Medication
                string insertMedicationQuery = "INSERT INTO medication (date, patientId, noOfDays, dose, medicineId, patientName, medicineName) " +
                                             "VALUES (@Date, @PatientId, @NoOfDays, @Dose, @MedicineId, @PatientName, @MedicineName)";

                // Create a MySqlCommand with the INSERT query for Medication and connection
                using (MySqlCommand medicationCommand = new MySqlCommand(insertMedicationQuery, connection))
                {
                    medicationCommand.Parameters.AddWithValue("@Date", DateTime.Now); // Use the current date/time
                    medicationCommand.Parameters.AddWithValue("@PatientId", patientId);
                    medicationCommand.Parameters.AddWithValue("@NoOfDays", noOfDays);
                    medicationCommand.Parameters.AddWithValue("@Dose", dose);
                    medicationCommand.Parameters.AddWithValue("@MedicineId", medicineId);
                    medicationCommand.Parameters.AddWithValue("@PatientName", patientName);
                    medicationCommand.Parameters.AddWithValue("@MedicineName", medicineName);

                    // Execute the INSERT query for Medication
                    int rowsAffectedMedication = medicationCommand.ExecuteNonQuery();

                    if (rowsAffectedMedication > 0)
                    {
                        // Retrieve the ID of the last inserted Medication
                        long lastInsertedMedicationId = medicationCommand.LastInsertedId; // Use the correct method to get the last inserted ID (e.g., LastInsertedId or SELECT MAX(id) as lastId)

                        // Now, you can save the Payment information linked to this Medication and Patient
                        string insertPaymentQuery = "INSERT INTO payment (medicationId, patientId, paymentDate, price, type, status, patientName) " +
                                                    "VALUES (@MedicationId, @PatientId, @PaymentDate, @Price, @Type, @Status, @PatientName)";

                        using (MySqlCommand paymentCommand = new MySqlCommand(insertPaymentQuery, connection))
                        {
                            // Set parameters for the Payment query
                            paymentCommand.Parameters.AddWithValue("@MedicationId", lastInsertedMedicationId);
                            paymentCommand.Parameters.AddWithValue("@PatientId", patientId);
                            paymentCommand.Parameters.AddWithValue("@PaymentDate", DateTime.Now); // Use the current date/time
                            paymentCommand.Parameters.AddWithValue("@Price", price);
                            paymentCommand.Parameters.AddWithValue("@Type", "MEDICATION");
                            paymentCommand.Parameters.AddWithValue("@Status", "PENDING");
                            paymentCommand.Parameters.AddWithValue("@PatientName", patientName);

                            // Execute the INSERT query for Payment
                            int paymentRowsAffected = paymentCommand.ExecuteNonQuery();

                            if (paymentRowsAffected > 0)
                            {
                                MessageBox.Show("Medication and payment information saved successfully.");
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
                // Perform any additional actions you need (e.g., refreshing the UI)
            }
        }


        public DataTable LoadMedicationData()
        {
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();

                // Define the SQL query to retrieve medication data
                string query = "SELECT * FROM medication ORDER BY Id DESC";

                // Create a MySqlCommand with the query and connection
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Create a MySqlDataAdapter to fill the DataTable
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle connection or database-related errors here
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }



    }
}
