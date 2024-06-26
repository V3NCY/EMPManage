using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Document Request Buttons
        private void OnSickLeaveRequestsButtonClick(object sender, RoutedEventArgs e)
        {
            SickLeaveWindow sickLeaveWindow = new SickLeaveWindow();
            sickLeaveWindow.ShowDialog();
            DocumentsToggleButton.IsChecked = false;
        }

        private void OnOtherLeaveRequestsButtonClick(object sender, RoutedEventArgs e)
        {
            OtherLeaveWindow specialRequestsWindow = new OtherLeaveWindow();
            specialRequestsWindow.ShowDialog();
            DocumentsToggleButton.IsChecked = false;
        }

        private void OnPaidLeaveRequestsButtonClick(object sender, RoutedEventArgs e)
        {
            PaidLeaveWindow paidLeaveWindow = new PaidLeaveWindow();
            paidLeaveWindow.ShowDialog();
            DocumentsToggleButton.IsChecked = false;
        }

        // Employee Management Buttons
        private void OnAddEmployeeButtonClick(object sender, RoutedEventArgs e)
        {
            AddEmployeesWindow addEmployeesWindow = new AddEmployeesWindow();
            addEmployeesWindow.ShowDialog();
            EmployeesToggleButton.IsChecked = false;
        }

        private void OnEmployeeListButtonClick(object sender, RoutedEventArgs e)
        {
            EmployeeListWindow employeeListWindow = new EmployeeListWindow();
            employeeListWindow.ShowDialog();
            EmployeesToggleButton.IsChecked = false;
        }

        private void OnArchiveListButtonClick(object sender, RoutedEventArgs e)
        {
            ArchiveListWindow archiveListWindow = new ArchiveListWindow();
            archiveListWindow.ShowDialog();
            EmployeesToggleButton.IsChecked = false;
        }

        private void OnPersonalFilesButtonClick(object sender, RoutedEventArgs e)
        {
            PersonalFilesWindow personalFilesWindow = new PersonalFilesWindow();
            personalFilesWindow.ShowDialog();
            EmployeesToggleButton.IsChecked = false;
        }

        // ToggleButton Checked and Unchecked Handlers
        private void DocumentsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            CloseEmployeesPopup();
            DocumentsPopup.IsOpen = true;
        }

        private void DocumentsToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            DocumentsPopup.IsOpen = false;
        }

        private void EmployeesToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            CloseDocumentsPopup();
            EmployeesPopup.IsOpen = true;
        }

        private void EmployeesToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            EmployeesPopup.IsOpen = false;
        }
        private void CarsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            CarsPopup.IsOpen = true;
        }

        private void CarsToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            CarsPopup.IsOpen = false;
        }

        private void OnAddVehicleButtonClick(object sender, RoutedEventArgs e)
        {
            AddVehicleWindow addVehicleWindow = new AddVehicleWindow();
            addVehicleWindow.ShowDialog();
        }

        private void OnVehicleListButtonClick(object sender, RoutedEventArgs e)
        {
            VehicleListWindow vehicleListWindow = new VehicleListWindow();
            vehicleListWindow.ShowDialog();
        }

        private void OnVehicleArchiveListButtonClick(object sender, RoutedEventArgs e)
        {
            ArchiveVehicleWindow archiveVehicleWindow = new ArchiveVehicleWindow();
            archiveVehicleWindow.ShowDialog();
        }
        
        private void CloseAllPopups()
        {
            DocumentsPopup.IsOpen = false;
            EmployeesPopup.IsOpen = false;
        }

        private void CloseDocumentsPopup()
        {
            DocumentsPopup.IsOpen = false;
        }

        private void CloseEmployeesPopup()
        {
            EmployeesPopup.IsOpen = false;
        }
    }
}
