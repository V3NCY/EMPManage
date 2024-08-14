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
    }
}
