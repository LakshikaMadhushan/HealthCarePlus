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
using static HealthCarePlus.Schedule;

namespace HealthCarePlus
{
    public partial class Appointment : Form
    {
        string con;
        MySqlConnection connection;

        // Create a list of DoctorItem to hold the doctor data.
        List<DoctorItem> doctorList = new List<DoctorItem>();
        List<SheduleItem> schedulerList = new List<SheduleItem>();
        public Appointment()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            //con = "Server=localhost;Database=mydatabase;Uid=root;Pwd='';";
            connection = new MySqlConnection(con);
            //timePickerStart.Format = DateTimePickerFormat.Time;
            //timePickerStart.ShowUpDown = true;
            //timePickerEnd.Format = DateTimePickerFormat.Time;
            //timePickerEnd.ShowUpDown = true;

            cmbStatus.Items.Add("ACTIVE");
            cmbStatus.Items.Add("INACTIVE");
            // Set a default or placeholder text
            cmbStatus.Text = "Select Status";

            //load doctors
            doctor_load();
            //load tabe
            table_load();
        }

        private void label2_Click(object sender, EventArgs e)
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

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

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
            //cmbDate.Items.Clear();
            schedule_load();
        }
        private void schedule_load()
        {
            try
            {
                connection.Open();

                // Define your SQL query to retrieve active doctors.
                string query = "SELECT * FROM schedule WHERE userId = @Id AND status = 'ACTIVE' AND date >= CURDATE()";

                int selectedIndex = cmdDoctor.SelectedIndex;
                int selectedDoctorId = 0;
                string selectedDoctorName = null;
                if (selectedIndex >= 0)
                {
                    selectedDoctorId = doctorList[selectedIndex].Id;
                    selectedDoctorName = doctorList[selectedIndex].Name;
                    // Use the selectedDoctorId as needed.
                }

                // Create a MySqlCommand with the query and connection.
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", selectedDoctorId);
                    // Create a data reader to retrieve data from the database.
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        //Clear any existing items in the ComboBox.
                        //cmdDoctor.Items.Clear();

                        // Loop through the retrieved data.
                        while (reader.Read())
                        {
                            // Retrieve the id and name.
                            int id = reader.GetInt32("id");
                            string date = reader.GetString("date");

                            SheduleItem sheduleItem = new SheduleItem(id, date);
                            schedulerList.Add(sheduleItem);

                            cmbDate.Items.Add(sheduleItem);
                        }

                        // Optionally, set the selected index.
                        if (cmbDate.Items.Count > 0)
                        {
                            cmbDate.SelectedIndex = 0; // Select the first item.
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
        }
        public class SheduleItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public SheduleItem(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private void cmbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                // Define your SQL query to retrieve active doctors.
                string query = "SELECT s.startTime AS startTime,s.endTime AS endTime,s.price AS price, COUNT(a.id) AS appointmentCount\r\nFROM schedule AS s\r\nLEFT JOIN appointment AS a ON s.id = a.scheduleId AND a.status = 'ACTIVE'\r\nWHERE s.id = @Id\r\nGROUP BY s.id;";

                int selectedIndex = cmbDate.SelectedIndex;
                int selectedDateId = 0;

                if (selectedIndex >= 0)
                {
                    selectedDateId = schedulerList[selectedIndex].Id;

                    // Use the selectedDoctorId as needed.
                }

                // Create a MySqlCommand with the query and connection.
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", selectedDateId);
                    // Create a data reader to retrieve data from the database.
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        //Clear any existing items in the ComboBox.
                        //cmdDoctor.Items.Clear();

                        // Loop through the retrieved data.
                        while (reader.Read())
                        {
                            // Retrieve the id and name.
                            //int id = reader.GetInt32("id");
                            string startTime = reader.GetString("startTime");
                            string endTime = reader.GetString("endTime");
                            //int maxPatient = reader.GetInt32("maxPatient");
                            string price = reader.GetString("price");
                            //int countPatient = reader.GetInt32("countPatient");
                            int appointmentCount = reader.GetInt32("appointmentCount");

                            txtPrice.Text = price;
                            txtCount.Text = appointmentCount+"";
                            txtTime.Text = startTime + " To " + endTime;
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
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM patient WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string name = reader["name"].ToString();



                        txtName.Text = name;


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

            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtName.Text)
            || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(cmbDate.SelectedItem.ToString())
            || string.IsNullOrEmpty(cmdDoctor.SelectedItem.ToString())
            || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString()))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            connection.Open();
            // Start a SQL transaction.
            using (MySqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    // Define your SQL query to insert data into the "appointment" table.
                    string insertAppointmentQuery = "INSERT INTO appointment (scheduleId, userId, price, doctorName, date, status,patientId,patientName) " +
                                                    "VALUES (@ScheduleId, @UserId, @Price, @DoctorName, @Date, @Status,@PatientId,@PatientName)";

                    int selectedIndex = cmdDoctor.SelectedIndex;
                    int selectedIndexShedule = cmbDate.SelectedIndex;
                    int selectedDoctorId = 0;
                    int selectedSchedule = 0;
                    string selectedDoctorName = null;
                    if (selectedIndex >= 0)
                    {
                        selectedDoctorId = doctorList[selectedIndex].Id;
                        selectedDoctorName = doctorList[selectedIndex].Name;

                    }
                    if (selectedIndex >= 0)
                    {
                        selectedSchedule = schedulerList[selectedIndex].Id;

                        // Use the selectedDoctorId as needed.
                    }
                    // Create a MySqlCommand for the appointment table insert.
                    using (MySqlCommand insertAppointmentCommand = new MySqlCommand(insertAppointmentQuery, connection))
                    {
                        insertAppointmentCommand.Transaction = transaction;

                        // Set parameter values for the appointment table insert.
                        insertAppointmentCommand.Parameters.AddWithValue("@ScheduleId", selectedSchedule);
                        insertAppointmentCommand.Parameters.AddWithValue("@UserId", selectedDoctorId);
                        insertAppointmentCommand.Parameters.AddWithValue("@Price", txtPrice.Text);
                        insertAppointmentCommand.Parameters.AddWithValue("@DoctorName", selectedDoctorName);
                        insertAppointmentCommand.Parameters.AddWithValue("@Date", "");
                        insertAppointmentCommand.Parameters.AddWithValue("@Status", cmbStatus.Text);
                        insertAppointmentCommand.Parameters.AddWithValue("@PatientId", txtId.Text);
                        insertAppointmentCommand.Parameters.AddWithValue("@PatientName", txtName.Text);


                        // Execute the appointment table insert.
                        int rowsAffected = insertAppointmentCommand.ExecuteNonQuery();

                        //if (rowsAffected > 0)
                        //{
                        //    // Successfully inserted into the appointment table.

                        //    //update the "schedule" table by incrementing countPatient by 1.
                        //    string updateScheduleQuery = "UPDATE schedule SET countPatient = countPatient + 1 WHERE id = @ScheduleId";


                        //    using (MySqlCommand updateScheduleCommand = new MySqlCommand(updateScheduleQuery, connection))
                        //    {
                        //        updateScheduleCommand.Transaction = transaction;


                        //        updateScheduleCommand.Parameters.AddWithValue("@ScheduleId", selectedSchedule);


                        //        int rowsUpdated = updateScheduleCommand.ExecuteNonQuery();

                                //if (rowsUpdated > 0)
                                if (rowsAffected > 0)
                                {
                                    // Commit the transaction to save the appointment and update the schedule.
                                    transaction.Commit();

                                    // Now, insert a payment row related to this appointment.
                                    string insertPaymentQuery = "INSERT INTO payment (appointmentId, typeId, paymentDate, price, type, status, patientName,patientId) " +
                                                               "VALUES (@AppointmentId, @TypeId, @PaymentDate, @Price, @Type, @Status, @PatientName,@PatientId)";

                                    using (MySqlCommand insertPaymentCommand = new MySqlCommand(insertPaymentQuery, connection))
                                    {
                                        insertPaymentCommand.Parameters.AddWithValue("@AppointmentId", insertAppointmentCommand.LastInsertedId);
                                        insertPaymentCommand.Parameters.AddWithValue("@TypeId", 0); // Update the type as needed
                                        insertPaymentCommand.Parameters.AddWithValue("@PaymentDate", DateTime.Now.Date);
                                        insertPaymentCommand.Parameters.AddWithValue("@Price", txtPrice.Text);
                                        insertPaymentCommand.Parameters.AddWithValue("@Type", "APPOINTMENT");
                                        insertPaymentCommand.Parameters.AddWithValue("@Status", "PENDING");
                                        insertPaymentCommand.Parameters.AddWithValue("@PatientName", txtName.Text);
                                        insertPaymentCommand.Parameters.AddWithValue("@PatientId", txtId.Text);

                                        int rowsPaymentInserted = insertPaymentCommand.ExecuteNonQuery();

                                        if (rowsPaymentInserted > 0)
                                        {
                                            Console.WriteLine("Payment record inserted successfully.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to insert payment record.");
                                        }
                                    }
                                }
                        else
                        {
                            transaction.Rollback();
                            Console.WriteLine("Failed to update schedule.");
                        }
                        //}
                    }
                        //else
                        //{
                        //    transaction.Rollback();
                        //    Console.WriteLine("Failed to insert appointment.");
                        //}
                    //}

                    table_load();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Transaction error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
            private void btnSearch2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM appointment WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtAppointment.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string sheduleId = reader["scheduleId"].ToString();
                        string price = reader["price"].ToString();
                        string status = reader["status"].ToString();
                        cmbStatus.Text = "";
                        cmbStatus.SelectedText = status;    
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

        private void table_load()
        {
            try
            {
                // Create a connection to the database

                connection.Open();

                // Define the SQL query to select data
                string selectQuery = "SELECT\r\n    appointment.id,\r\n    appointment.scheduleId,\r\n  appointment.PatientId,\r\n  appointment.PatientName,\r\n    appointment.userId,\r\n    appointment.price,\r\n    appointment.doctorName,\r\n     appointment.status,\r\n    schedule.startTime,\r\n    schedule.endTime,\r\n    schedule.date AS scheduleDate\r\nFROM\r\n    appointment\r\nINNER JOIN\r\n    schedule\r\nON\r\n    appointment.scheduleId = schedule.id;";

                // Create a data adapter to execute the query and fill a DataSet
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "appointment"); // Replace with your table name


                    // Bind the DataGridView to a specific DataTable within the DataSet
                    dataGridView1.DataSource = dataSet.Tables["appointment"]; // Replace with your table name
                }

                // Close the connection
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }finally { 
                connection.Close(); 
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppointment.Text) || (cmbStatus.SelectedText==null)){
                MessageBox.Show("Please Fill All Required Field.");
                return;

            }
            try
            {
                connection.Open();

                // Define your SQL query to update the "status" in the "appointment" table.
                string updateQuery = "UPDATE appointment SET status = @Status WHERE id = @AppointmentId";

                // Create a MySqlCommand with the query and connection.
                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    // Set parameter values for the UPDATE query.
                    updateCommand.Parameters.AddWithValue("@Status", cmbStatus.Text);
                    updateCommand.Parameters.AddWithValue("@AppointmentId", txtAppointment.Text);

                    // Execute the UPDATE query.
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Update successful.");
                        connection.Close();
                        table_load();

                    }
                    else
                    {
                        Console.WriteLine("No records were updated.");
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
            //=====================

            //====================
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
         

            txtId.Text = "";
            txtPrice.Text = "";
            txtName.Text = "";
            cmbDate.Text = "";
            cmdDoctor.Text = "";
            cmbStatus.Text = "";
            txtCount.Text = "";
            txtAppointment.Text = "";

        }
    }
}
        