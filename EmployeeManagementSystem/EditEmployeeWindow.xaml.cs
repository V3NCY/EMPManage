using System;
using System.Data.Odbc;
using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class EditEmployeeWindow : Window
    {
        public LocalEmployee Employee { get; set; }
        private bool isChanged = false;

        public EditEmployeeWindow(LocalEmployee employee)
        {
            InitializeComponent();
            Employee = employee;

            // Populate the fields with employee data
            txtFullName.Text = Employee.FullName;
            txtJobTitle.Text = Employee.JobTitle;
            txtDepartment.Text = Employee.Department;
            txtEGN.Text = Employee.EGN;

            // Disable EGN editing by default
            txtEGN.IsEnabled = false;
        }

        // Special EGN Permission Button
        private void OnRequestEgnEditClick(object sender, RoutedEventArgs e)
        {
            var inputDialog = new PermissionDialog();
            if (inputDialog.ShowDialog() == true && inputDialog.HasPermission)
            {
                txtEGN.IsEnabled = true; // Enable EGN editing if permission is granted
                MessageBox.Show("EGN editing enabled.");
            }
            else
            {
                MessageBox.Show("Permission denied. You cannot edit the EGN.");
            }
        }

        // Cancel button handler
        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            // Ask for confirmation only if changes were made
            if (isChanged)
            {
                var result = MessageBox.Show("Сигурни ли сте, че искате да затворите прозореца без да запазите промените?",
                                             "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                Close(); // No changes, just close the window
            }
        }

        // Save button handler
        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            // Check if any changes were made
            bool hasChanges = txtFullName.Text != Employee.FullName ||
                              txtJobTitle.Text != Employee.JobTitle ||
                              txtDepartment.Text != Employee.Department ||
                              (txtEGN.IsEnabled && txtEGN.Text != Employee.EGN); // Compare EGN only if enabled

            if (!hasChanges)
            {
                // No changes, show an error message
                MessageBox.Show("Няма промени за запазване.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return; // Exit without saving
            }

            // Save old values before updating the employee object
            string oldFullName = Employee.FullName;
            string oldJobTitle = Employee.JobTitle;
            string oldDepartment = Employee.Department;
            string oldEGN = Employee.EGN;

            // Update the employee object with new values
            Employee.FullName = txtFullName.Text;
            Employee.JobTitle = txtJobTitle.Text;
            Employee.Department = txtDepartment.Text;

            if (txtEGN.IsEnabled)
            {
                Employee.EGN = txtEGN.Text; // Save EGN only if editing was enabled
            }

            // Call the method with the required arguments
            UpdateEmployeeInDatabase(Employee, oldFullName, oldJobTitle, oldEGN, oldDepartment);

            // Close the dialog and indicate success
            DialogResult = true;
            Close();
        }

        // Update employee in the database and log changes
        private void UpdateEmployeeInDatabase(LocalEmployee employee, string oldFullName, string oldJobTitle, string oldEGN, string oldDepartment)
        {
            string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();

                    // Log the old details
                    string logQuery = "INSERT INTO EmployeeChanges (EmployeeId, OldFullName, OldJobTitle, OldEGN, OldDepartment, ChangeDate) " +
                                      "VALUES (?, ?, ?, ?, ?, ?)";
                    using (OdbcCommand logCommand = new OdbcCommand(logQuery, connection))
                    {
                        logCommand.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                        logCommand.Parameters.AddWithValue("@OldFullName", oldFullName);
                        logCommand.Parameters.AddWithValue("@OldJobTitle", oldJobTitle);
                        logCommand.Parameters.AddWithValue("@OldEGN", oldEGN);
                        logCommand.Parameters.AddWithValue("@OldDepartment", oldDepartment);
                        logCommand.Parameters.AddWithValue("@ChangeDate", DateTime.Now);

                        logCommand.ExecuteNonQuery();
                    }

                    // Update employee details in the database
                    string updateQuery = "UPDATE Employees SET FirstName = ?, LastName = ?, JobTitle = ?, Department = ?, EGN = ? WHERE EmployeeId = ?";
                    using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, connection))
                    {
                        string[] nameParts = employee.FullName.Split(' ');
                        string firstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
                        string lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

                        updateCommand.Parameters.AddWithValue("@FirstName", firstName);
                        updateCommand.Parameters.AddWithValue("@LastName", lastName);
                        updateCommand.Parameters.AddWithValue("@JobTitle", employee.JobTitle);
                        updateCommand.Parameters.AddWithValue("@Department", employee.Department);
                        updateCommand.Parameters.AddWithValue("@EGN", employee.EGN);
                        updateCommand.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);

                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes to the database: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
