using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class ProjectsAndTasksWindow : Window
    {
        public ProjectsAndTasksWindow()
        {
            InitializeComponent();
        }

        private void OnAddProjectClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add Project Clicked!");
        }

        private void OnEditProjectClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit Project Clicked!");
        }

        private void OnDeleteProjectClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete Project Clicked!");
        }

        private void OnSaveTaskClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save Task Clicked!");
        }

        private void OnDeleteTaskClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete Task Clicked!");
        }
    }
}
