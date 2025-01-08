using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem
{
    public partial class TaskInputDialog : Window
    {
        public string TaskDescription { get; private set; }
        public string SelectedColumn { get; private set; }

        public TaskInputDialog()
        {
            InitializeComponent();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskDescription = TaskDescriptionBox.Text;
            if (ColumnSelectionBox.SelectedItem is ComboBoxItem selectedItem)
            {
                SelectedColumn = selectedItem.Content.ToString();
            }

            if (string.IsNullOrWhiteSpace(TaskDescription) || string.IsNullOrWhiteSpace(SelectedColumn))
            {
                MessageBox.Show("Please enter a task description and select a column.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
        }
    }
}
