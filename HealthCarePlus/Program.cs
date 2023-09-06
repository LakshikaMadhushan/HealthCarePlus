
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HealthCarePlus
{
    internal static class Program
    {
         /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string con;
            MySqlConnection connection;


            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            //con = "Server=localhost;Database=mydatabase;Uid=root;Pwd='';";
            connection = new MySqlConnection(con);
            try
            {
                // Open the connection
                connection.Open();

            // SQL command to create a database
            string createDatabaseQuery = "CREATE DATABASE IF NOT EXISTS mydatabases";
            MySqlCommand createDatabaseCmd = new MySqlCommand(createDatabaseQuery, connection);
            createDatabaseCmd.ExecuteNonQuery();

                //SQL command to create tables, if needed
                string createTableQuery = "CREATE TABLE IF NOT EXISTS user (\r\n    id INT PRIMARY KEY AUTO_INCREMENT,\r\n    name VARCHAR(255) NOT NULL,\r\n    address VARCHAR(255),\r\n    qualification VARCHAR(255),\r\n    contact VARCHAR(15),\r\n    email VARCHAR(255) UNIQUE NOT NULL,\r\n    password VARCHAR(255) NOT NULL,\r\n    role VARCHAR(255) NOT NULL,\r\n  status VARCHAR(255) NOT NULL,\r\n  nic VARCHAR(255) NOT NULL\r\n);";
                MySqlCommand createTableCmd = new MySqlCommand(createTableQuery, connection);
                createTableCmd.ExecuteNonQuery();

                string patientTbl = "CREATE TABLE IF NOT EXISTS patient (\r\n    id INT PRIMARY KEY AUTO_INCREMENT,\r\n    name VARCHAR(255) NOT NULL,\r\n    email VARCHAR(255) UNIQUE NOT NULL,\r\n     dateOfBirth DATE,\r\n    address VARCHAR(255),\r\n    gender VARCHAR(10),\r\n    nic VARCHAR(20),\r\n    contactNo VARCHAR(15)\r\n);";
                MySqlCommand createpatientTbl = new MySqlCommand(patientTbl, connection);
                createpatientTbl.ExecuteNonQuery();

                string schedukeTbl = "CREATE TABLE IF NOT EXISTS schedule (\r\n    id INT AUTO_INCREMENT PRIMARY KEY,\r\n    userId INT,\r\n    startTime TIME,\r\n    endTime TIME,\r\n    status VARCHAR(255),\r\n  userName VARCHAR(255),\r\n    date DATE,\r\n    price DECIMAL(10, 2),\r\n    maxPatient INT,\r\n  countPatient INT,\r\n    FOREIGN KEY (userId) REFERENCES user(id)\r\n );";
                MySqlCommand createScheduletTbl = new MySqlCommand(schedukeTbl, connection);
                createScheduletTbl.ExecuteNonQuery();

                string appointmentTbl = "CREATE TABLE IF NOT EXISTS appointment (\r\n    id INT AUTO_INCREMENT PRIMARY KEY,\r\n    scheduleId INT,\r\n    userId INT,\r\n   patientId INT,\r\n    price DECIMAL(10, 2),\r\n  patientName VARCHAR(255),\r\n    doctorName VARCHAR(255),\r\n    date DATE,\r\n    status VARCHAR(255),\r\n    FOREIGN KEY (scheduleId) REFERENCES schedule(id),\r\n  FOREIGN KEY (patientId) REFERENCES user(id),\r\n    FOREIGN KEY (userId) REFERENCES user(id)\r\n);";
                MySqlCommand createAppointmenttTbl = new MySqlCommand(appointmentTbl, connection);
                createAppointmenttTbl.ExecuteNonQuery();

                string resourceTbl = "CREATE TABLE IF NOT EXISTS resource (\r\n    id INT AUTO_INCREMENT PRIMARY KEY,\r\n    name VARCHAR(255) NOT NULL,\r\n    type VARCHAR(255),\r\n    buyingDate DATE,\r\n    price DECIMAL(10, 2),\r\n    status VARCHAR(255) NOT NULL,\r\n    remark TEXT,\r\n    repairedDate DATE\r\n);";
                MySqlCommand createResourcepointmenttTbl = new MySqlCommand(resourceTbl, connection);
                createResourcepointmenttTbl.ExecuteNonQuery();

                string theaterTbl = "CREATE TABLE IF NOT EXISTS theater (\r\n    id INT AUTO_INCREMENT PRIMARY KEY,\r\n    name VARCHAR(255) NOT NULL,\r\n    price DECIMAL(10, 2) NOT NULL,\r\n    maxPatient INT NOT NULL,\r\n    Specification TEXT,\r\n    status VARCHAR(255) NOT NULL,\r\n    type VARCHAR(255) NOT NULL\r\n);";
                MySqlCommand createTheaterTbl = new MySqlCommand(theaterTbl, connection);
                createTheaterTbl.ExecuteNonQuery();

                string medicineTbl = "CREATE TABLE IF NOT EXISTS medicine (\r\n    id INT AUTO_INCREMENT PRIMARY KEY,\r\n    name VARCHAR(255) NOT NULL\r\n);";
                MySqlCommand createMedicineTbl = new MySqlCommand(medicineTbl, connection);
                createMedicineTbl.ExecuteNonQuery();

                string medicationTbl = "CREATE TABLE IF NOT EXISTS medication (\r\n    id INT AUTO_INCREMENT PRIMARY KEY,\r\n    date DATE NOT NULL,\r\n    patientId INT NOT NULL,\r\n    noOfDays INT NOT NULL,\r\n    dose VARCHAR(50) NOT NULL,\r\n    medicineId INT NOT NULL,\r\n    patientName VARCHAR(255) NOT NULL,\r\n    medicineName VARCHAR(255) NOT NULL,\r\n    FOREIGN KEY (patientId) REFERENCES patient(id),\r\n    FOREIGN KEY (medicineId) REFERENCES medicine(id)\r\n);";
                MySqlCommand createMedicationTbl = new MySqlCommand(medicationTbl, connection);
                createMedicationTbl.ExecuteNonQuery();

                string reportTbl = "CREATE TABLE IF NOT EXISTS report (\r\n    id INT AUTO_INCREMENT PRIMARY KEY,\r\n    fileName VARCHAR(255) NOT NULL,\r\n    fileData LONGBLOB NOT NULL,\r\n    date DATE,\r\n    patientName VARCHAR(255),\r\n    path VARCHAR(255),\r\n    remark VARCHAR(255),\r\n    patientId INT,\r\n    FOREIGN KEY (patientId) REFERENCES patient(id)\r\n);";
                MySqlCommand createReportTbl = new MySqlCommand(reportTbl, connection);
                createReportTbl.ExecuteNonQuery();
                 
                string theaterDetailTbl = "CREATE TABLE IF NOT EXISTS theaterDetail (\r\n    id INT AUTO_INCREMENT PRIMARY KEY,\r\n    date DATE,\r\n    patientName VARCHAR(255) NOT NULL,\r\n    price DECIMAL(10, 2),\r\n    status VARCHAR(255),\r\n    type VARCHAR(255),\r\n    theaterId INT,\r\n    patientId INT,\r\n    FOREIGN KEY (theaterId) REFERENCES theater(id),\r\n    FOREIGN KEY (patientId) REFERENCES patient(id)\r\n);";
                MySqlCommand createTheaterDetailsTbl = new MySqlCommand(theaterDetailTbl, connection);
                createTheaterDetailsTbl.ExecuteNonQuery();

                string paymentTbl = "CREATE TABLE IF NOT EXISTS Payment (\r\n    id INT AUTO_INCREMENT PRIMARY KEY,\r\n    patientId INT,\r\n    appointmentId INT,\r\n    theaterDetailsId INT,\r\n    medicationId INT,\r\n    typeId INT,\r\n    paymentDate DATE,\r\n    price DECIMAL(10, 2),\r\n    type VARCHAR(255),\r\n    status VARCHAR(255),\r\n    patientName VARCHAR(255),\r\n    FOREIGN KEY (patientId) REFERENCES patient(id),\r\n    FOREIGN KEY (appointmentId) REFERENCES appointment(id),\r\n    FOREIGN KEY (theaterDetailsId) REFERENCES theaterDetail(id),\r\n    FOREIGN KEY (medicationId) REFERENCES medication(id)\r\n);";
                MySqlCommand createPaymentTbl = new MySqlCommand(paymentTbl, connection);
                createPaymentTbl.ExecuteNonQuery();

                //string staffTbl = "CREATE TABLE IF NOT EXISTS staff (\r\n    id INT PRIMARY KEY AUTO_INCREMENT,\r\n    name VARCHAR(255) NOT NULL,\r\n    address VARCHAR(255),\r\n    contact VARCHAR(15),\r\n    status VARCHAR(255),\r\n    nic VARCHAR(20),\r\n    email VARCHAR(255) UNIQUE NOT NULL,\r\n    password VARCHAR(255) NOT NULL,\r\n    userRole VARCHAR(255) NOT NULL\r\n);";
                //MySqlCommand createStaffTbl = new MySqlCommand(staffTbl, connection);
                //createStaffTbl.ExecuteNonQuery();

                // Close the connection
                connection.Close();
            } 
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new login());
            //Application.Run(new Registration());
            //Application.Run(new Patient());
            //Application.Run(new PatientPop());
            //Application.Run(new Appointment());
            //Application.Run(new Staff());
            //Application.Run(new Theater());
            //Application.Run(new TheaterPop());
            //Application.Run(new Bill());
            //Application.Run(new Medication());
            Application.Run(new Schedule());
            //Application.Run(new Report());
            //Application.Run(new DashBoard());
            //Application.Run(new PatientPop());
            //Application.Run(new Resource());
            //Application.Run(new Payment());
        }
    }
}
