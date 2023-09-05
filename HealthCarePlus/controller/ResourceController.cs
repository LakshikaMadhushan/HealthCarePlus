using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCarePlus.model;
using System.Data;

namespace HealthCarePlus.controller
{
    internal class ResourceController
    {
        private MySqlConnection connection;
        public ResourceController(MySqlConnection connection)
        {
            this.connection = connection;
        }
            public Resources GetResourceById(int resourceId)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM resource WHERE id = @Id;";

                using (MySqlCommand cmd = new MySqlCommand(searchQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", resourceId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Data found for the given ID
                            Resources resource = new Resources
                            {
                                Name = reader["name"].ToString(),
                                Type = reader["type"].ToString(),
                                Status = reader["status"].ToString(),
                                BuyingDate = DateTime.Parse(reader["buyingDate"].ToString()),
                                RepairedDate = DateTime.Parse(reader["repairedDate"].ToString()),
                                Remark = reader["remark"].ToString(),
                                Price = decimal.Parse(reader["price"].ToString())
                            };

                            return resource;
                        }
                        else
                        {
                            // No data found for the given ID
                            return null;
                        }
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



        public bool UpdateResource(int resourceId, string name, string type, DateTime buyingDate, decimal price, string status, string remark, DateTime repairedDate)
        {
            try
            {
                connection.Open();
                string updateQuery = "UPDATE resource " +
                                    "SET name = @Name, type = @Type, buyingDate = @BuyingDate, price = @Price, status = @Status, remark = @Remark, repairedDate = @RepairedDate " +
                                    "WHERE id = @Id;";

                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Type", type);
                    command.Parameters.AddWithValue("@BuyingDate", buyingDate);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Remark", remark);
                    command.Parameters.AddWithValue("@RepairedDate", repairedDate);
                    command.Parameters.AddWithValue("@Id", resourceId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them or show an error message)
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


        public bool InsertResource(string name, string type, DateTime buyingDate, decimal price, string status, string remark, DateTime repairedDate)
        {
            try
            {
                connection.Open();

                // Define the INSERT query
                string insertQuery = "INSERT INTO resource (name, type, buyingDate, price, status, remark, repairedDate) " +
                                    "VALUES (@Name, @Type, @BuyingDate, @Price, @Status, @Remark, @RepairedDate)";

                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    // Set parameters
                    insertCommand.Parameters.AddWithValue("@Name", name);
                    insertCommand.Parameters.AddWithValue("@Type", type);
                    insertCommand.Parameters.AddWithValue("@BuyingDate", buyingDate);
                    insertCommand.Parameters.AddWithValue("@Price", price);
                    insertCommand.Parameters.AddWithValue("@Status", status);
                    insertCommand.Parameters.AddWithValue("@Remark", remark);
                    insertCommand.Parameters.AddWithValue("@RepairedDate", repairedDate);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
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



        public DataTable GetAllResources()
        {
            try
            {
                connection.Open();

                // Define the SQL query to retrieve resource data
                string query = "SELECT * FROM resource";

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

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle connection or database-related errors here
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
