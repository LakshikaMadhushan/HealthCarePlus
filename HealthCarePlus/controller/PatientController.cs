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


        //========================================================================
        public PatientReportData SearchPatientReport(string reportId)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM report  WHERE id = @Id;";

                using (MySqlCommand cmd = new MySqlCommand(searchQuery, connection))
                {
                    
                    cmd.Parameters.AddWithValue("@Id", reportId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           
                            PatientReportData reportData = new PatientReportData
                            {
                                FileName = reader["fileName"].ToString(),
                                Date = reader["date"].ToString(),
                                PatientName = reader["patientName"].ToString(),
                                PatientId = reader["patientId"].ToString(),
                                Remark = reader["remark"].ToString(),
                                Path = reader["path"].ToString(),
                                PdfData = (byte[])reader["fileData"]
                            };

                            return reportData;
                        }
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

            return null;
        }





        public bool UpdateReport(int reportId, string fileName, byte[] fileData, DateTime date, string patientName, string remark, int patientId, string path)
        {
            try
            {
                connection.Open();

               
                string updateQuery = "UPDATE report " +
                                     "SET fileName = @FileName, fileData = @FileData, date = @Date, " +
                                     "patientName = @PatientName, remark = @Remark, patientId = @PatientId, path = @Path " +
                                     "WHERE id = @ReportId";

                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@FileData", fileData);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@PatientName", patientName);
                    command.Parameters.AddWithValue("@Remark", remark);
                    command.Parameters.AddWithValue("@PatientId", patientId);
                    command.Parameters.AddWithValue("@Path", path);
                    command.Parameters.AddWithValue("@ReportId", reportId);

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



        public bool RegisterPatientReport(string fileName, byte[] fileData, DateTime date, string patientName, string remark, int patientId, string path)
        {
            try
            {
                connection.Open();

              
                string insertQuery = "INSERT INTO report (fileName, fileData, date, patientName, remark, patientId, path) " +
                                     "VALUES (@FileName, @FileData, @Date, @PatientName, @Remark, @PatientId, @Path)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@FileData", fileData);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@PatientName", patientName);
                    command.Parameters.AddWithValue("@Remark", remark);
                    command.Parameters.AddWithValue("@PatientId", patientId);
                    command.Parameters.AddWithValue("@Path", path);

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



        public DataTable LoadPatientReports(int patientId)
        {
            try
            {
                connection.Open();

                // SQL query to select data from the "report" table
                string selectQuery = "SELECT id, fileName, date, patientName, remark, patientId, path FROM report WHERE patientId=@Id";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", patientId);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
