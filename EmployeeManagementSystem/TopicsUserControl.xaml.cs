using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            if (IsInputValid())
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
            else
            {
                MessageBox.Show("Моля, попълнете всички полета, преди да добавите нова тема.",
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        private void OnDeleteTopicButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedTopic = TopicsDataGrid.SelectedItem as Topic;

            if (selectedTopic != null)
            {
                if (IsTopicFilled(selectedTopic))
                {
                    MessageBoxResult result = MessageBox.Show("Сигурни ли сте, че искате да изтриете тази тема?",
                                                              "Confirm Deletion",
                                                              MessageBoxButton.YesNo,
                                                              MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        _topics.Remove(selectedTopic);
                    }
                }
                else
                {
                    MessageBox.Show("Грешка! Полето все още не е попълно и не може да бъде изтрито!",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Моля първо изберете темата, която искате да изтриете!",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void OnTopicsDataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TopicsDataGrid.SelectedItem is Topic selectedTopic)
            {
                if (IsTopicFilled(selectedTopic))
                {
                    TopicNameTextBox.Text = selectedTopic.TopicName;
                    TopicCreditsTextBox.Text = selectedTopic.Credits;
                    TopicHoursTextBox.Text = selectedTopic.Hours;
                    ProgramIncludeTextBox.Text = selectedTopic.ProgramIncludes;
                }
                else
                {
                    ClearInputFields();
                }
            }
            else
            {
                ClearInputFields();
            }
        }
        //private void OnDataGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (TopicsDataGrid.SelectedItem is Topic selectedTopic)
        //    {
        //        if (IsTopicFilled(selectedTopic))
        //        {
        //        }
        //        else
        //        {
        //            MessageBox.Show("Unable to delete, please fill first.",
        //                            "Error",
        //                            MessageBoxButton.OK,
        //                            MessageBoxImage.Warning);
        //        }
        //    }
        //}

        private void ClearInputFields()
        {

            TopicNameTextBox.Clear();
            TopicCreditsTextBox.Clear();
            TopicHoursTextBox.Clear();
            ProgramIncludeTextBox.Clear();
        }

        private bool IsTopicFilled(Topic topic)
        {
            return !string.IsNullOrWhiteSpace(topic.TopicName) &&
                   !string.IsNullOrWhiteSpace(topic.Credits) &&
                   !string.IsNullOrWhiteSpace(topic.Hours) &&
                   !string.IsNullOrWhiteSpace(topic.ProgramIncludes);
        }

        private bool IsInputValid()
        {
            return !string.IsNullOrWhiteSpace(TopicNameTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(TopicCreditsTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(TopicHoursTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(ProgramIncludeTextBox.Text);
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
