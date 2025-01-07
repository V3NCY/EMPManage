using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class PermissionDialog : Window
    {
        public bool HasPermission { get; private set; }

        public PermissionDialog()
        {
            InitializeComponent();
        }

        private void OnSubmitClick(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "adminJjY")
            {
                HasPermission = true;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Invalid password. Permission denied.");
                HasPermission = false;
            }
        }
    }
}
