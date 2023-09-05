using HealthCarePlus.controller;
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

namespace HealthCarePlus
{
    public partial class Report : Form
    {
        string con;
        MySqlConnection connection;
        ReportController reportController;
        public Report()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            reportController = new ReportController(connection);
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
            Staff staff= new Staff();
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

        private void btnIncomeSearch_Click(object sender, EventArgs e)
        {
            

            DateTime startDate = dateTimePickerB1.Value.Date;
            DateTime endDate = dateTimePickerB2.Value.Date;

            DataTable incomeTable = reportController.SearchIncome(startDate, endDate);

            if (incomeTable != null)
            {
                // Bind the DataTable to the DataGridView
                dataGridView2.DataSource = incomeTable;

                // Calculate the total price
                decimal totalPrice = 0;
                foreach (DataRow row in incomeTable.Rows)
                {
                    totalPrice += Convert.ToDecimal(row["price"]);
                }

                txtTotal.Text = "Total Income: $" + totalPrice.ToString("0.00");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
          

            DateTime startDate = dateTimePickerB1.Value.Date;
            DateTime endDate = dateTimePickerB2.Value.Date;

            DataTable allocationTable = reportController.SearchAllocations(startDate, endDate);

            if (allocationTable != null)
            {
                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = allocationTable;

                // Calculate the total price
                decimal totalPrice = 0;
                foreach (DataRow row in allocationTable.Rows)
                {
                    totalPrice += Convert.ToDecimal(row["price"]);
                }

                txtAllocation.Text = "Total Allocation: $" + totalPrice.ToString("0.00");
            }
        }
    }
}
