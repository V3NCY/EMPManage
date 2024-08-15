using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem
{
    public partial class TopicsUserControl : UserControl
    {
        private ObservableCollection<Topic> _topics;

        public TopicsUserControl()
        {
            InitializeComponent();
            _topics = new ObservableCollection<Topic>();
            TopicsDataGrid.ItemsSource = _topics;
        }

        private void OnUploadTopicClick(object sender, RoutedEventArgs e)
        {
            var newTopic = new Topic
            {
                TopicName = TopicNameTextBox.Text,
                Credits = TopicCreditsTextBox.Text,
                Hours = TopicHoursTextBox.Text,
                ProgramIncludes = ProgramIncludeTextBox.Text
            };

            _topics.Add(newTopic);
            ClearInputFields();
        }

        private void OnSaveChangesClick(object sender, RoutedEventArgs e)
        {
            if (TopicsDataGrid.SelectedItem is Topic selectedTopic)
            {
                selectedTopic.TopicName = TopicNameTextBox.Text;
                selectedTopic.Credits = TopicCreditsTextBox.Text;
                selectedTopic.Hours = TopicHoursTextBox.Text;
                selectedTopic.ProgramIncludes = ProgramIncludeTextBox.Text;

                TopicsDataGrid.Items.Refresh();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Моля, първо изберете темата.");
            }
        }

        private void OnTopicsDataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TopicsDataGrid.SelectedItem is Topic selectedTopic)
            {
                TopicNameTextBox.Text = selectedTopic.TopicName;
                TopicCreditsTextBox.Text = selectedTopic.Credits;
                TopicHoursTextBox.Text = selectedTopic.Hours;
                ProgramIncludeTextBox.Text = selectedTopic.ProgramIncludes;
            }
        }

        private void ClearInputFields()
        {
            TopicNameTextBox.Clear();
            TopicCreditsTextBox.Clear();
            TopicHoursTextBox.Clear();
            ProgramIncludeTextBox.Clear();
        }
    }

    public class Topic
    {
        public string TopicName { get; set; }
        public string Credits { get; set; }
        public string Hours { get; set; }
        public string ProgramIncludes { get; set; }
    }
}
