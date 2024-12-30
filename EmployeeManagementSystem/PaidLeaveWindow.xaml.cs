using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeManagementSystem
{
    public partial class PaidLeaveWindow : Window
    {
        public PaidLeaveWindow()
        {
            InitializeComponent();
        }
        private void OnLeaveRequestsButtonClick(object sender, RoutedEventArgs e)
        {
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Paid leave request saved.");
            this.Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
