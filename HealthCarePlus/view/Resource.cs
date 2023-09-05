using HealthCarePlus.controller;
using HealthCarePlus.model;
using HealthCarePlus.service;
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
        ResourceController resourceController;
        public Resource()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            //con = "Server=localhost;Database=mydatabase;Uid=root;Pwd='';";
            connection = new MySqlConnection(con);

            resourceController = new ResourceController(connection);
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
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPrice.Text) ||
       string.IsNullOrEmpty(cmbStatus.SelectedItem?.ToString()) || string.IsNullOrEmpty(cmbType.SelectedItem?.ToString()) ||
       string.IsNullOrEmpty(txtRemark.Text) || string.IsNullOrEmpty(dateRepaired.Text) ||
       string.IsNullOrEmpty(dateBuying.Text))
            {
                MessageBox.Show("Please Fill All Required Fields.");
                return;
            }

            // Create an instance of the ResourceRepository with the database connection
            

            // Call the method to insert a new resource
            bool insertionResult = resourceController.InsertResource(
                txtName.Text,
                cmbType.SelectedItem.ToString(),
                dateBuying.Value,
                Convert.ToDecimal(txtPrice.Text),
                cmbStatus.SelectedItem.ToString(),
                txtRemark.Text,
                dateRepaired.Value
            );

            if (insertionResult)
            {
                MessageBox.Show("Resource record inserted successfully.");
                table_load();
            }
            else
            {
                MessageBox.Show("Resource record insertion failed.");
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
            DataTable dataTable = resourceController.GetAllResources();

            if (dataTable != null)
            {
                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Failed to load resource data.");
            }
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

            if (!int.TryParse(txtId.Text, out int resourceId))
            {
                MessageBox.Show("Please enter a valid resource ID.");
                return;
            }

            Resources resource = resourceController.GetResourceById(resourceId);

            if (resource != null)
            {
                // Populate your UI controls with the resource data
                txtName.Text = resource.Name;
                cmbType.Text = resource.Type;
                cmbStatus.Text = resource.Status;
                dateBuying.Text = resource.BuyingDate.ToString();
                dateRepaired.Text = resource.RepairedDate.ToString();
                txtRemark.Text = resource.Remark;
                txtPrice.Text = resource.Price.ToString();
            }
            else
            {
                MessageBox.Show("Resource record not found.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtId.Text)
                    || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString())
                    || string.IsNullOrEmpty(cmbType.SelectedItem.ToString())
                    || string.IsNullOrEmpty(txtRemark.Text) || string.IsNullOrEmpty(dateRepaired.Text)
                    || string.IsNullOrEmpty(dateBuying.Text))
                {
                    MessageBox.Show("Please Fill All Required Field.");
                    return;
                }


                bool updated = resourceController.UpdateResource(
                    int.Parse(txtId.Text),
                    txtName.Text,
                    cmbType.Text,
                    dateBuying.Value,
                    decimal.Parse(txtPrice.Text),
                    cmbStatus.Text,
                    txtRemark.Text,
                    dateRepaired.Value
                );

                if (updated)
                {
                    MessageBox.Show("Resource record updated successfully.");
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
