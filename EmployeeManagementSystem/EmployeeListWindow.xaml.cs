using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem
{
    public partial class EmployeeListWindow : Window
    {
        
        //private List<Employee> EmployeesList { get; set; }

        public EmployeeListWindow()
        {
            InitializeComponent();
            //InitializeEmployeesList(); 
            //DataContext = this; 
            LoadEmployees();
        }
        // Method to load employees from the database
        private void LoadEmployees()
        {
            string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";
            string query = "SELECT EmployeeId, FirstName, LastName, EGN, JobTitle, Department FROM Employees";
            List<Employee> employeesList = new List<Employee>();

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
                                employeesList.Add(new Employee
                                {
                                    EmployeeId = reader.GetInt32(0),
                                    FullName = $"{reader.GetString(1)} {reader.GetString(2)}", // Concatenate FirstName and LastName
                                    EGN = reader.GetString(3),
                                    JobTitle = reader.GetString(4),
                                    Department = reader.GetString(5)
                                });
                            }
                        }
                    }
                }

                // Bind the employees list to the ListView
                lvEmployees.ItemsSource = employeesList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
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
            
        }

        //private void OnArchiveEmployeeClick(object sender, RoutedEventArgs e)
        //{
        //    // Example: Archive selected employee
        //    if (lvEmployees.SelectedItem is Employee selectedEmployee)
        //    {
        //        // Implement archive logic here (e.g., update database or mark as archived)
        //        MessageBox.Show($"Archived employee {selectedEmployee.FullName}");
        //    }
        //}

        //private void OnEditButtonClick(object sender, RoutedEventArgs e)
        //{
        //    // Example: Edit selected employee
        //    if (lvEmployees.SelectedItem is Employee selectedEmployee)
        //    {
        //        // Implement edit logic here (e.g., open edit window)
        //        MessageBox.Show($"Editing employee {selectedEmployee.FullName}");
        //    }
        //}

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            LoadEmployees(); // Refresh the employee list
        }
        public class Employee
        {
            public int EmployeeId { get; set; }
            public string FullName { get; set; }
            public string EGN { get; set; }  
            public string JobTitle { get; set; }  
            public string Department { get; set; }  
        }

    }
}
