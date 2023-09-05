using HealthCarePlus.model;
using HealthCarePlus.service;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCarePlus.controller
{
    internal class StaffController
    {
        private MySqlConnection connection;
        Auth auth;
        public StaffController(MySqlConnection connection)
        {
            this.connection = connection;
            auth = new Auth();
        }
        public UserData GetUserById(string userId)
        {
            try
            {
                connection.Open();

                // Define the SQL query to retrieve user data by ID
                string query = "SELECT * FROM user WHERE id = @Id;";

                // Create a MySqlCommand with the query and connection
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Data found for the given ID
                            return new UserData
                            {
                                Id = reader["id"].ToString(),
                                Name = reader["name"].ToString(),
                                Email = reader["email"].ToString(),
                                Contact = reader["contact"].ToString(),
                                Nic = reader["nic"].ToString(),
                                Qualification = reader["qualification"].ToString(),
                                Address = reader["address"].ToString(),
                                Role = reader["role"].ToString(),
                                Status = reader["status"].ToString()
                            };
                        }
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

            return null; // User not found
        }




        public bool UpdateUser(
        string id,
        string name,
        string email,
        string address,
        string status,
        string nic,
        string contactNo,
        string role,
        string password,
        string qualification)
        {
            try
            {
                connection.Open();
                string updateQuery = null;

                if (string.IsNullOrEmpty(password))
                {
                    updateQuery = "UPDATE user " +
                                  "SET name = @Name, email = @Email, address = @Address, " +
                                  "qualification = @Qualification, status = @Status, nic = @Nic, " +
                                  "contact = @ContactNo, role = @Role " +
                                  "WHERE id = @Id;";
                }
                else
                {
                    updateQuery = "UPDATE user " +
                                  "SET name = @Name, email = @Email, address = @Address, " +
                                  "qualification = @Qualification, status = @Status, nic = @Nic, " +
                                  "contact = @ContactNo, password = @Password, role = @Role " +
                                  "WHERE id = @Id;";
                }

                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Nic", nic);
                    command.Parameters.AddWithValue("@ContactNo", contactNo);
                    command.Parameters.AddWithValue("@Role", role);

                    if (!string.IsNullOrEmpty(password))
                    {
                        // Hash the password before setting it
                        string hashedPassword = auth.HashPassword(password);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                    }

                    command.Parameters.AddWithValue("@Qualification", qualification);
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }




        public bool RegisterUser(string name, string email, string address, string qualification, string status, string nic, string contact, string password, string role)
        {
            try
            {
                connection.Open();

                string insertQuery = "INSERT INTO user (name, email, address, qualification, status, nic, contact, password, role) " +
                                     "VALUES (@Name, @Email, @Address, @Qualification, @Status, @Nic, @ContactNo, @Password, @Role)";

                MySqlCommand command = new MySqlCommand(insertQuery, connection);
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Nic", nic);
                    command.Parameters.AddWithValue("@ContactNo", contact);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Password", auth.HashPassword(password));
                    command.Parameters.AddWithValue("@Qualification", qualification);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true; // Registration successful
                    }
                    else
                    {
                        return false; // Registration failed
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here or log them
                return false; // Registration failed
            }
            finally
            {
                connection.Close();
            }
        }



        public void LoadDataIntoDataGridView(System.Windows.Forms.DataGridView dataGridView)
        {
            try
            {
                connection.Open();

                string selectQuery = "SELECT id AS Id, name AS Name, email AS Email, status AS Status, " +
                    "address AS Address, role AS Role, nic AS NIC, contact AS Contact FROM user";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "user");

                    dataGridView.DataSource = dataSet.Tables["user"];
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
