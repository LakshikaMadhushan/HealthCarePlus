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
        public class DashBoardCountService
        {
            private MySqlConnection connection; // You should inject the connection or create it as needed.

            public DashBoardCountService(MySqlConnection connection)
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



            public int GetPatientCount()
            {
                int userCount = 0;

                try
                {
                    connection.Open();

                    // Create the SQL SELECT query
                    string selectQuery = "SELECT COUNT(*) AS userCount " +
                                         "FROM patient";

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


            public int GetStaffCount()
            {
                int userCount = 0;

                try
                {
                    connection.Open();

                    // Create the SQL SELECT query
                    string selectQuery = "SELECT COUNT(*) AS userCount " +
                                         "FROM user " +
                                         "WHERE status = 'ACTIVE' AND role NOT IN ('DOCTOR')";

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


            public int GetActiveRoomCount()
            {
                int roomCount = 0;

                try
                {
                    connection.Open();

                    // Create the SQL SELECT query
                    string selectQuery = "SELECT COUNT(*) AS roomCount " +
                                         "FROM theater WHERE status='ACTIVE' AND type='ROOM'";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                roomCount = Convert.ToInt32(reader["roomCount"]);
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

                return roomCount;
            }



            public int GetTodayAppointmentCount()
            {
                int appointmentCount = 0;

                try
                {
                    connection.Open();

                    // Get the current date
                    DateTime today = DateTime.Today;

                    // Create the SQL SELECT query
                    string selectQuery = "SELECT COUNT(*) AS appointmentCount " +
                                         "FROM appointment WHERE status='ACTIVE' AND  DATE(date) = @TodayDate";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TodayDate", today);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                appointmentCount = Convert.ToInt32(reader["appointmentCount"]);
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

                return appointmentCount;
            }

            public int GetActiveTheaterCount()
            {
                int theaterCount = 0;

                try
                {
                    connection.Open();

                    // Create the SQL SELECT query
                    string selectQuery = "SELECT COUNT(*) AS theaterCount " +
                                         "FROM theater WHERE status='ACTIVE' AND type='THEATER'";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                theaterCount = Convert.ToInt32(reader["theaterCount"]);
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

                return theaterCount;
            }
        }
    }
}
