using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCarePlus.controller
{
    public class AppointmentController
    {
        private MySqlConnection connection;
        public AppointmentController(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public string SearchPatientById(string patientId)
        {
            string patientName = string.Empty;

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
                        patientName = reader["name"].ToString();
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

            return patientName;
        }


        public bool SearchAppointmentById(string appointmentId, out string scheduleId, out string price, out string status)
        {
            scheduleId = price = status = string.Empty;

            try
            {
                connection.Open();

                string searchQuery = "SELECT * FROM appointment WHERE id = @Id;";
                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);
                cmd.Parameters.AddWithValue("@Id", appointmentId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        scheduleId = reader["scheduleId"].ToString();
                        price = reader["price"].ToString();
                        status = reader["status"].ToString();
                        return true; // Data found
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

            return false; // Data not found
        }


        public bool UpdateAppointmentStatus(string appointmentId, string status)
        {
            try
            {
                connection.Open();

                string updateQuery = "UPDATE appointment SET status = @Status WHERE id = @AppointmentId";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Status", status);
                    updateCommand.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    return rowsAffected > 0; // Return true if any rows were updated
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }





    }
    
}
