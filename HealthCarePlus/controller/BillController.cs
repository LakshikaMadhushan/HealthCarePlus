using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCarePlus.controller
{
    public class BillController
    {
        private MySqlConnection connection;
        public BillController(MySqlConnection connection)
        {
            this.connection = connection;
        }
        public  void GenerateAndDownloadPDF(Panel panel)
        {
            using (Bitmap bitmap = new Bitmap(panel.Width, panel.Height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Capture the contents of the panel
                panel.DrawToBitmap(bitmap, new Rectangle(0, 0, panel.Width, panel.Height));

                // Create a SaveFileDialog to specify the PDF file path
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save PDF File";
                saveFileDialog.FileName = "output.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the captured content as a PDF file
                    using (bitmap)
                    using (var document = new PrintDocument())
                    {
                        document.PrintPage += (sender, e) =>
                        {
                            e.Graphics.DrawImage(bitmap, 0, 0);
                        };

                        document.PrinterSettings.PrintToFile = true;
                        document.PrinterSettings.PrintFileName = saveFileDialog.FileName;
                        document.Print();
                    }

                    MessageBox.Show("PDF file saved successfully!");
                }
            }
        }


        public  void ShowActiveBills(DataGridView dataGridView, Label txtTotal)
        {
            try
            {
                connection.Open();

                // Construct the SQL query to retrieve active bills
                string selectActiveBillsQuery = "SELECT id,patientName,paymentDate, price,type, status  FROM payment WHERE status = 'PENDING'";

                using (MySqlCommand command = new MySqlCommand(selectActiveBillsQuery, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable billTable = new DataTable();
                        adapter.Fill(billTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView.DataSource = billTable;

                        // Calculate the total price
                        decimal totalPrice = 0;
                        foreach (DataRow row in billTable.Rows)
                        {
                            totalPrice += Convert.ToDecimal(row["price"]);
                        }

                        txtTotal.Text = "Total Price: $" + totalPrice.ToString("0.00");
                    }
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
