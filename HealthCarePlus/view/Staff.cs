using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using HealthCarePlus.controller;
using HealthCarePlus.model;

namespace HealthCarePlus
{
    public partial class Staff : Form
    {
        string con;
        MySqlConnection connection;
        StaffController staffController;
        public Staff()
        {
            InitializeComponent();
            
            cmdRole.Items.Add("ADMIN");
            cmdRole.Items.Add("STAFF");
            cmdRole.Items.Add("DOCTOR");
            cmdStatus.Items.Add("ACTIVE");
            cmdStatus.Items.Add("INACTIVE");
            // Set a default or placeholder text
            cmdRole.Text = "Select Role";
            cmdStatus.Text = "Select Status";
            //Database connection
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            staffController=new StaffController(connection);
            table_load();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text)
                 || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(cmdStatus.SelectedItem.ToString())
                 || string.IsNullOrEmpty(txtNic.Text) || string.IsNullOrEmpty(txtContact.Text)
                 || string.IsNullOrEmpty(cmdRole.SelectedItem.ToString()) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }

                string name = txtName.Text;
                string email = txtEmail.Text;
                string address = txtAddress.Text;
                string qualification = txtQualification.Text;
                string status = cmdStatus.SelectedItem.ToString();
                string nic = txtNic.Text;
                string contact = txtContact.Text;
                string password = txtPassword.Text;
                string role = cmdRole.SelectedItem.ToString();

                // Call the RegisterUser method from the service class
                bool registrationResult = staffController.RegisterUser(name, email, address, qualification, status, nic, contact, password, role);

                if (registrationResult)
                {
                    MessageBox.Show("Staff record inserted successfully.");
                    // Clear input fields or perform other actions as needed.
                    table_load();
                    clearText();
                }
                else
                {
                    MessageBox.Show("Insertion failed.");
                }

            
        }
        

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
        private void table_load()
        {
            staffController.LoadDataIntoDataGridView(dataGridView1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                UserData user = staffController.GetUserById(txtId.Text);

                if (user != null)
                {
                    // Populate the form fields with user data
                    txtName.Text = user.Name;
                    txtEmail.Text = user.Email;
                    txtAddress.Text = user.Address;
                    cmdStatus.Text = user.Status;
                    txtNic.Text = user.Nic;
                    txtContact.Text = user.Contact;
                    cmdRole.Text = user.Role;
                    txtQualification.Text = user.Qualification;
                }
                else
                {
                    MessageBox.Show("Staff record not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || (string.IsNullOrEmpty(txtId.Text)) || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(cmdStatus.SelectedItem.ToString())
                || string.IsNullOrEmpty(txtNic.Text) || string.IsNullOrEmpty(txtContact.Text)
                || string.IsNullOrEmpty(cmdRole.SelectedItem.ToString()))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
          

          
            bool updateResult = staffController.UpdateUser(
                txtId.Text,
                txtName.Text,
                txtEmail.Text,
                txtAddress.Text,
                cmdStatus.SelectedItem.ToString(),
                txtNic.Text,
                txtContact.Text,
                cmdRole.SelectedItem.ToString(),
                txtPassword.Text,
                txtQualification.Text);

            if (updateResult)
            {
                MessageBox.Show("User updated successfully.");
                table_load();
            }
            else
            {
                MessageBox.Show("User update failed.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearText();
        }
        private void clearText()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            cmdStatus.Text = "";
            txtNic.Text = "";
            txtContact.Text = "";
            cmdRole.Text = "";
            txtPassword.Text = "";
            txtQualification.Text = "";
            txtId.Text = "";
        }
    }
}
