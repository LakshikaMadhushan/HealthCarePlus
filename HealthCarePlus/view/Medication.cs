using HealthCarePlus.controller;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCarePlus
{
    public partial class Medication : Form
    {
        string con;
        MySqlConnection connection;
        MedicationController medicationController;
        public Medication()
        {
            InitializeComponent();
              con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            //con = "Server=localhost;Database=mydatabase;Uid=root;Pwd='';";
            connection = new MySqlConnection(con);
            medicationController=new MedicationController(connection);
            //cmb status
            cmbDose.Items.Add("One Time");
            cmbDose.Items.Add("Two Time");
            cmbDose.Items.Add("Three TIme");
            // Set a default or placeholder text
            cmbDose.Text = "Select Dose";

            table2_load();
            table_load();

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void btnPayment_Click(object sender, EventArgs e)
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

        private void btnPayment_Click_1(object sender, EventArgs e)
        {
            Payment payment = new Payment();
            if (payment == null)
            {
                payment.Parent = this;
            }
            payment.Show();
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

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNames.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            string medicineName = txtNames.Text; // Get the medicine name from a TextBox
            medicationController.RegisterMedicine(medicineName);
            table_load();
        }
        private void table_load()
        {
            medicationController.LoadMedicineData(dataGridView2);

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void table2_load()
        {
            //try
            //{
            //    connection.Open();

            //    // Define the SQL query to retrieve resource data
            //    string query = "SELECT * FROM medication ORDER BY Id DESC";

            //    // Create a MySqlCommand with the query and connection
            //    using (MySqlCommand command = new MySqlCommand(query, connection))
            //    {
            //        // Create a DataTable to hold the retrieved data
            //        DataTable dataTable = new DataTable();

            //        // Create a MySqlDataAdapter to fill the DataTable
            //        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            //        {
            //            adapter.Fill(dataTable);
            //        }

            //        // Bind the DataTable to the DataGridView
            //        dataGridView1.DataSource = dataTable;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Handle connection or database-related errors here
            //    Console.WriteLine("Error: " + ex.Message);
            //}
            //finally { connection.Close(); }

            DataTable dataTable = medicationController.LoadMedicationData();

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;


        }

        

        private void btnSrch_Click(object sender, EventArgs e)
        {
            medicationController.SearchMedicineById(txtIds, txtNames);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNames.Text) || string.IsNullOrEmpty(txtIds.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            string medicineName = txtNames.Text;
            string medicineId = txtIds.Text;

            medicationController.UpdateMedicine(medicineName, medicineId);
            table_load();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear_text();
        }

        private void clear_text()
        {
            txtId.Text = "";
            txtIds.Text = "";
            txtMId.Text = "";
            txtName.Text = "";
            txtNames.Text = "";
            txtDate.Text = "";
            cmbDose.Text = "Select Dose";
            txtCount.Text = "";
            table2_load();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtNames.Text) || string.IsNullOrEmpty(txtPrice.Text)
                    || string.IsNullOrEmpty(txtIds.Text) || string.IsNullOrEmpty(cmbDose.SelectedItem.ToString())
                    || string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtCount.Text))
                {
                    MessageBox.Show("Please Fill All Required Field.");
                    return;
                }
                //    connection.Open();

                //    // Construct the INSERT query for Medication
                //    string insertMedicationQuery = "INSERT INTO medication (date, patientId, noOfDays, dose, medicineId, patientName, medicineName) " +
                //                                 "VALUES (@Date, @PatientId, @NoOfDays, @Dose, @MedicineId, @PatientName, @MedicineName)";

                //    // Create a MySqlCommand with the INSERT query for Medication and connection
                //    using (MySqlCommand medicationCommand = new MySqlCommand(insertMedicationQuery, connection))
                //    {
                //        medicationCommand.Parameters.AddWithValue("@Date", txtDate.Value);
                //        medicationCommand.Parameters.AddWithValue("@PatientId", txtId.Text);
                //        medicationCommand.Parameters.AddWithValue("@NoOfDays", txtCount.Text);
                //        medicationCommand.Parameters.AddWithValue("@Dose", cmbDose.Text);
                //        medicationCommand.Parameters.AddWithValue("@MedicineId", txtIds.Text);
                //        medicationCommand.Parameters.AddWithValue("@PatientName", txtName.Text);
                //        medicationCommand.Parameters.AddWithValue("@MedicineName", txtNames.Text);

                //        // Execute the INSERT query for Medication
                //        int rowsAffectedMedication = medicationCommand.ExecuteNonQuery();

                //        if (rowsAffectedMedication > 0)
                //        {
                //            MessageBox.Show("Medication data saved successfully.");

                //            // Retrieve the ID of the last inserted Medication
                //            long lastInsertedMedicationId = medicationCommand.LastInsertedId; // Use the correct method to get the last inserted ID (e.g., LastInsertedId or SELECT MAX(id) as lastId)

                //            // Now, you can save the Payment information linked to this Medication and Patient
                //            string insertPaymentQuery = "INSERT INTO payment (medicationId, patientId, paymentDate, price, type, status, patientName) " +
                //                                        "VALUES (@MedicationId, @PatientId, @PaymentDate, @Price, @Type, @Status, @PatientName)";

                //            using (MySqlCommand paymentCommand = new MySqlCommand(insertPaymentQuery, connection))
                //            {
                //                // Set parameters for the Payment query
                //                paymentCommand.Parameters.AddWithValue("@MedicationId", lastInsertedMedicationId);
                //                paymentCommand.Parameters.AddWithValue("@PatientId", txtId.Text);
                //                paymentCommand.Parameters.AddWithValue("@PaymentDate", DateTime.Now); 
                //                paymentCommand.Parameters.AddWithValue("@Price", txtPrice.Text);
                //                paymentCommand.Parameters.AddWithValue("@Type", "MEDICATION"); 
                //                paymentCommand.Parameters.AddWithValue("@Status", "PENDING"); 
                //                paymentCommand.Parameters.AddWithValue("@PatientName", txtName.Text);

                //                // Execute the INSERT query for Payment
                //                int paymentRowsAffected = paymentCommand.ExecuteNonQuery();

                //                if (paymentRowsAffected > 0)
                //                {
                //                    MessageBox.Show("Payment information saved successfully.");
                //                }
                //                else
                //                {
                //                    MessageBox.Show("Failed to save payment information.");
                //                }
                //            }
                //        }
                //        else
                //        {
                //            MessageBox.Show("Failed to save medication data.");
                //        }
                //    }

                string patientId = txtId.Text;
                string medicineId = txtIds.Text;
                string medicineName = txtNames.Text;
                string patientName = txtName.Text;
                string dose = cmbDose.SelectedItem.ToString();
                string noOfDays = txtCount.Text;
                decimal price = Convert.ToDecimal(txtPrice.Text);

                medicationController.RegisterMedicationAndPayment(patientId, medicineId, medicineName, patientName, dose, noOfDays, price);




            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                table2_load();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            medicationController.SearchPatientAndMedication(txtId.Text, txtName, dataGridView1);
        }

        private void btnMSearch_Click(object sender, EventArgs e)
        {
            medicationController.SearchMedication(txtMId.Text, txtNames, txtName, txtId, txtIds, txtDate, txtCount, cmbDose);
        }
        private void btnClear2_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtIds.Text = "";
            txtMId.Text = "";
            txtName.Text = "";
            txtNames.Text = "";
            txtDate.Text = "";
            cmbDose.Text = "Select Dose";
            txtCount.Text = "";
            table2_load();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtNames.Text) || string.IsNullOrEmpty(txtId.Text)
                   || string.IsNullOrEmpty(txtIds.Text) || string.IsNullOrEmpty(cmbDose.SelectedItem.ToString())
                   || string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtCount.Text) || string.IsNullOrEmpty(txtMId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            string id = txtMId.Text;
            string patientId = txtId.Text;
            string medicineId = txtIds.Text;
            string medicineName = txtNames.Text;
            string patientName = txtName.Text;
            string dose = cmbDose.SelectedItem.ToString();
            string noOfDays = txtCount.Text;
            DateTime date = txtDate.Value;

            medicationController.UpdateMedicationRecord(id, patientId, medicineId, medicineName, patientName, dose, noOfDays, date);

        }
    }
}
