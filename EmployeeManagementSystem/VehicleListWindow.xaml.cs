using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class VehicleListWindow : Window
    {
        public VehicleListWindow()
        {
            InitializeComponent();
        }

        private void lvCars_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Handle the selection change event
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            // Handle the refresh button click event
            // You can add logic here to refresh the ListView items
        }
    }
}
