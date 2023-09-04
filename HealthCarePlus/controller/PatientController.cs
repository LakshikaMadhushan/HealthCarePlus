using HealthCarePlus.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCarePlus.service
{
    internal class PatientController
    {
        private MySqlConnection connection;
        public PatientController(MySqlConnection connection) 
        {
            this.connection = connection;
        }


        public bool RegisterPatient(string name, string email, string address, string gender, string nic, string contactNo, string dateOfBirth)
        {
            try
            {
                connection.Open();

                string insertQuery = "INSERT INTO patient (name, email, address, gender, nic, contactNo, dateOfBirth) " +
                                     "VALUES (@Name, @Email, @Address, @Gender, @Nic, @ContactNo, @DateOfBirth)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Nic", nic);
                    command.Parameters.AddWithValue("@ContactNo", contactNo);
                    command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }



        public DataTable GetPatients()
        {
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                // Define the SQL query to select data
                string selectQuery = "SELECT id AS Id, name AS Name, email AS Email, dateOfBirth AS DOB, address AS Address, gender AS Gender, nic AS NIC, contactNo AS Contact FROM patient"; // Replace with your table name

                // Create a data adapter to execute the query and fill the DataTable
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection))
                {
                    adapter.Fill(dataTable);
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

            return dataTable;
        }


        public PatientData SearchPatientById(string patientId)
        {
            PatientData patientData = null;

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
                        patientData = new PatientData
                        {
                            Name = reader["name"].ToString(),
                            Email = reader["email"].ToString(),
                            Gender = reader["gender"].ToString(),
                            ContactNo = reader["contactNo"].ToString(),
                            NIC = reader["nic"].ToString(),
                            DateOfBirth = reader["dateOfBirth"].ToString(),
                            Address = reader["address"].ToString()
                        };
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

            return patientData;
        }





        public bool UpdatePatient(string id, string name, string email, string address, string gender, string nic, string contactNo, string dateOfBirth)
        {
            try
            {
                connection.Open();

                string updateQuery = "UPDATE patient " +
                                     "SET " +
                                     "    name = @Name," +
                                     "    email = @Email," +
                                     "    address = @Address," +
                                     "    gender = @Gender," +
                                     "    nic = @Nic," +
                                     "    contactNo = @ContactNo," +
                                     "    dateOfBirth = @DateOfBirth " +
                                     "WHERE id = @Id;";

                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Nic", nic);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
