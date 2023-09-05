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
    internal class ReportController
    {
        private MySqlConnection connection;
        public ReportController(MySqlConnection connection) {
            this.connection = connection;
        }
        public DataTable SearchIncome(DateTime startDate, DateTime endDate)
        {
            try
            {
                connection.Open();

                // Construct the SQL query to retrieve active bills
                string selectActiveBillsQuery = "SELECT * FROM payment WHERE status = 'PAID' AND paymentDate BETWEEN @StartDate AND @EndDate";

                using (MySqlCommand command = new MySqlCommand(selectActiveBillsQuery, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable billTable = new DataTable();
                        adapter.Fill(billTable);

                        return billTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable SearchAllocations(DateTime startDate, DateTime endDate)
        {
            try
            {
                connection.Open();

                // Construct the SQL query to retrieve allocations
                string selectAllocationQuery = "SELECT * FROM resource WHERE buyingDate BETWEEN @StartDate AND @EndDate";

                using (MySqlCommand command = new MySqlCommand(selectAllocationQuery, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable allocationTable = new DataTable();
                        adapter.Fill(allocationTable);

                        return allocationTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
