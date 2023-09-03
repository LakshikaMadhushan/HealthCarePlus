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


namespace HealthCarePlus
{
    public partial class PatientPop : Form
    {
        string con;
        MySqlConnection connection;
        string patientId;
        string patientNames;
        public PatientPop()
        {
            InitializeComponent();
            con = "datasource=localhost;port=3306;username=root;password='';database='mydatabases'";
            connection = new MySqlConnection(con);
            table_load();
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
            table_load();
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
            try
            {
                
                    connection.Open();

                    // Sample data to insert
                    string fileName = txtFile.Text;
                    byte[] fileData = File.ReadAllBytes(txtPath.Text); 
                    DateTime date = txtDate.Value;
                    string patientName = txtPName.Text;
                    string remark = txtRemark.Text;
                    string path = txtPath.Text;
                    int patientId = int.Parse(txtPId.Text);

                // SQL query to insert data into the "report" table
                string insertQuery = "INSERT INTO report (fileName, fileData, date, patientName, remark, patientId,path) " +
                                         "VALUES (@FileName, @FileData, @Date, @PatientName, @Remark, @PatientId, @Path)";

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FileName", fileName);
                        command.Parameters.AddWithValue("@FileData", fileData);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@PatientName", patientName);
                        command.Parameters.AddWithValue("@Remark", remark);
                        command.Parameters.AddWithValue("@PatientId", patientId);
                        command.Parameters.AddWithValue("@Path", path);

                    int rowsAffected=command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Patient record inserted successfully.");
                        clear();
                       
                    }
                    else
                    {
                        MessageBox.Show("Insertion failed.");
                    }
                }
                
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                table_load();
            }
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

        private void table_load()
        {
            try
            {
                    connection.Open();

                    // SQL query to select data from the "report" table
                    string selectQuery = "SELECT id ,fileName,date,patientName,remark,patientId,path FROM report WHERE patientId=@Id";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                    command.Parameters.AddWithValue("@Id", patientId);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                    connection.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string searchQuery = "SELECT * FROM report  WHERE id = @Id;";


                MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

                // Provide the ID you want to search for as a parameter
                cmd.Parameters.AddWithValue("@Id", txtId.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Data found for the given ID
                        string fileName = reader["fileName"].ToString();
                        string date = reader["date"].ToString();
                        string pName = reader["patientName"].ToString();
                        string pId = reader["patientId"].ToString();
                        string remark = reader["remark"].ToString();
                        string path = reader["path"].ToString();
                        byte[] pdfData = (byte[])reader["fileData"];

                        txtDate.Text = date;
                        txtFile.Text = fileName;   
                        txtRemark.Text = remark;
                        txtPName.Text = pName;
                        txtPath.Text = path;
                        txtPId.Text = pId;



                        // Save the PDF data to a temporary file
                        string tempPdfFilePath = Path.Combine(Path.GetTempPath(), "temp.pdf");
                        File.WriteAllBytes(tempPdfFilePath, pdfData);

                        // Display the PDF in the PDF viewer control
                        pdfViewer1.LoadDocument(tempPdfFilePath);




                    }
                    else
                    {
                        // No data found for the given ID
                        MessageBox.Show("Patient details record not found.");
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
            table_load();
           
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
            try
            {
                     connection.Open();

                    // Sample data to update
                    string fileName = txtFile.Text;
                    byte[] fileData = File.ReadAllBytes(txtPath.Text);
                    DateTime date = txtDate.Value;
                    string patientName = txtPName.Text;
                    string remark = txtRemark.Text;
                    int patientId = int.Parse(txtPId.Text);
                    string path = txtPath.Text;

                // SQL query to update data in the "report" table by ID
                string updateQuery = "UPDATE report " +
                                         "SET fileName = @FileName, fileData = @FileData, date = @Date, " +
                                         "patientName = @PatientName, remark = @Remark, patientId = @PatientId, path = @Path " +
                                         "WHERE id = @ReportId";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FileName", fileName);
                        command.Parameters.AddWithValue("@FileData", fileData);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@PatientName", patientName);
                        command.Parameters.AddWithValue("@Remark", remark);
                        command.Parameters.AddWithValue("@PatientId", patientId);
                        command.Parameters.AddWithValue("@Path", path);
                       
                        command.Parameters.AddWithValue("@ReportId",txtId.Text );

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Report record updated successfully.");
                            clear();
                            table_load();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Report record with the specified ID was not found.");
                        }
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
