using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem
{
    public partial class EmployeeListWindow : Window
    {
        // Example list of employees (replace with your actual data source)
        //private List<Employee> EmployeesList { get; set; }

        public EmployeeListWindow()
        {
            InitializeComponent();
            //InitializeEmployeesList(); // Load or initialize employee data
            //DataContext = this; // Set data context for binding
        }

        //private void InitializeEmployeesList()
        //{
        //    // Replace with actual logic to fetch employees from database or other source
        //    EmployeesList = new List<Employee>
        //    {
        //        new Employee { EmployeeId = 1, FullName = "John Doe", EGN = "1234567890", JobTitle = "Developer", Department = "IT", RemainingLeaveDays = 20 },
        //        new Employee { EmployeeId = 2, FullName = "Jane Smith", EGN = "0987654321", JobTitle = "Manager", Department = "HR", RemainingLeaveDays = 15 }
        //        // Add more employees as needed
        //    };
        //}

        private void lvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change if needed
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
            // Refresh employee list (e.g., reload data from database)
            //InitializeEmployeesList();
            lvEmployees.Items.Refresh(); // Refresh the ListView
        }
    }
}
