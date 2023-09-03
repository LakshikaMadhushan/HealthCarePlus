using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Patagames.Pdf.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace HealthCarePlus
{
    public partial class Bill : Form
    {
        string con;
        string ids;
        MySqlConnection connection;
        public Bill()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
        }
        public Bill(string id)
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            ids = id;
            ShowActiveBills();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ShowActiveBills()
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
                        dataGridView1.DataSource = billTable;

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

        private void Bill_Load(object sender, EventArgs e)
        {

        }

        private void GeneratePDF()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "PDF File|*.pdf";
                saveFileDialog1.Title = "Save PDF File";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string pdfFilePath = saveFileDialog1.FileName;

                    PdfWriter writer = new PdfWriter(pdfFilePath);
                    iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer);
                    Document document = new Document(pdf);
                    iText.Layout.Element.Table table = new iText.Layout.Element.Table(dataGridView1.Columns.Count);

                    // Add headers from the DataGridView to the PDF table
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(column.HeaderText)));
                    }

                    // Add rows and data from the DataGridView to the PDF table
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            table.AddCell(new Cell().Add(new Paragraph(cell.Value.ToString())));
                        }
                    }

                    document.Add(table);
                    document.Close();
                    MessageBox.Show("PDF saved successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            GenerateAndDownloadPDF();
        }
        private void GenerateAndDownloadPDF()
        {
            // Create a new PDF document
            using (Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Capture the contents of a control (e.g., a panel)
                panel1.DrawToBitmap(bitmap, new Rectangle(0, 0, panel1.Width, panel1.Height));

                // Create a SaveFileDialog to specify the PDF file path
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save PDF File";
                saveFileDialog.FileName = "output.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the captured content as a PDF file
                    using (bitmap)
                    using (var document = new System.Drawing.Printing.PrintDocument())
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

        private void txtTotal_Click(object sender, EventArgs e)
        {

        }
    }
}
