using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace EmployeeManagementSystem
{
    public partial class CalendarWindow : Window, INotifyPropertyChanged
    {
        private DateTime _currentDate;
        private Dictionary<DateTime, string> _events;

        public event PropertyChangedEventHandler PropertyChanged;

        public CalendarWindow()
        {
            InitializeComponent();
            PopulateYearAndMonthComboBoxes();
            _currentDate = DateTime.Now;
            _events = new Dictionary<DateTime, string>(); // Initialize events dictionary
            DataContext = this;
            CreateCalendar(_currentDate);
        }

        // Notify property change for dynamic UI updates
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Populate year and month ComboBoxes
        private void PopulateYearAndMonthComboBoxes()
        {
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year <= currentYear + 2; year++)
            {
                YearComboBox.Items.Add(year);
            }
            YearComboBox.SelectedIndex = 0; // Default to current year

            for (int month = 1; month <= 12; month++)
            {
                MonthComboBox.Items.Add(new DateTime(1, month, 1).ToString("MMMM"));
            }
            MonthComboBox.SelectedIndex = DateTime.Now.Month - 1; // Default to current month
        }

        // Event handler for year or month changes
        private void YearOrMonthChanged(object sender, SelectionChangedEventArgs e)
        {
            if (YearComboBox.SelectedItem != null && MonthComboBox.SelectedItem != null)
            {
                int year = (int)YearComboBox.SelectedItem;
                int month = MonthComboBox.SelectedIndex + 1;
                _currentDate = new DateTime(year, month, 1);
                CreateCalendar(_currentDate);
                UpdateCurrentDate();
            }
        }

        // Create the calendar grid dynamically
        private void CreateCalendar(DateTime date)
        {
            CalendarGrid.Children.Clear(); // Clear previous cells

            if (_events == null)  // Ensure the _events dictionary is initialized
            {
                _events = new Dictionary<DateTime, string>(); // Initialize it if it is null
            }

            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            int startDay = (int)firstDayOfMonth.DayOfWeek;

            int row = 0;
            int col = startDay;

            for (int day = 1; day <= daysInMonth; day++)
            {
                var cell = new Border
                {
                    Style = (Style)this.Resources["CalendarCellStyle"],
                    Background = Brushes.White
                };

                var cellContent = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                var dateText = new TextBlock
                {
                    Text = day.ToString(),
                    Style = (Style)this.Resources["DayTextStyle"]
                };

                cellContent.Children.Add(dateText);

                // Check if there are events for the current day and display the indicator (colored dot)
                DateTime eventDate = date.AddDays(day - 1); // Calculate the exact date
                if (_events.ContainsKey(eventDate))
                {
                    var eventIndicator = new Ellipse
                    {
                        Style = (Style)this.Resources["EventBadgeStyle"],
                        Fill = Brushes.Green, // You can customize the color for events
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    cellContent.Children.Add(eventIndicator);
                }

                cell.Child = cellContent;

                // Highlight today’s date
                if (date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month && day == DateTime.Now.Day)
                {
                    cell.Background = Brushes.LightSteelBlue; // Light color for today's date
                }

                // Context menu for adding/editing/removing events
                var contextMenu = new ContextMenu();

                var addEventMenuItem = new MenuItem { Header = "Add Event" };
                addEventMenuItem.Click += (s, e) => ShowAddEventDialog(eventDate, day, cell);
                contextMenu.Items.Add(addEventMenuItem);

                if (_events.ContainsKey(eventDate))
                {
                    var editEventMenuItem = new MenuItem { Header = "Edit Event" };
                    editEventMenuItem.Click += (s, e) => ShowAddEventDialog(eventDate, day, cell);
                    contextMenu.Items.Add(editEventMenuItem);
                }

                var removeEventMenuItem = new MenuItem { Header = "Remove Event" };
                removeEventMenuItem.Click += (s, e) => RemoveEvent(eventDate);
                contextMenu.Items.Add(removeEventMenuItem);

                cell.ContextMenu = contextMenu;

                Grid.SetRow(cell, row);
                Grid.SetColumn(cell, col);
                CalendarGrid.Children.Add(cell);

                col++;
                if (col > 6)
                {
                    col = 0;
                    row++;
                }
            }
        }

        // Show dialog for adding/editing events
        private void ShowAddEventDialog(DateTime date, int day, Border cell)
        {
            var eventDialog = new Window
            {
                Title = "Add/Edit Event",
                Width = 400,
                Height = 250,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            var stackPanel = new StackPanel();
            var textBox = new TextBox
            {
                AcceptsReturn = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Width = 350,
                Height = 150,
                Margin = new Thickness(10),
                Text = _events.ContainsKey(date) ? _events[date] : ""
            };

            var saveButton = new Button { Content = "Save", Margin = new Thickness(10) };
            saveButton.Click += (s, e) =>
            {
                var eventText = textBox.Text;
                if (string.IsNullOrWhiteSpace(eventText))
                {
                    _events.Remove(date); // Remove the event if the text is empty
                }
                else
                {
                    _events[date] = eventText; // Add or update the event
                }

                eventDialog.Close();
                CreateCalendar(_currentDate); // Refresh the calendar to reflect changes
            };

            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(saveButton);

            eventDialog.Content = stackPanel;
            eventDialog.ShowDialog();
        }

        // Remove an event
        private void RemoveEvent(DateTime date)
        {
            if (_events.ContainsKey(date))
            {
                _events.Remove(date);
                CreateCalendar(_currentDate); // Refresh the calendar after removal
            }
        }

        // Method to update current date property
        private void UpdateCurrentDate()
        {
            OnPropertyChanged(nameof(IsCurrentDate));
        }

        // Property to check if the selected date is today
        public bool IsCurrentDate
        {
            get { return DateTime.Today == _currentDate; }
        }
    }
}
