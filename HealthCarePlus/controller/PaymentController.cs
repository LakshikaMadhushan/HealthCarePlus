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
    public class PaymentController
    {
        private MySqlConnection connection;
        public PaymentController(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public bool SearchPaymentRecord(string paymentId, TextBox txtPId, TextBox txtPName, TextBox txtPrice, ComboBox cmbStatus, DateTimePicker txtPDate, ComboBox cmbType)
        {
            try
            {
               
                    connection.Open();
                    string searchQuery = "SELECT * FROM payment WHERE id = @Id;";

                    MySqlCommand cmd = new MySqlCommand(searchQuery, connection);
                    cmd.Parameters.AddWithValue("@Id", paymentId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Data found for the given ID
                            txtPId.Text = reader["patientId"].ToString();
                            txtPName.Text = reader["patientName"].ToString();
                            txtPrice.Text = reader["price"].ToString();
                            cmbStatus.Text = reader["status"].ToString();
                            txtPDate.Text = reader["paymentDate"].ToString();
                            cmbType.Text = reader["type"].ToString();

                            return true; // Record found
                        }
                        else
                        {
                            // No data found for the given ID
                            MessageBox.Show("Payment record not found.");
                            return false; // Record not found
                        }
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // An error occurred
            }finally { connection.Close(); }
        }




        public bool SearchPatientRecord(string patientId, TextBox txtPName)
        {
            try
            {
                
                    connection.Open();
                    string searchQuery = "SELECT * FROM patient WHERE id = @Id;";

                    MySqlCommand cmd = new MySqlCommand(searchQuery, connection);
                    cmd.Parameters.AddWithValue("@Id", patientId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Data found for the given ID
                            string name = reader["name"].ToString();
                            txtPName.Text = name;
                            return true; // Record found
                        }
                        else
                        {
                            // No data found for the given ID
                            MessageBox.Show("Patient record not found.");
                            return false; // Record not found
                        }
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // An error occurred
            }
            finally { connection.Close(); }
        }



        public bool UpdatePaymentRecord(string paymentId, string patientId, string typeId, DateTime paymentDate, string price, string type, string status, string patientName)
        {
            try
            {
                
                    connection.Open();

                    // Construct the SQL UPDATE statement
                    string updateQuery = "UPDATE payment " +
                                         "SET patientId = @PatientId, typeId = @TypeId, paymentDate = @PaymentDate, " +
                                         "price = @Price, type = @Type, status = @Status, patientName = @PatientName " +
                                         "WHERE id = @PaymentIdToUpdate";

                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@PaymentIdToUpdate", paymentId);
                        updateCommand.Parameters.AddWithValue("@PatientId", patientId);
                        updateCommand.Parameters.AddWithValue("@TypeId", typeId); // You can modify this based on your needs
                        updateCommand.Parameters.AddWithValue("@PaymentDate", paymentDate);
                        updateCommand.Parameters.AddWithValue("@Price", price);
                        updateCommand.Parameters.AddWithValue("@Type", type);
                        updateCommand.Parameters.AddWithValue("@Status", status);
                        updateCommand.Parameters.AddWithValue("@PatientName", patientName);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            return true; 
                        }
                        else
                        {
                            return false; 
                        }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false; // An error occurred
            }finally { connection.Close(); }
        }




        public bool InsertPaymentRecord(string patientId, string typeId, DateTime paymentDate, string price, string type, string status, string patientName)
        {
            try
            {
         
                    connection.Open();

                    // Construct the SQL INSERT statement
                    string insertQuery = "INSERT INTO payment (patientId, typeId, paymentDate, price, type, status, patientName) " +
                                         "VALUES (@PatientId, @TypeId, @PaymentDate, @Price, @Type, @Status, @PatientName)";

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@PatientId", patientId);
                        insertCommand.Parameters.AddWithValue("@TypeId", typeId); 
                        insertCommand.Parameters.AddWithValue("@PaymentDate", paymentDate);
                        insertCommand.Parameters.AddWithValue("@Price", price);
                        insertCommand.Parameters.AddWithValue("@Type", type);
                        insertCommand.Parameters.AddWithValue("@Status", status);
                        insertCommand.Parameters.AddWithValue("@PatientName", patientName);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            return true; 
                        }
                        else
                        {
                            return false; 
                        }
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }


        public DataTable LoadPaymentRecords(string patientId)
        {
            DataTable dataTable = new DataTable();

            try
            {
              
                    connection.Open();

                    // SQL query to select data from the "payment" table
                    string selectQuery = "SELECT * FROM payment WHERE patientId=@Id";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", patientId);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }

                    connection.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dataTable;
        }

        public DataTable LoadAllPaymentRecords()
        {
            DataTable dataTable = new DataTable();

            try
            {
               
                    connection.Open();

                    // SQL query to select data from the "payment" table
                    string selectQuery = "SELECT * FROM payment";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }

                    connection.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dataTable;
        }


    }
}
