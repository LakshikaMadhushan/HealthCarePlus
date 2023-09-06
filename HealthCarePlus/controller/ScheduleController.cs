using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCarePlus.controller
{
    public class ScheduleController
    {
        private MySqlConnection connection;


        public ScheduleController(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void LoadData(DataGridView dataGridView)
        {
            try
            {
                connection.Open();

                // Define the SQL query to select data
                string selectQuery = "SELECT id AS Id, userId AS DoctorId, userName AS DoctorName, " +
                                     "startTime AS StartTime, endTime AS EndTime, status AS Status, " +
                                     "date AS Date, price AS Price, maxPatient AS MaxPatient FROM schedule";

                // Create a data adapter to execute the query and fill a DataSet
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "schedule");

                    // Bind the DataGridView to a specific DataTable within the DataSet
                    dataGridView.DataSource = dataSet.Tables["schedule"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
