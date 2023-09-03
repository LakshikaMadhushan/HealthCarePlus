using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HealthCarePlus.service.Dashboard;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HealthCarePlus
{
    public partial class DashBoard : Form
    {
        string con;
        MySqlConnection connection;
        public DashBoard()
        {
            InitializeComponent();

            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            GetActiveDoctorCount();
            GetPatientCount();
            GetStaffCount();
            GetActiveRoomCount();
            GetTodayAppointment();
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

        private void picLogout_Click(object sender, EventArgs e)
        {
            login l = new login();
            if (l == null)
            {
                l.Parent = this;
            }
            l.Show();
            this.Hide();
        }

        private void GetActiveDoctorCount()
        {
            //int userCount = 0;

            //    try
            //    {
            //        connection.Open();

            //        // Create the SQL SELECT query
            //        string selectQuery = "SELECT COUNT(*) AS userCount " +
            //                             "FROM user " +
            //                             "WHERE status = 'ACTIVE' AND role = 'DOCTOR'";

            //        using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
            //        {
            //            using (MySqlDataReader reader = command.ExecuteReader())
            //            {
            //                if (reader.Read())
            //                {
            //                    userCount = Convert.ToInt32(reader["userCount"]);
            //                }
            //            }
            //        }
            //    connection.Close();
            //}
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Error: " + ex.Message);
            //    }

            //lblDoctor.Text = userCount.ToString();


            DoctorCountService doctorCountService = new DoctorCountService(connection);
            int doctorCount = doctorCountService.GetActiveDoctorCount();
            lblDoctor.Text = doctorCount.ToString();

        }
        private void GetPatientCount()
        {
            int userCount = 0;

            try
            {
                connection.Open();

                // Create the SQL SELECT query
                string selectQuery = "SELECT COUNT(*) AS userCount " +
                                     "FROM patient";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userCount = Convert.ToInt32(reader["userCount"]);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            lblPatient.Text = userCount.ToString();

        }

        private void GetStaffCount()
        {
            int userCount = 0;

            try
            {
                connection.Open();

                // Create the SQL SELECT query
                string selectQuery = "SELECT COUNT(*) AS userCount " +
                                         "FROM user " +
                                         "WHERE status = 'ACTIVE' AND role NOT IN ('DOCTOR')";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userCount = Convert.ToInt32(reader["userCount"]);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            lblStaff.Text = userCount.ToString();

        }

        private void GetActiveRoomCount()
        {
            int userCount = 0;

            try
            {
                connection.Open();

                // Create the SQL SELECT query
                string selectQuery = "SELECT COUNT(*) AS count " +
                                     "FROM theater WHERE status='ACTIVE' AND type='ROOM'";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userCount = Convert.ToInt32(reader["count"]);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            lblRoom.Text = userCount.ToString();

        }
        
        private void GetTodayAppointment()
        {
            int userCount = 0;

            try
            {
                connection.Open();

                // Get the current date
                DateTime today = DateTime.Today;


                // Create the SQL SELECT query
                string selectQuery = "SELECT COUNT(*) AS count " +
                                     "FROM appointment WHERE status='ACTIVE' AND  DATE(date) = @TodayDate";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@TodayDate", today);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userCount = Convert.ToInt32(reader["count"]);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            lblAppointmnet.Text = userCount.ToString();

        }

        private void GetActiveTheaterCount()
        {
            int userCount = 0;

            try
            {
                connection.Open();

                // Create the SQL SELECT query
                string selectQuery = "SELECT COUNT(*) AS count " +
                                     "FROM theater WHERE status='ACTIVE' AND type='THEATER'";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userCount = Convert.ToInt32(reader["count"]);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            lblTheater.Text = userCount.ToString();

        }

        private void lblDoctor_Click(object sender, EventArgs e)
        {

        }
    }
}
