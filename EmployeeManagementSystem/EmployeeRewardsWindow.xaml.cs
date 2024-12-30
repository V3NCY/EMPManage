using System.Collections.Generic;
using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class EmployeeRewardsWindow : Window
    {
        public EmployeeRewardsWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            //var monthlyRewards = new List<EmployeeReward>
            //{
            //    new EmployeeReward { EmployeeName = "Иван Иванов", Month = "Януари", Amount = 1000 },
            //    new EmployeeReward { EmployeeName = "Мария Петрова", Month = "Февруари", Amount = 1200 }
            //};

            //var additionalBonuses = new List<EmployeeBonus>
            //{
            //    new EmployeeBonus { EmployeeName = "Иван Иванов", Date = "2024-07-15", BonusAmount = 200 },
            //    new EmployeeBonus { EmployeeName = "Мария Петрова", Date = "2024-08-01", BonusAmount = 150 }
            //};

            //RewardsDataGrid.ItemsSource = monthlyRewards;
            //BonusesDataGrid.ItemsSource = additionalBonuses;
        }

        private void OnMonthlyRewardsButtonClick(object sender, RoutedEventArgs e)
        {
            MainContentTabControl.SelectedIndex = 0;
        }

        private void OnAdditionalBonusesButtonClick(object sender, RoutedEventArgs e)
        {
            MainContentTabControl.SelectedIndex = 1;
        }

        private void OnAddRewardButtonClick(object sender, RoutedEventArgs e)
        {
            var addRewardWindow = new AddRewardWindow();
            addRewardWindow.ShowDialog();
        }

        private void OnAddBonusButtonClick(object sender, RoutedEventArgs e)
        {
            var addBonusWindow = new AddBonusWindow();
            addBonusWindow.ShowDialog();
        }

        public class EmployeeBonus
        {
            public string ID { get; set; }
            public string EmployeeName { get; set; }
            public string Date { get; set; }
            public decimal BonusAmount { get; set; }
        }

        public class EmployeeReward
        {
            public string ID { get; set; }
            public string EmployeeName { get; set; }
            public string Month { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
