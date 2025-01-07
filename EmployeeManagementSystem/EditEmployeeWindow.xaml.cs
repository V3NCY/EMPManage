using System;
using System.Data.Odbc;
using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class EditEmployeeWindow : Window
    {
        public LocalEmployee Employee { get; set; }

        public EditEmployeeWindow(LocalEmployee employee)
        {
            InitializeComponent();
            Employee = employee;

            // Populate the fields with employee data
            txtFullName.Text = Employee.FullName;
            txtJobTitle.Text = Employee.JobTitle;
            txtDepartment.Text = Employee.Department;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            // Save changes back to the employee object
            string oldFullName = Employee.FullName;
            string oldJobTitle = Employee.JobTitle;
            string oldDepartment = Employee.Department;

            Employee.FullName = txtFullName.Text;
            Employee.JobTitle = txtJobTitle.Text;
            Employee.Department = txtDepartment.Text;

            // Update the database
            UpdateEmployeeInDatabase(Employee, oldFullName, oldJobTitle, oldDepartment);

            // Close the dialog and indicate success
            DialogResult = true;
            Close();
        }

        private void UpdateEmployeeInDatabase(LocalEmployee employee, string oldFullName, string oldJobTitle, string oldDepartment)
        {
            string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";

            // Split FullName into FirstName and LastName
            string[] nameParts = employee.FullName.Split(' ');
            string firstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
            string lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();

                    // Log the old details
                    string logQuery = "INSERT INTO EmployeeChanges (EmployeeId, OldFullName, OldJobTitle, OldDepartment, ChangeDate) " +
                                      "VALUES (?, ?, ?, ?)";
                    using (OdbcCommand logCommand = new OdbcCommand(logQuery, connection))
                    {
                        logCommand.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                        logCommand.Parameters.AddWithValue("@OldFullName", oldFullName);
                        logCommand.Parameters.AddWithValue("@OldJobTitle", oldJobTitle);
                        logCommand.Parameters.AddWithValue("@OldDepartment", oldDepartment);
                        logCommand.Parameters.AddWithValue("@ChangeDate", DateTime.Now);
                        logCommand.ExecuteNonQuery();
                    }

                    // Update the employee record
                    string updateQuery = "UPDATE Employees SET FirstName = ?, LastName = ?, JobTitle = ?, Department = ?, WHERE EmployeeId = ?";
                    using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@FirstName", firstName);
                        updateCommand.Parameters.AddWithValue("@LastName", lastName);
                        updateCommand.Parameters.AddWithValue("@JobTitle", employee.JobTitle);
                        updateCommand.Parameters.AddWithValue("@Department", employee.Department);
                        updateCommand.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}");
            }
        }
    }
}
