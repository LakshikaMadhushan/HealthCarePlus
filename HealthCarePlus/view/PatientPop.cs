using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Patagames.Pdf.Net.Controls.WinForms;
using HealthCarePlus.service;
using HealthCarePlus.model;


namespace HealthCarePlus
{
    public partial class PatientPop : Form
    {
        string con;
        MySqlConnection connection;
        string patientId;
        string patientNames;
        PatientController patientController;
        public PatientPop()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
          
            patientController = new PatientController(connection);
           
        }

        public PatientPop(string id,string patientName)
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
           
            patientId=id;
            patientNames= patientName;
            txtPId.Text = patientId;
            txtPName.Text = patientName;
            patientController = new PatientController(connection);
            DataTable dataTable = patientController.LoadPatientReports(Convert.ToInt32(patientId));

            if (dataTable != null)
            {
                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;
            }
            
        }



        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSavePDF_Click(object sender, EventArgs e)
        {
            // Create an instance of the OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // only allow PDF files
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected PDF file path
                string selectedFilePath = openFileDialog.FileName;

                string destinationFolderPath = @"G:\Music"; 

                try
                {
                    // Get the file name from the selected file path
                    string fileName = Path.GetFileName(selectedFilePath);

                    // Combine the destination folder path with the file name
                    string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

                    // Copy the selected PDF file to the destination folder
                    File.Copy(selectedFilePath, destinationFilePath, true);

                    txtPath.Text = destinationFilePath;
                    txtFile.Text = fileName;

                    
                    pdfViewer1.LoadDocument(destinationFilePath);


                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPId.Text) || string.IsNullOrEmpty(txtPName.Text)
                 || string.IsNullOrEmpty(txtRemark.Text) 
                 || string.IsNullOrEmpty(txtPath.Text) || string.IsNullOrEmpty(txtDate.Text)
                 || string.IsNullOrEmpty(txtFile.Text))
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
          

            string fileName = txtFile.Text;
            byte[] fileData = File.ReadAllBytes(txtPath.Text);
            DateTime date = txtDate.Value;
            string patientName = txtPName.Text;
            string remark = txtRemark.Text;
            int patientId = int.Parse(txtPId.Text);
            string path = txtPath.Text;

            if (patientController.RegisterPatientReport(fileName, fileData, date, patientName, remark, patientId, path))
            {
                MessageBox.Show("Patient record inserted successfully.");
                clear();
            }
            else
            {
                MessageBox.Show("Insertion failed.");
            }
            DataTable dataTable = patientController.LoadPatientReports(Convert.ToInt32(patientId));
        }

        private void btnPSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM patient WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtPId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string name = reader["name"].ToString();

                        txtPName.Text = name;
               
                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Patient record not found.");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }

        

        private void btnSearch_Click(object sender, EventArgs e)
        {
         
            string reportId = txtId.Text;
            PatientReportData reportData = patientController.SearchPatientReport(reportId);

            if (reportData != null)
            {
                // Data found for the given ID
                txtDate.Text = reportData.Date;
                txtFile.Text = reportData.FileName;
                txtRemark.Text = reportData.Remark;
                txtPName.Text = reportData.PatientName;
                txtPath.Text = reportData.Path;
                txtPId.Text = reportData.PatientId;

                // Save the PDF data to a temporary file
                string tempPdfFilePath = Path.Combine(Path.GetTempPath(), reportData.FileName+".pdf");
                File.WriteAllBytes(tempPdfFilePath, reportData.PdfData);

                // Display the PDF in the PDF viewer control
                pdfViewer1.LoadDocument(tempPdfFilePath);
            }
            else
            {
                // No data found for the given ID
                MessageBox.Show("Patient details record not found.");
            }
        }
        private void clear()
        {
            txtDate.Text = "";
            txtFile.Text = "";
            txtRemark.Text = "";
            //txtPName.Text = "";
            txtPath.Text = "";
            //txtPId.Text = "";
            txtId.Text = "";
            pdfViewer1.Document = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            DataTable dataTable = patientController.LoadPatientReports(Convert.ToInt32(patientId));

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPId.Text) || string.IsNullOrEmpty(txtPName.Text)
                 || string.IsNullOrEmpty(txtRemark.Text) || string.IsNullOrEmpty(txtId.Text)
                 || string.IsNullOrEmpty(txtPath.Text) || string.IsNullOrEmpty(txtDate.Text)
                 || string.IsNullOrEmpty(txtFile.Text)) 
            {
                MessageBox.Show("Please Fill All Required Field.");
                return;
            }
           
            int reportId = int.Parse(txtId.Text);
            string fileName = txtFile.Text;
            byte[] fileData = File.ReadAllBytes(txtPath.Text);
            DateTime date = txtDate.Value;
            string patientName = txtPName.Text;
            string remark = txtRemark.Text;
            int patientId = int.Parse(txtPId.Text);
            string path = txtPath.Text;

            if (patientController.UpdateReport(reportId, fileName, fileData, date, patientName, remark, patientId, path))
            {
                MessageBox.Show("Report record updated successfully.");
                clear();
                DataTable dataTable = patientController.LoadPatientReports(Convert.ToInt32(patientId));
            }
            else
            {
                MessageBox.Show("Update failed. Report record with the specified ID was not found.");
            }
        }
    }
}
