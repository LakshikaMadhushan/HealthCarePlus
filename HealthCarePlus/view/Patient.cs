using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySql.Data.MySqlClient;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using HealthCarePlus.service;
using HealthCarePlus.model;

namespace HealthCarePlus
{
    public partial class Patient : Form
    {
        string con;
        MySqlConnection connection;
        PatientController patientController;

        public Patient()
        {
            InitializeComponent();
            // Add "Male" and "Female" items to the ComboBox
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            // Set a default or placeholder text
            cmbGender.Text = "Select Gender";
            //Database connection
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            patientController=new PatientController(connection);
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

        private void btnRegistration_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected item
            string selectedGender = cmbGender.SelectedItem.ToString();

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateInputSave())
            {
                MessageBox.Show("Please Fill All Required Fields.");
                return;
            }
            bool success = patientController.RegisterPatient(txtName.Text, txtEmail.Text, txtAddress.Text, cmbGender.SelectedItem.ToString(),
                txtNic.Text, txtContact.Text, dateBirth.Text);

            if (success)
            {
                MessageBox.Show("Patient record inserted successfully.");
                // Clear input fields or perform other actions as needed.
                table_load();
            }
            else
            {
                MessageBox.Show("Insertion failed.");
            }
        }

        private bool ValidateInputSave()
        {
            return !string.IsNullOrEmpty(txtName.Text) &&
                   !string.IsNullOrEmpty(txtEmail.Text) &&
                   !string.IsNullOrEmpty(txtAddress.Text) &&
                   !string.IsNullOrEmpty(cmbGender.SelectedItem?.ToString()) &&
                   !string.IsNullOrEmpty(txtNic.Text) &&
                   !string.IsNullOrEmpty(txtContact.Text) &&
                   !string.IsNullOrEmpty(dateBirth.Text);
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            
        }

        private void table_load()
        {
            DataTable patientsTable = patientController.GetPatients();

            dataGridView1.DataSource = patientsTable;
        }

        private void clearText()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text  = "";
            cmbGender.Text = "Select Gender";
            txtNic.Text= "";
            txtContact.Text= "";
            dateBirth.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            string patientId = txtId.Text;
            PatientData patientData = patientController.SearchPatientById(patientId);

            if (patientData != null)
            {
                // Data found for the given ID, populate the fields
                txtName.Text = patientData.Name;
                txtEmail.Text = patientData.Email;
                txtAddress.Text = patientData.Address;
                cmbGender.Text = patientData.Gender;
                txtNic.Text = patientData.NIC;
                txtContact.Text = patientData.ContactNo;
                dateBirth.Text = patientData.DateOfBirth;
            }
            else
            {
                // No data found for the given ID
                MessageBox.Show("Patient record not found.");
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text)
                 || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(cmbGender.SelectedItem.ToString())
                 || string.IsNullOrEmpty(txtNic.Text) || string.IsNullOrEmpty(txtContact.Text)
                 || string.IsNullOrEmpty(dateBirth.Text) || string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
     
            string id = txtId.Text;
            string name = txtName.Text;
            string email = txtEmail.Text;
            string address = txtAddress.Text;
            string gender = cmbGender.SelectedItem.ToString();
            string nic = txtNic.Text;
            string contactNo = txtContact.Text;
            string dateOfBirth = dateBirth.Value.ToString("yyyy-MM-dd");

            if (patientController.UpdatePatient(id, name, email, address, gender, nic, contactNo, dateOfBirth))
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

        private void btnPatientReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
            PatientPop patientPop = new PatientPop(txtId.Text,txtName.Text);
            if (patientPop == null)
            {
                patientPop.Parent = this;
            }
            patientPop.Show();
            //this.Hide();
        }

        private void dateBirth_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
