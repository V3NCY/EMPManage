using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class EducationDepartmentWindow : Window
    {
        public EducationDepartmentWindow()
        {
            InitializeComponent();
        }

        private void OnTopicsButtonClick(object sender, RoutedEventArgs e)
        {
            TopicsUserControl topicsControl = new TopicsUserControl();
            MainContentControl.Content = topicsControl;
        }

        private void OnEducationCatalogueControlButtonClick(object sender, RoutedEventArgs e)
        {
            EducationCatalogueControl catalogueControl= new EducationCatalogueControl();
            MainContentControl.Content = catalogueControl;
        }



    }
}
