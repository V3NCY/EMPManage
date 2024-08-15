using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmployeeManagementSystem
{
    public partial class EducationCatalogueControl : UserControl
    {
        public EducationCatalogueControl()
        {
            InitializeComponent();
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Търси...")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Търси...";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void OnAddNewCatalogClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
