using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace EmployeeManagementSystem
{
    public partial class PersonalFilesWindow : Window
    {
        public PersonalFilesWindow()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            
        }

        private void OnEmployeeDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (employeeListView.SelectedItem is Employee selectedEmployee)
            {
                MessageBox.Show($"Employee ID: {selectedEmployee.EmployeeId}, Name: {selectedEmployee.FullName}");
            }
        }
    }

    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; internal set; }
    }
}
