using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem
{
    public partial class AdministrativeDepartmentWindow : Window
    {
        private Button _activeButton;

        public AdministrativeDepartmentWindow()
        {
            InitializeComponent();
        }

        private void SetActiveButton(Button button)
        {
            // Reset the style of the previously active button
            if (_activeButton != null)
            {
                _activeButton.ClearValue(Button.BackgroundProperty);
                _activeButton.ClearValue(Button.ForegroundProperty);
            }

            // Set the new active button and update its style
            _activeButton = button;
            _activeButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 55, 99)); // #073763
            _activeButton.Foreground = System.Windows.Media.Brushes.White;
        }

        private void OnDocumentsListButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            DocumentsListWindow documentsListsWindow = new DocumentsListWindow();
            MainContentControl.Content = documentsListsWindow.Content;
        }

        private void OnPersonalFilesButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            PersonalFilesWindow personalFilesWindow = new PersonalFilesWindow();
            MainContentControl.Content = personalFilesWindow.Content;
        }

        private void OnEmployeeRewardsButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            EmployeeRewardsWindow employeeRewardsWindow = new EmployeeRewardsWindow();
            MainContentControl.Content = employeeRewardsWindow.Content;
        }

        private void OnProjectsAndTasksButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            ProjectsAndTasksWindow projectsAndTasksWindow = new ProjectsAndTasksWindow();
            MainContentControl.Content = projectsAndTasksWindow.Content;
        }

        private void OnFinanceAndBudgetButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            FinanceAndBudgetWindow financeAndBudgetWindow = new FinanceAndBudgetWindow();
            MainContentControl.Content = financeAndBudgetWindow.Content;
        }

        private void OnCommunicationEventsButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            CommunicationEventsWindow communicationEventsWindow = new CommunicationEventsWindow();
            MainContentControl.Content = communicationEventsWindow.Content;
        }

        private void OnEducationDevelopmentButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            EducationDevelopmentWindow educationDevelopmentWindow = new EducationDevelopmentWindow();
            MainContentControl.Content = educationDevelopmentWindow.Content;
        }

        private void OnRulesAndPolicyButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            RulesAndPolicyWindow rulesAndPolicyWindow = new RulesAndPolicyWindow();
            MainContentControl.Content = rulesAndPolicyWindow.Content;
        }

        private void OnMaintenanceAndInfrastructureButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            MaintenanceAndInfrastructureWindow miWindow = new MaintenanceAndInfrastructureWindow();
            MainContentControl.Content = miWindow.Content;
        }

        private void OnContractListButtonClick(object sender, RoutedEventArgs e)
        {
            SetActiveButton(sender as Button);
            ContractListWindow contractListsWindow = new ContractListWindow();
            MainContentControl.Content = contractListsWindow.Content;
        }
    }
}
