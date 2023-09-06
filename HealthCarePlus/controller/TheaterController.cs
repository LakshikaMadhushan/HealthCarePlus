using HealthCarePlus.model;
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
    public class TheaterController
    {
        private MySqlConnection connection;


        public TheaterController(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public TheaterData GetTheaterById(int theaterId)
        {
            try
            {
                connection.Open();
                string selectQuery = "SELECT * FROM theater WHERE id = @Id;";

                MySqlCommand cmd = new MySqlCommand(selectQuery, connection);
                cmd.Parameters.AddWithValue("@Id", theaterId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TheaterData
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Price = reader.GetDecimal("price"),
                            MaxPatient = reader.GetInt32("maxPatient"),
                            Specification = reader.GetString("Specification"),
                            Status = reader.GetString("status"),
                            Type = reader.GetString("type")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching theater data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return null; // Theater not found
        }



        public bool UpdateTheater(
        string theaterId, string name, string price, string maxPatient, string specification,
        string status, string type)
        {
            try
            {
                connection.Open();

                string updateQuery = "UPDATE theater " +
                    "SET name = @Name, price = @Price, maxPatient = @MaxPatient, " +
                    "Specification = @Specification, status = @Status, type = @Type " +
                    "WHERE id = @Id;";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Name", name);
                    updateCommand.Parameters.AddWithValue("@Price", price);
                    updateCommand.Parameters.AddWithValue("@MaxPatient", maxPatient);
                    updateCommand.Parameters.AddWithValue("@Specification", specification);
                    updateCommand.Parameters.AddWithValue("@Status", status);
                    updateCommand.Parameters.AddWithValue("@Type", type);
                    updateCommand.Parameters.AddWithValue("@Id", theaterId);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

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



        public bool RegisterTheater(string name, string price, string maxPatient, string specification,
       string status, string type)
        {
            try
            {
                connection.Open();

                string insertQuery = "INSERT INTO theater (name, price, maxPatient, Specification, status, type) " +
                                     "VALUES (@Name, @Price, @MaxPatient, @Specification, @Status, @Type)";

                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Name", name);
                    insertCommand.Parameters.AddWithValue("@Price", price);
                    insertCommand.Parameters.AddWithValue("@MaxPatient", maxPatient);
                    insertCommand.Parameters.AddWithValue("@Specification", specification);
                    insertCommand.Parameters.AddWithValue("@Status", status);
                    insertCommand.Parameters.AddWithValue("@Type", type);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

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




        public DataTable LoadTheaterData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                string selectQuery = "SELECT id, name, price, maxPatient, Specification, status, type FROM theater";

                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(selectCommand))
                    {
                        dataAdapter.Fill(dataTable);
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

            return dataTable;
        }
    }
}
