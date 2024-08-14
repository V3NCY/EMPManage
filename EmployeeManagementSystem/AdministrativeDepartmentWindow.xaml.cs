using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class AdministrativeDepartmentWindow : Window
    {
        public AdministrativeDepartmentWindow()
        {
            InitializeComponent();
        }

        private void OnPersonalFilesButtonClick(object sender, RoutedEventArgs e)
        {
            PersonalFilesWindow personalFilesWindow = new PersonalFilesWindow();

            MainContentControl.Content = personalFilesWindow.Content;
        }

        private void OnContractListButtonClick(object sender, RoutedEventArgs e)
        {
            ContractListWindow contractListsWindow = new ContractListWindow();

            MainContentControl.Content = contractListsWindow.Content;
        }

        private void OnDocumentsListButtonClick(object sender, RoutedEventArgs e)
        {
            DocumentsListWindow documentsListsWindow = new DocumentsListWindow();

            MainContentControl.Content = documentsListsWindow.Content;
        }

        private void OnEmployeeRewardsButtonClick(object sender, RoutedEventArgs e)
        {
            EmployeeRewardsWindow employeeRewardsWindow = new EmployeeRewardsWindow();

            MainContentControl.Content = employeeRewardsWindow.Content;
        }


    }
}
