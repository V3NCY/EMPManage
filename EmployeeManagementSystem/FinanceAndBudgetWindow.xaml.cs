using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem
{
    public partial class FinanceAndBudgetWindow : Window
    {
        public FinanceAndBudgetWindow()
        {
            InitializeComponent();
            LoadFinancialData();
        }

        private void LoadFinancialData()
        {

            TotalRevenueTextBlock.Text = "Total Revenue: $123,456";
            TotalExpensesTextBlock.Text = "Total Expenses: $78,900";
            NetIncomeTextBlock.Text = "Net Income: $44,556";

        }

        private void OnViewReportsClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Displaying detailed reports...");
        }

        private void OnAddRecordClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add new record functionality.");
        }

        private void OnEditRecordClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit existing record functionality.");
        }

        private void OnDeleteRecordClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete record functionality.");
        }
    }
}
