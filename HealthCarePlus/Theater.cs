using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HealthCarePlus
{
    public partial class Theater : Form
    {
        string con;
        MySqlConnection connection;
        public Theater()
        {
            InitializeComponent();
            // Add  items to the ComboBox
            cmbStatus.Items.Add("ACTIVE");
            cmbStatus.Items.Add("INATIVE");
            // Set a default or placeholder text
            cmbStatus.Text = "Select Status";
            // Add  items to the ComboBox
            cmbType.Items.Add("ROOM");
            cmbType.Items.Add("THEATER");
            // Set a default or placeholder text
            cmbType.Text = "Select Type";
            //Database connection
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            table_load();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            if (report == null)
            {
                report.Parent = this;
            }
            report.Show();
            this.Hide();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            Payment payment = new Payment();
            if (payment == null)
            {
                payment.Parent = this;
            }
            payment.Show();
            this.Hide();
        }

        private void BtnMedication_Click(object sender, EventArgs e)
        {
            Medication medication = new Medication();
            if (medication == null)
            {
                medication.Parent = this;
            }
            medication.Show();
            this.Hide();
        }

        private void btnResource_Click(object sender, EventArgs e)
        {
            Resource resource = new Resource();
            if (resource == null)
            {
                resource.Parent = this;
            }
            resource.Show();
            this.Hide();
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            Schedule schedule = new Schedule();
            if (schedule == null)
            {
                schedule.Parent = this;
            }
            schedule.Show();
            this.Hide();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            if (staff == null)
            {
                staff.Parent = this;
            }
            staff.Show();
            this.Hide();
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment();
            if (appointment == null)
            {
                appointment.Parent = this;
            }
            appointment.Show();
            this.Hide();
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            if (patient == null)
            {
                patient.Parent = this;
            }
            patient.Show();
            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            if (dashBoard == null)
            {
                dashBoard.Parent = this;
            }
            dashBoard.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtMax.Text)
                  || string.IsNullOrEmpty(txtSpecific.Text) || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString())
                  || string.IsNullOrEmpty(txtprice.Text) || string.IsNullOrEmpty(cmbType.SelectedItem.ToString()))
                {
                    MessageBox.Show("Please Fill All Required Field.");
                    return;
                }
                connection.Open();

                // Construct the INSERT query
                string insertQuery = "INSERT INTO theater (name, price, maxPatient, Specification, status, type) " +
                                     "VALUES (@Name, @Price, @MaxPatient, @Specification, @Status, @Type)";

                // Create a MySqlCommand with the INSERT query and connection
                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    // Set parameters for the query
                    insertCommand.Parameters.AddWithValue("@Name", txtName.Text);
                    insertCommand.Parameters.AddWithValue("@Price", txtprice.Text);
                    insertCommand.Parameters.AddWithValue("@MaxPatient", txtMax.Text);
                    insertCommand.Parameters.AddWithValue("@Specification", txtSpecific.Text);
                    insertCommand.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                    insertCommand.Parameters.AddWithValue("@Type", cmbType.SelectedItem.ToString());

                    // Execute the INSERT query
                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Theater information saved successfully.");
                       
                    }
                    else
                    {
                        MessageBox.Show("Failed to save theater information.");
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

        private void table_load()
        {
            try
            {
                // Create a connection to the database

                connection.Open();

                // Define the SQL query to retrieve theater data
                string selectQuery = "SELECT id, name, price, maxPatient, Specification, status, type FROM theater";

                // Create a MySqlCommand with the SELECT query and connection
                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                {
                    // Create a DataTable to store the results
                    var dataTable = new DataTable();

                    // Create a DataAdapter to fill the DataTable
                    using (var dataAdapter = new MySqlDataAdapter(selectCommand))
                    {
                        // Fill the DataTable with data from the database
                        dataAdapter.Fill(dataTable);
                    }

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
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
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtMax.Text)
                 || string.IsNullOrEmpty(txtSpecific.Text) || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString())
                 || string.IsNullOrEmpty(txtprice.Text) || string.IsNullOrEmpty(cmbType.SelectedItem.ToString()))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            try
            {
                connection.Open();

               
                // Construct the SQL UPDATE query
                string updateQuery = "UPDATE theater\r\n    SET\r\n        name = @Name,\r\n        price = @Price,\r\n        maxPatient = @MaxPatient,\r\n        Specification = @Specification,\r\n        status = @Status,\r\n        type = @Type\r\n    WHERE\r\n        id = @Id;";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Name", txtName.Text);
                    updateCommand.Parameters.AddWithValue("@Price", txtprice.Text);
                    updateCommand.Parameters.AddWithValue("@MaxPatient", txtprice.Text);
                    updateCommand.Parameters.AddWithValue("@Specification", txtSpecific.Text);
                    updateCommand.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                    updateCommand.Parameters.AddWithValue("@Type", cmbType.SelectedItem.ToString());
                    updateCommand.Parameters.AddWithValue("@Id", txtId.Text);


                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Theater updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update theater.");
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM theater WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string name = reader["name"].ToString();
                        string price = reader["price"].ToString();
                        string maxPatient = reader["maxPatient"].ToString();
                        string Specification = reader["Specification"].ToString();
                        string status = reader["status"].ToString();
                        string type = reader["type"].ToString();
                        
                        txtName.Text = name;
                        txtprice.Text = price;
                        txtMax.Text = maxPatient;
                        txtSpecific.Text = Specification;
                        cmbStatus.SelectedItem = status;
                        cmbType.SelectedItem = type;
                      
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {
            txtName.Text = "";
            txtprice.Text = "";
            txtMax.Text = "";
            txtSpecific.Text = "";
            cmbStatus.Text = "";
            cmbType.Text = "";
            txtId.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            TheaterPop theaterPop = new TheaterPop(txtId.Text,txtName.Text,txtprice.Text);
            if (theaterPop == null)
            {
                theaterPop.Parent = this;
            }
            theaterPop.Show();
            //this.Hide();
        }
    }
}
