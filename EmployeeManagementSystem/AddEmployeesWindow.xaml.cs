using System;
using System.Data.Odbc;
using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class AddEmployeesWindow : Window
    {
        public AddEmployeesWindow()
        {
            InitializeComponent();
        }

        // Test connection to the database
        private void OnTestConnectionButtonClick(object sender, RoutedEventArgs e)
        {
            string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";
            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Connection successful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Add employee to the database
        private void OnAddEmployeeButtonClick(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string egn = txtEGN.Text;
            string jobTitle = txtJobTitle.Text;
            string department = txtDepartment.Text;

            string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";
            string query = "INSERT INTO Employees (FirstName, LastName, EGN, JobTitle, Department) VALUES (?, ?, ?, ?, ?)";

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        // Adding parameters in order
                        command.Parameters.AddWithValue("FirstName", firstName);
                        command.Parameters.AddWithValue("LastName", lastName);
                        command.Parameters.AddWithValue("EGN", egn);
                        command.Parameters.AddWithValue("JobTitle", jobTitle);
                        command.Parameters.AddWithValue("Department", department);

                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) added successfully.");
                    }
                }

                // Close the AddEmployee window after success
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
