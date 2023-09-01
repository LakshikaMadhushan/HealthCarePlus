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
    public partial class Resource : Form
    {
        string con;
        MySqlConnection connection;
        public Resource()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            //con = "Server=localhost;Database=mydatabase;Uid=root;Pwd='';";
            connection = new MySqlConnection(con);
            //cmb status
            cmbStatus.Items.Add("ACTIVE");
            cmbStatus.Items.Add("INACTIVE");
            cmbStatus.Items.Add("DELETED");
            // Set a default or placeholder text
            cmbStatus.Text = "Select Status";

            //cmb typeX-rays, MRIs, and CT scans etc.
            cmbType.Items.Add("XRAY");
            cmbType.Items.Add("MRIS");
            cmbType.Items.Add("CT");
            cmbType.Items.Add("OTHER");
            // Set a default or placeholder text
            cmbType.Text = "Select Type";
            table_load();
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

        private void btnTheaters_Click(object sender, EventArgs e)
        {
            Theater theater = new Theater();
            if (theater == null)
            {
                theater.Parent = this;
            }
            theater.Show();
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text)
                  || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString())
                   || string.IsNullOrEmpty(cmbType.SelectedItem.ToString())
                  || string.IsNullOrEmpty(txtRemark.Text) || string.IsNullOrEmpty(dateRepaired.Text)
                  || string.IsNullOrEmpty(dateBuying.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            // Define the INSERT query
            string insertQuery = "INSERT INTO resource (name, type, buyingDate, price, status, remark, repairedDate) " +
                                "VALUES (@Name, @Type, @BuyingDate, @Price, @Status, @Remark, @RepairedDate)";

            // Create a MySqlCommand with the INSERT query and connection
            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
            {
                // Set parameters
                insertCommand.Parameters.AddWithValue("@Name", txtName.Text);
                insertCommand.Parameters.AddWithValue("@Type", cmbType.Text);
                insertCommand.Parameters.AddWithValue("@BuyingDate", dateBuying.Value);
                insertCommand.Parameters.AddWithValue("@Price", txtPrice.Text);
                insertCommand.Parameters.AddWithValue("@Status", cmbStatus.Text);
                insertCommand.Parameters.AddWithValue("@Remark", txtRemark.Text);
                insertCommand.Parameters.AddWithValue("@RepairedDate", dateRepaired.Value);

                // Execute the INSERT query
                int rowsAffected = insertCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Resource record inserted successfully.");
                }
                else
                {
                    MessageBox.Show("Resource record inserted unsuccessfully.");
                }
               
                connection.Close();
                table_load();
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
        private void table_load()
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

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle connection or database-related errors here
                Console.WriteLine("Error: " + ex.Message);
            }finally { connection.Close(); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearText();
        }
        private void clearText()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtPrice.Text = "";
            txtRemark.Text = "";
            cmbStatus.Text = "Select Status";
            cmbType.Text = "Select Type";
            dateBuying.Text = "";
            dateRepaired.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM resource WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string name = reader["name"].ToString();
                        string type = reader["type"].ToString();
                        string status = reader["status"].ToString();
                        string buyingDate = reader["buyingDate"]+"";
                        string reparedDate = reader["repairedDate"]+"";
                        string remark = reader["remark"].ToString();
                        string price = reader["price"].ToString();


                        txtName.Text = name;
                        txtPrice.Text = price;
                        txtRemark.Text = remark;
                        cmbStatus.Text = status;
                        cmbType.Text = type;
                        dateBuying.Text =buyingDate ;
                        dateRepaired.Text =reparedDate ;

                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Resource record not found.");
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text)|| string.IsNullOrEmpty(txtId.Text) 
                    || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString())
                     || string.IsNullOrEmpty(cmbType.SelectedItem.ToString())
                    || string.IsNullOrEmpty(txtRemark.Text) || string.IsNullOrEmpty(dateRepaired.Text)
                    || string.IsNullOrEmpty(dateBuying.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            string updateQuery = " UPDATE resource\r\n    SET\r\n        name = @Name,\r\n        type = @Type,\r\n        buyingDate = @BuyingDate,\r\n        price = @Price,\r\n        status = @Status,\r\n        remark = @Remark,\r\n        repairedDate = @RepairedDate\r\n    WHERE\r\n        id = @Id;";


            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            {
                // Set parameters
                command.Parameters.AddWithValue("@Name", txtName.Text);
                command.Parameters.AddWithValue("@Type", cmbType.Text);
                command.Parameters.AddWithValue("@BuyingDate", dateBuying.Value);
                command.Parameters.AddWithValue("@Price", txtPrice.Text);
                command.Parameters.AddWithValue("@Status", cmbStatus.Text);
                command.Parameters.AddWithValue("@Remark", txtRemark.Text);
                command.Parameters.AddWithValue("@RepairedDate", dateRepaired.Value);
                command.Parameters.AddWithValue("@Id", txtId.Text);


                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Patient record updated successfully.");
                    clearText();
                    // Clear input fields or perform other actions as needed.
                    table_load();
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }
        }
    }
}
