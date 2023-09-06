using HealthCarePlus.controller;
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
using System.Xml.Linq;
using static HealthCarePlus.Schedule;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HealthCarePlus
{
    public partial class Schedule : Form
    {
        string con;
        MySqlConnection connection;
        ScheduleController scheduleController;
       
        // Create a list of DoctorItem to hold the doctor data.
        List<DoctorItem> doctorList = new List<DoctorItem>();

        public Schedule()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            //con = "Server=localhost;Database=mydatabase;Uid=root;Pwd='';";
            connection = new MySqlConnection(con);
             scheduleController = new ScheduleController(connection);
            timePickerStart.Format = DateTimePickerFormat.Time;
            timePickerStart.ShowUpDown = true;
            timePickerEnd.Format = DateTimePickerFormat.Time;
            timePickerEnd.ShowUpDown = true;

            cmdStatus.Items.Add("ACTIVE");
            cmdStatus.Items.Add("INACTIVE");
            // Set a default or placeholder text
            cmdStatus.Text = "Select Status";

            //load doctors
            doctor_load();
            //load tabe
            table_load();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
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

        private void Schedule_Load(object sender, EventArgs e)
        {
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtMax.Text)
                || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(cmdDoctor.SelectedItem.ToString())
                || string.IsNullOrEmpty(timePickerStart.Text) || string.IsNullOrEmpty(timePickerEnd.Text)
                || string.IsNullOrEmpty(cmdStatus.SelectedItem.ToString()))
                {
                    MessageBox.Show("Please Fill All Required Field.");
                    return;
                }
                connection.Open();
                string insertQuery = "INSERT INTO schedule (userId, startTime, endTime, status, date, price, maxPatient,userName) " +
                                     "VALUES (@UserId, @StartTime, @EndTime, @Status, @Date, @Price, @MaxPatient,@UserName)";
                
                int selectedIndex = cmdDoctor.SelectedIndex;
                int selectedDoctorId = 0;
                string selectedDoctorName =null;
                if (selectedIndex >= 0)
                {
                     selectedDoctorId = doctorList[selectedIndex].Id;
                     selectedDoctorName = doctorList[selectedIndex].Name;
                    // Use the selectedDoctorId as needed.
                }

                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                {
                    // Set parameter values.
                    insertCommand.Parameters.AddWithValue("@UserId", selectedDoctorId); 
                    insertCommand.Parameters.AddWithValue("@UserName", selectedDoctorName); 
                    insertCommand.Parameters.AddWithValue("@StartTime", timePickerStart.Text);
                    insertCommand.Parameters.AddWithValue("@EndTime", timePickerEnd.Text);
                    insertCommand.Parameters.AddWithValue("@Status", cmdStatus.SelectedItem);
                    insertCommand.Parameters.AddWithValue("@Date", txtDate.Value);
                    insertCommand.Parameters.AddWithValue("@Price", txtPrice.Text);
                    insertCommand.Parameters.AddWithValue("@MaxPatient",txtMax.Text);


                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Shedule record inserted successfully.");
                        connection.Close();
                        // Clear input fields or perform other actions as needed.
                        //table_load();
                    }
                    else
                    {
                        MessageBox.Show("Insertion failed.");
                        connection.Close();
                    }
                    table_load();

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


        private void doctor_load()
        {
            try
            {
                connection.Open();

                // Define your SQL query to retrieve active doctors.
                string query = "SELECT id, name FROM user WHERE role = 'Doctor' AND status = 'ACTIVE'";

                // Create a MySqlCommand with the query and connection.
                using (MySqlCommand command = new MySqlCommand(query, connection))
                // Create a data reader to retrieve data from the database.
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Clear any existing items in the ComboBox.
                    cmdDoctor.Items.Clear();

                    // Loop through the retrieved data.
                    while (reader.Read())
                    {
                        // Retrieve the id and name.
                        int id = reader.GetInt32("id");
                        string name = reader.GetString("name");

                        // Create a DoctorItem and add it to the list.
                        DoctorItem doctor = new DoctorItem(id, name);
                        doctorList.Add(doctor);

                        // Add the doctor to the ComboBox.
                        cmdDoctor.Items.Add(doctor);
                    }

                    // Optionally, set the selected index.
                    if (cmdDoctor.Items.Count > 0)
                    {
                        cmdDoctor.SelectedIndex = 0; // Select the first item.
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
        }

     
        private void cmdDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        // Define a custom class to hold id and name.
        public class DoctorItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public DoctorItem(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }


        private void table_load()
        {
            //try
            //{
            //    // Create a connection to the database

            //    connection.Open();

            //    // Define the SQL query to select data
            //    string selectQuery = "SELECT id AS Id,userId AS DoctorId,userName AS DoctorName,startTime AS StartTime,endTime AS EndTime,status AS Status, date AS Date,price AS Price,maxPatient AS MaxPatient FROM schedule"; // Replace with your table name

            //    // Create a data adapter to execute the query and fill a DataSet
            //    using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection))
            //    {
            //        DataSet dataSet = new DataSet();
            //        adapter.Fill(dataSet, "schedule"); // Replace with your table name


            //        // Bind the DataGridView to a specific DataTable within the DataSet
            //        dataGridView1.DataSource = dataSet.Tables["schedule"]; // Replace with your table name
            //    }

            //    // Close the connection
            //    connection.Close();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}

            scheduleController.LoadData(dataGridView1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM schedule WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        
                        // Data found for the given ID
                        string doctorId = GetDoctorNameById(reader.GetInt32(reader.GetOrdinal("userId")));
                        string startTime = reader["startTime"].ToString();
                        string endTime = reader["endTime"].ToString();
                        string status = reader["status"].ToString();
                        string date = reader["date"].ToString();
                        string max = reader["maxPatient"].ToString();
                        string price = reader["price"].ToString();




                        timePickerStart.Text = startTime;
                        timePickerEnd.Text=endTime;
                        cmdStatus.Text = status;
                        txtDate.Text = date;
                        txtPrice.Text=price; 
                        txtMax.Text=max;
                  



                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Schedule record not found.");
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

        // Function to get the doctor's name by ID
        private string GetDoctorNameById(int doctorId)
        {
            // Search for the doctor with the provided ID in the doctorList
            DoctorItem doctor = doctorList.Find(item => item.Id == doctorId);

            // Check if the doctor with the given ID was found
            if (doctor != null)
            {
                return doctor.Name;
            }
            else
            {
                // Handle the case where the doctor with the provided ID was not found
                MessageBox.Show("Doctor record not found.");
                return "0";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           
            clearText();       
        }
        private void clearText()
        {
            timePickerStart.Text = "";
            timePickerEnd.Text = "";
            cmdStatus.Text = "";
            txtDate.Text = "";
            txtPrice.Text = "";
            txtMax.Text = "";
            txtId.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtMax.Text)
                || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(cmdDoctor.SelectedItem.ToString())
                || string.IsNullOrEmpty(timePickerStart.Text) || string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(timePickerEnd.Text)
                || string.IsNullOrEmpty(cmdStatus.SelectedItem.ToString()))
                {
                    MessageBox.Show("Please Fill All Required Field.");
                    return;
                }
                connection.Open();
                string insertQuery = "UPDATE schedule " +
                             "SET userId = @UserId, " +
                             "startTime = @StartTime, " +
                             "endTime = @EndTime, " +
                             "status = @Status, " +
                             "date = @Date, " +
                             "price = @Price, " +
                             "maxPatient = @MaxPatient, " +
                             "userName = @UserName " +
                             "WHERE id = @Id";

                int selectedIndex = cmdDoctor.SelectedIndex;
                int selectedDoctorId = 0;
                string selectedDoctorName = null;
                if (selectedIndex >= 0)
                {
                    selectedDoctorId = doctorList[selectedIndex].Id;
                    selectedDoctorName = doctorList[selectedIndex].Name;
                    // Use the selectedDoctorId as needed.
                }

                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                {
                    // Set parameter values.
                    insertCommand.Parameters.AddWithValue("@UserId", selectedDoctorId);
                    insertCommand.Parameters.AddWithValue("@StartTime", timePickerStart.Text);
                    insertCommand.Parameters.AddWithValue("@EndTime", timePickerEnd.Text);
                    insertCommand.Parameters.AddWithValue("@Status", cmdStatus.SelectedItem);
                    insertCommand.Parameters.AddWithValue("@Date", txtDate.Value);
                    insertCommand.Parameters.AddWithValue("@Price", txtPrice.Text);
                    insertCommand.Parameters.AddWithValue("@MaxPatient", txtMax.Text);
                    insertCommand.Parameters.AddWithValue("@Id", txtId.Text);
                    insertCommand.Parameters.AddWithValue("@UserName", selectedDoctorName);


                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Shedule record updated successfully.");
                        connection.Close();
                        // Clear input fields or perform other actions as needed.
                        //table_load();
                    }
                    else
                    {
                        MessageBox.Show("updated failed.");
                        connection.Close();
                    }
                    table_load();

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
    }
}
