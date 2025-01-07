using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EmployeeManagementSystem;


namespace EmployeeManagementSystem
{
    public partial class EmployeeListWindow : Window
    {

        //private List<Employee> EmployeesList { get; set; }
        private List<LocalEmployee> employeesList;
        public EmployeeListWindow()
        {
            InitializeComponent();
            //InitializeEmployeesList(); 
            //DataContext = this; 
            LoadEmployees();
        }
        private void LoadEmployees()
        {
            string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";
            string query = "SELECT EmployeeId, FirstName, LastName, EGN, JobTitle, Department FROM Employees WHERE IsArchived = 0"; // Only load active employees

            // Clear the existing list
            employeesList = new List<LocalEmployee>();

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employeesList.Add(new LocalEmployee
                                {
                                    EmployeeId = reader.GetInt32(0),
                                    FullName = $"{reader.GetString(1)} {reader.GetString(2)}",
                                    EGN = reader.GetString(3),
                                    JobTitle = reader.GetString(4),
                                    Department = reader.GetString(5)
                                });
                            }
                        }
                    }
                }

                // Update the ListView
                lvEmployees.ItemsSource = null; // Clear the ListView
                lvEmployees.ItemsSource = employeesList; // Rebind the updated list
                lvEmployees.Items.Refresh(); // Force UI refresh
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task LoadEmployeesAsync()
        {
            string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";
            string query = "SELECT EmployeeId, FirstName, LastName, EGN, JobTitle, Department FROM Employees WHERE IsArchived = 0"; // Only load active employees

            employeesList = await Task.Run(() =>
            {
                var tempList = new List<LocalEmployee>();

                try
                {
                    using (OdbcConnection connection = new OdbcConnection(connectionString))
                    {
                        connection.Open();
                        using (OdbcCommand command = new OdbcCommand(query, connection))
                        {
                            using (OdbcDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    tempList.Add(new LocalEmployee
                                    {
                                        EmployeeId = reader.GetInt32(0),
                                        FullName = $"{reader.GetString(1)} {reader.GetString(2)}",
                                        EGN = reader.GetString(3),
                                        JobTitle = reader.GetString(4),
                                        Department = reader.GetString(5)
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading employees: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return tempList;
            });

            // Update the ListView
            lvEmployees.ItemsSource = null; // Clear the ListView
            lvEmployees.ItemsSource = employeesList; // Rebind the updated list
            lvEmployees.Items.Refresh(); // Force UI refresh
        }

        // Method to fetch employees from the database
        private List<LocalEmployee> FetchEmployeesFromDatabase(string connectionString, string query)
        {
            var employees = new List<LocalEmployee>();
            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var employee = new LocalEmployee
                                {
                                    EmployeeId = reader.GetInt32(0),
                                    FullName = $"{reader["FirstName"]} {reader["LastName"]}",
                                    EGN = reader.GetString(3),
                                    JobTitle = reader.GetString(4),
                                    Department = reader.GetString(5),
                                    RemainingLeaveDays = 30 // Example value
                                };
                                employees.Add(employee);
                                Console.WriteLine($"Fetched Employee: {employee.FullName}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}");
            }
            return employees;
        }

        //private void InitializeEmployeesList()
        //{
        //  
        //    EmployeesList = new List<Employee>
        //    {
        //        new Employee { EmployeeId = 1, FullName = "John Doe", EGN = "1234567890", JobTitle = "Developer", Department = "IT", RemainingLeaveDays = 20 },
        //        new Employee { EmployeeId = 2, FullName = "Jane Smith", EGN = "0987654321", JobTitle = "Manager", Department = "HR", RemainingLeaveDays = 15 }
        //      
        //    };
        //}

        private void lvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvEmployees.SelectedItem is LocalEmployee selectedEmployee)
            {
            }
        }


        private void OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItem is LocalEmployee selectedEmployee)
            {
                var editWindow = new EditEmployeeWindow(selectedEmployee);
                if (editWindow.ShowDialog() == true)
                {
                    lvEmployees.Items.Refresh(); // Refresh the ListView
                    MessageBox.Show($"Employee {selectedEmployee.FullName} updated successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to edit.");
            }
        }

        private void OnArchiveEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (lvEmployees.SelectedItem is LocalEmployee selectedEmployee)
            {
                var result = MessageBox.Show($"Are you sure you want to archive employee: {selectedEmployee.FullName}?",
                                             "Confirm Archive", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes)
                {
                    return; // Exit if the user cancels
                }

                string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";

                try
                {
                    using (OdbcConnection connection = new OdbcConnection(connectionString))
                    {
                        connection.Open();

                        // Start a transaction to ensure both updates succeed
                        using (OdbcTransaction transaction = connection.BeginTransaction())
                        {
                            // Step 1: Mark the employee as archived in the Employees table
                            string updateQuery = "UPDATE Employees SET IsArchived = 1 WHERE EmployeeId = ?";
                            using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@EmployeeId", selectedEmployee.EmployeeId);
                                updateCommand.ExecuteNonQuery();
                            }

                            // Step 2: Insert the employee details into ArchivedEmployees
                            string insertQuery = "INSERT INTO ArchivedEmployees (EmployeeId, FullName, EGN, JobTitle, Department) " +
                                                 "VALUES (?, ?, ?, ?, ?)";
                            using (OdbcCommand insertCommand = new OdbcCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@EmployeeId", selectedEmployee.EmployeeId);
                                insertCommand.Parameters.AddWithValue("@FullName", selectedEmployee.FullName);
                                insertCommand.Parameters.AddWithValue("@EGN", selectedEmployee.EGN);
                                insertCommand.Parameters.AddWithValue("@JobTitle", selectedEmployee.JobTitle);
                                insertCommand.Parameters.AddWithValue("@Department", selectedEmployee.Department);
                                insertCommand.ExecuteNonQuery();
                            }

                            // Commit the transaction
                            transaction.Commit();
                        }
                    }

                    // Remove the employee from the active list
                    employeesList.Remove(selectedEmployee);
                    lvEmployees.ItemsSource = null;
                    lvEmployees.ItemsSource = employeesList;
                    lvEmployees.Items.Refresh();

                    MessageBox.Show($"Employee {selectedEmployee.FullName} has been archived.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error archiving employee: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to archive.");
            }
        }

        private void OnViewArchivedEmployeesClick(object sender, RoutedEventArgs e)
        {
            var archiveWindow = new ArchiveListWindow();
            archiveWindow.ShowDialog(); // Open the archive list window
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            LoadEmployees(); // Re-fetch data from the database
            lvEmployees.Items.Refresh(); // Refresh the ListView
        }




    }
}
