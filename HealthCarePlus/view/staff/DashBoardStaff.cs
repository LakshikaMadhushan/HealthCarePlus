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
using static HealthCarePlus.service.Dashboard;

namespace HealthCarePlus.view
{
    public partial class DashBoardStaff : Form
    {
        string con;
        MySqlConnection connection;
        DashBoardCountService dashboardCountService;
        public DashBoardStaff()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            dashboardCountService = new DashBoardCountService(connection);
            GetActiveDoctorCount();
            GetPatientCount();
            GetStaffCount();
            GetActiveRoomCount();
            GetTodayAppointment();
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



            int doctorCount = dashboardCountService.GetActiveDoctorCount();
            lblDoctor.Text = doctorCount.ToString();

        }
        private void GetPatientCount()
        {
            //int userCount = 0;

            //try
            //{
            //    connection.Open();

            //    // Create the SQL SELECT query
            //    string selectQuery = "SELECT COUNT(*) AS userCount " +
            //                         "FROM patient";

            //    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
            //    {
            //        using (MySqlDataReader reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                userCount = Convert.ToInt32(reader["userCount"]);
            //            }
            //        }
            //    }
            //    connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //}

            //lblPatient.Text = userCount.ToString();

            int patientCount = dashboardCountService.GetPatientCount();
            lblPatient.Text = patientCount.ToString();

        }

        private void GetStaffCount()
        {
            //int userCount = 0;

            //try
            //{
            //    connection.Open();

            //    // Create the SQL SELECT query
            //    string selectQuery = "SELECT COUNT(*) AS userCount " +
            //                             "FROM user " +
            //                             "WHERE status = 'ACTIVE' AND role NOT IN ('DOCTOR')";

            //    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
            //    {
            //        using (MySqlDataReader reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                userCount = Convert.ToInt32(reader["userCount"]);
            //            }
            //        }
            //    }
            //    connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //}

            //lblStaff.Text = userCount.ToString();
            int staffCount = dashboardCountService.GetStaffCount();
            lblStaff.Text = staffCount.ToString();

        }

        private void GetActiveRoomCount()
        {
            //int userCount = 0;

            //try
            //{
            //    connection.Open();

            //    // Create the SQL SELECT query
            //    string selectQuery = "SELECT COUNT(*) AS count " +
            //                         "FROM theater WHERE status='ACTIVE' AND type='ROOM'";

            //    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
            //    {
            //        using (MySqlDataReader reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                userCount = Convert.ToInt32(reader["count"]);
            //            }
            //        }
            //    }
            //    connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //}

            //lblRoom.Text = userCount.ToString();
            int roomCount = dashboardCountService.GetActiveRoomCount();
            lblRoom.Text = roomCount.ToString();

        }

        private void GetTodayAppointment()
        {
            //int userCount = 0;

            //try
            //{
            //    connection.Open();

            //    // Get the current date
            //    DateTime today = DateTime.Today;


            //    // Create the SQL SELECT query
            //    string selectQuery = "SELECT COUNT(*) AS count " +
            //                         "FROM appointment WHERE status='ACTIVE' AND  DATE(date) = @TodayDate";

            //    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
            //    {
            //        command.Parameters.AddWithValue("@TodayDate", today);
            //        using (MySqlDataReader reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                userCount = Convert.ToInt32(reader["count"]);
            //            }
            //        }
            //    }
            //    connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //}

            //lblAppointmnet.Text = userCount.ToString();
            int appointmentCount = dashboardCountService.GetTodayAppointmentCount();
            lblAppointmnet.Text = appointmentCount.ToString();

        }

        private void GetActiveTheaterCount()
        {
            //int userCount = 0;

            //try
            //{
            //    connection.Open();

            //    // Create the SQL SELECT query
            //    string selectQuery = "SELECT COUNT(*) AS count " +
            //                         "FROM theater WHERE status='ACTIVE' AND type='THEATER'";

            //    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
            //    {
            //        using (MySqlDataReader reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                userCount = Convert.ToInt32(reader["count"]);
            //            }
            //        }
            //    }
            //    connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //}

            //lblTheater.Text = userCount.ToString();
            int theaterCount = dashboardCountService.GetActiveTheaterCount();
            lblAppointmnet.Text = theaterCount.ToString();

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            PatientStaff patient = new PatientStaff();
            if (patient == null)
            {
                patient.Parent = this;
            }
            patient.Show();
            this.Hide();
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            AppointmentStaff appointment = new AppointmentStaff();
            if (appointment == null)
            {
                appointment.Parent = this;
            }
            appointment.Show();
            this.Hide();
        }

        private void btnTheaters_Click(object sender, EventArgs e)
        {
            TheaterStaff theater = new TheaterStaff();
            if (theater == null)
            {
                theater.Parent = this;
            }
            theater.Show();
            this.Hide();
        }

        private void BtnMedication_Click(object sender, EventArgs e)
        {
            MedicationStaff medication = new MedicationStaff();
            if (medication == null)
            {
                medication.Parent = this;
            }
            medication.Show();
            this.Hide();
        }
    }
}
