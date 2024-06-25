using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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

        private void DocumentsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            CloseAllPopups();
            DocumentsPopup.IsOpen = true;
        }

        private void DocumentsToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            DocumentsPopup.IsOpen = false;
        }

        private void CloseAllPopups()
        {
            DocumentsPopup.IsOpen = false;
        }
    }
}
