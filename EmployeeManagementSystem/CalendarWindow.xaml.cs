using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmployeeManagementSystem
{
    public partial class CalendarWindow : Window, INotifyPropertyChanged
    {
        private DateTime _currentDate;
        public event PropertyChangedEventHandler PropertyChanged;

        public CalendarWindow()
        {
            InitializeComponent();
            PopulateYearAndMonthComboBoxes();
            _currentDate = DateTime.Now;
            DataContext = this;
            CreateCalendar(_currentDate);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PopulateYearAndMonthComboBoxes()
        {
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year <= currentYear + 2; year++)
            {
                YearComboBox.Items.Add(year);
            }
            YearComboBox.SelectedIndex = 0; // Select current year by default

            for (int month = 1; month <= 12; month++)
            {
                MonthComboBox.Items.Add(new DateTime(1, month, 1).ToString("MMMM"));
            }
            MonthComboBox.SelectedIndex = DateTime.Now.Month - 1; // Select current month by default
        }

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

        private void CreateCalendar(DateTime date)
        {
            CalendarGrid.Children.Clear();

            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            int startDay = (int)firstDayOfMonth.DayOfWeek;

            int row = 0;
            int col = startDay;

            for (int day = 1; day <= daysInMonth; day++)
            {
                var cell = new Border
                {
                    Style = (Style)this.Resources["CalendarCellStyle"]
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
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                var noteIndicator = new TextBlock
                {
                    Text = "",
                    Foreground = Brushes.Red,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                cellContent.Children.Add(dateText);
                cellContent.Children.Add(noteIndicator);

                cell.Child = cellContent;

                Grid.SetRow(cell, row);
                Grid.SetColumn(cell, col);

                var contextMenu = new ContextMenu();

                // Add color selection menu items
                var colorsMenu = new MenuItem { Header = "Select Color" };
                foreach (var color in new string[] { "White", "LightBlue", "LightGreen", "Yellow" })
                {
                    var colorMenuItem = new MenuItem { Header = color };
                    colorMenuItem.Click += ColorMenuItem_Click;
                    colorsMenu.Items.Add(colorMenuItem);
                }
                contextMenu.Items.Add(colorsMenu);

                var addNoteMenuItem = new MenuItem { Header = "Add Note" };
                addNoteMenuItem.Click += (s, e) => AddOrEditNote_Click(cell, noteIndicator, false);
                contextMenu.Items.Add(addNoteMenuItem);

                var editNoteMenuItem = new MenuItem { Header = "Edit Note" };
                editNoteMenuItem.Click += (s, e) => AddOrEditNote_Click(cell, noteIndicator, true);
                contextMenu.Items.Add(editNoteMenuItem);

                var deleteNoteMenuItem = new MenuItem { Header = "Delete Note" };
                deleteNoteMenuItem.Click += (s, e) => DeleteNote_Click(noteIndicator);
                contextMenu.Items.Add(deleteNoteMenuItem);

                cell.ContextMenu = contextMenu;

                CalendarGrid.Children.Add(cell);

                col++;
                if (col > 6)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private void UpdateCurrentDate()
        {
            OnPropertyChanged(nameof(IsCurrentDate));
        }

        private void AddOrEditNote_Click(Border cell, TextBlock noteIndicator, bool isEdit)
        {
            var noteWindow = new Window
            {
                Title = isEdit ? "Edit Note" : "Add Note",
                Width = 500,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            var stackPanel = new StackPanel();
            var textBox = new TextBox
            {
                AcceptsReturn = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Margin = new Thickness(10),
                Text = isEdit ? (string)noteIndicator.ToolTip : ""
            };

            var saveButton = new Button { Content = "Save", Margin = new Thickness(10) };
            saveButton.Click += (s, e) =>
            {
                var note = textBox.Text;
                noteIndicator.Text = "Note Added";
                noteIndicator.ToolTip = note;
                noteWindow.Close();
            };

            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(saveButton);

            noteWindow.Content = stackPanel;
            noteWindow.ShowDialog();
        }

        private void DeleteNote_Click(TextBlock noteIndicator)
        {
            noteIndicator.Text = "";
            noteIndicator.ToolTip = null;
        }

        private void ColorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is MenuItem parentMenu)
            {
                // Find the border that triggered the context menu
                Border border = ((ContextMenu)parentMenu.Parent).PlacementTarget as Border;
                if (border != null)
                {
                    // Set the background color based on selected color
                    switch (menuItem.Header.ToString())
                    {
                        case "White":
                            border.Background = Brushes.White;
                            break;
                        case "LightBlue":
                            border.Background = Brushes.LightBlue;
                            break;
                        case "LightGreen":
                            border.Background = Brushes.LightGreen;
                            break;
                        case "Yellow":
                            border.Background = Brushes.Yellow;
                            break;
                        case "Remove Color":
                            border.Background = Brushes.Transparent; // or any other default color you want
                            break;
                            // Add more cases for other colors as needed
                    }

                    // Check if border has background color set
                    UpdateContextMenu(border);
                }
            }
        }

        private void UpdateContextMenu(Border border)
        {
            // Find the context menu of the border
            ContextMenu contextMenu = border.ContextMenu as ContextMenu;
            if (contextMenu != null)
            {
                MenuItem removeColorMenuItem = null;

                // Find the "Remove Color" menu item
                foreach (MenuItem item in contextMenu.Items)
                {
                    if (item.Header.ToString() == "Remove Color")
                    {
                        removeColorMenuItem = item;
                        break;
                    }
                }

                // Update visibility of "Remove Color" menu item based on border background
                if (border.Background == Brushes.Transparent)
                {
                    if (removeColorMenuItem != null)
                        contextMenu.Items.Remove(removeColorMenuItem); // Remove if exists
                }
                else
                {
                    if (removeColorMenuItem == null)
                    {
                        // Create and add "Remove Color" menu item
                        removeColorMenuItem = new MenuItem { Header = "Remove Color" };
                        removeColorMenuItem.Click += (s, e) => RemoveColorMenuItem_Click(border);
                        contextMenu.Items.Add(removeColorMenuItem);
                    }
                }
            }
        }

        private void RemoveColorMenuItem_Click(Border border)
        {
            border.Background = Brushes.Transparent; // or any other default color you want
        }
        public bool IsCurrentDate
        {
            get { return DateTime.Today == _currentDate; }
        }
    }
}
