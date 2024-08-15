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

        private void OnProjectsAndTasksButtonClick(object sender, RoutedEventArgs e)
        {
            ProjectsAndTasksWindow projectsAndTasksWindow = new ProjectsAndTasksWindow();

            MainContentControl.Content = projectsAndTasksWindow.Content;
        }

        private void OnFinanceAndBudgetButtonClick(object sender, RoutedEventArgs e)
        {
            FinanceAndBudgetWindow financeAndBudgetWindow = new FinanceAndBudgetWindow();

            MainContentControl.Content = financeAndBudgetWindow.Content;
        }

        private void OnCommunicationEventsButtonClick(object sender, RoutedEventArgs e)
        {
            CommunicationEventsWindow communicationEventsWindow = new CommunicationEventsWindow();

            MainContentControl.Content = communicationEventsWindow.Content;
        }
        private void OnEducationDevelopmentButtonClick(object sender, RoutedEventArgs e)
        {
            EducationDevelopmentWindow educationDevelopmentWindow = new EducationDevelopmentWindow();

            MainContentControl.Content = educationDevelopmentWindow.Content;
        }

        private void OnRulesAndPolicyButtonClick(object sender, RoutedEventArgs e)
        {
            RulesAndPolicyWindow rulesAndPolicyWindow = new RulesAndPolicyWindow();

            MainContentControl.Content = rulesAndPolicyWindow.Content;
        }

        private void OnMaintenanceAndInfrastructureButtonClick(object sender, RoutedEventArgs e)
        {
            MaintenanceAndInfrastructureWindow miWindow = new MaintenanceAndInfrastructureWindow();

            MainContentControl.Content = miWindow.Content;
        }

    }
}
