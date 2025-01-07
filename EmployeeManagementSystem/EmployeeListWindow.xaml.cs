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
        private async void LoadEmployees()
        {
            string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";
            string query = "SELECT EmployeeId, FirstName, LastName, EGN, JobTitle, Department FROM Employees";

            // Call the asynchronous method to load employees
            employeesList = await Task.Run(() => FetchEmployeesFromDatabase(connectionString, query));

            // Update the UI after fetching data
            lvEmployees.ItemsSource = employeesList;
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
                MessageBox.Show($"Archived employee: {selectedEmployee.FullName}");
                employeesList.Remove(selectedEmployee); // Remove from the list
                lvEmployees.Items.Refresh(); // Refresh the ListView
            }
            else
            {
                MessageBox.Show("Please select an employee to archive.");
            }
        }


        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            LoadEmployees(); 
        }

        
    }
}
