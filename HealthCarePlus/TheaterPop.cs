using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HealthCarePlus
{
    public partial class TheaterPop : Form
    {
        string con;
        string tId;
        string tName;
        MySqlConnection connection;
        public TheaterPop()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            tId = "1";
            cmbStatus.Items.Add("ACTIVE");
            cmbStatus.Items.Add("INACTIVE");
            cmbStatus.Text = "Select Status";
            table_load();
        }

        public TheaterPop(string id,string name)
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            tId =id;
            tName = name;
            cmbStatus.Items.Add("ACTIVE");
            cmbStatus.Items.Add("INACTIVE");
            cmbStatus.Text = "Select Status";
            table_load();
            txtTId.Text = tId;
            txtType.Text = tName;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM patient WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtPId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string pName = reader["name"].ToString();
                        string PId = reader["id"].ToString();
                        txtPName.Text = pName;
                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Patient record not found.");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPId.Text) || string.IsNullOrEmpty(txtPName.Text) || string.IsNullOrEmpty(txtTId.Text)
                  || string.IsNullOrEmpty(txtCount.Text) || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString())
                  || string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtType.Text))
                {
                    MessageBox.Show("Please Fill All Required Field.");
                    return;
                }
                connection.Open();

                // Construct the INSERT query
                string insertQuery = "INSERT INTO theaterDetail (date, patientName, price, status, type, theaterId, patientId) " +
                                     "VALUES (@Date, @PatientName, @Price, @Status, @Type, @TheaterId, @PatientId)";

                // Create a MySqlCommand with the INSERT query and connection
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    // Set parameters for the query
                    command.Parameters.AddWithValue("@Date", txtDate.Value); 
                    command.Parameters.AddWithValue("@PatientName", txtPName.Text); 
                    command.Parameters.AddWithValue("@Price", txtPrice.Text);
                    command.Parameters.AddWithValue("@Status", cmbStatus.Text);
                    command.Parameters.AddWithValue("@Type",tName ); 
                    //command.Parameters.AddWithValue("@Type", txtType.Text); 
                    command.Parameters.AddWithValue("@TheaterId", tId); 
                    //command.Parameters.AddWithValue("@TheaterId", txtTId.Text); 
                    command.Parameters.AddWithValue("@PatientId", txtPId.Text);
                    // Execute the INSERT query
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Theater Details information saved successfully.");

                    }
                    else
                    {
                        MessageBox.Show("Failed to save theater details information.");
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
                table_load();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private int remainigPatientCount()
        {
            int remainingCapacity = 0;
           
            try
            {
               
                    connection.Open();

                    string query = "SELECT t.id, t.maxPatient - IFNULL(COUNT(td.id), 0) AS remainingCapacity " +
                                   "FROM theater t " +
                                   "LEFT JOIN theaterDetail td ON t.id = td.theaterId AND DATE(td.date) = @tdate " +
                                   "WHERE t.id = @theaterId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                    DateTime selectedDate = txtDate.Value;
                    string dateAsString = selectedDate.ToString("yyyy-MM-dd");
                   
                    command.Parameters.AddWithValue("@theaterId", txtTId.Text);
                        command.Parameters.AddWithValue("@tdate", dateAsString);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                remainingCapacity = Convert.ToInt32(reader["remainingCapacity"]);
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error: " + ex.Message);
            }
            connection.Close();
            return remainingCapacity;
        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            txtCount.Text= remainigPatientCount()+"";
        }

        private void table_load()
        {

            try
            {
                connection.Open();

                // SQL query to select data from the "report" table
                string selectQuery = "SELECT * FROM theaterdetail WHERE theaterId=@Id";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", tId);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date=null;
            try
            {
                
               connection.Open();
                string searchQuery = "SELECT * FROM theaterDetail WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        date = reader["date"].ToString();
                        string patientName = reader["patientName"].ToString();
                        string price = reader["price"].ToString();
                        string type = reader["type"].ToString();
                        string status = reader["status"].ToString();
                        string patientId = reader["patientId"].ToString();
                        string theaterId = reader["theaterId"].ToString();

                        txtPId.Text = patientId;
                        txtPName.Text = patientName;
                        txtTId.Text = theaterId;
                        cmbStatus.SelectedItem = status; 
                        
                        txtType.Text = type;
                        txtPrice.Text = price;
                       
                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Patient record not found.");
                    }
                }
                connection.Close();
                txtDate.Text = date;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
                txtCount.Text = remainigPatientCount() + "";
            }
        }

        private void clear()
        {
            txtPId.Text = "";
            txtId.Text = "";
            txtPName.Text = "";
            txtTId.Text = "";
            txtCount.Text = "";
            cmbStatus.Text = ""; 
            //txtDate.Value = DateTime.Now;
            txtType.Text = "";
            txtPrice.Text = "";
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPId.Text) || string.IsNullOrEmpty(txtPName.Text) || string.IsNullOrEmpty(txtTId.Text)
                    || string.IsNullOrEmpty(txtCount.Text) || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString())
                    || string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtType.Text))
                {
                    MessageBox.Show("Please Fill All Required Field.");
                    return;
                }

                connection.Open();

                // Construct the UPDATE query
                string updateQuery = "UPDATE theaterDetail " +
                                     "SET date = @Date, patientName = @PatientName, price = @Price, " +
                                     "status = @Status, type = @Type, theaterId = @TheaterId, patientId = @PatientId " +
                                     "WHERE id = @Id"; // Assuming id is the primary key of theaterDetail

                // Create a MySqlCommand with the UPDATE query and connection
                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    // Set parameters for the query
                    command.Parameters.AddWithValue("@Date", txtDate.Value);
                    command.Parameters.AddWithValue("@PatientName", txtPName.Text);
                    command.Parameters.AddWithValue("@Price", txtPrice.Text);
                    command.Parameters.AddWithValue("@Status", cmbStatus.Text);
                    command.Parameters.AddWithValue("@Type", txtType.Text); 
                    command.Parameters.AddWithValue("@TheaterId", txtTId.Text); 
                    command.Parameters.AddWithValue("@PatientId", txtPId.Text);
                    command.Parameters.AddWithValue("@Id", txtId.Text); 

                    // Execute the UPDATE query
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Theater Details information updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update theater details information. Please make sure the ID exists.");
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
                table_load();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
