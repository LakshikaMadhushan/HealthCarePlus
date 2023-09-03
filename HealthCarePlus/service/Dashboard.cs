using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCarePlus.service
{
    internal class Dashboard
    {
        public class DoctorCountService
        {
            private MySqlConnection connection; // You should inject the connection or create it as needed.

            public DoctorCountService(MySqlConnection connection)
            {
                this.connection = connection;
            }

            public int GetActiveDoctorCount()
            {
                int userCount = 0;

                try
                {
                    connection.Open();

                    // Create the SQL SELECT query
                    string selectQuery = "SELECT COUNT(*) AS userCount " +
                                         "FROM user " +
                                         "WHERE status = 'ACTIVE' AND role = 'DOCTOR'";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userCount = Convert.ToInt32(reader["userCount"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return userCount;
            }
        }

    }
}
