using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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

            int row = 1;
            int col = startDay;

            for (int day = 1; day <= daysInMonth; day++)
            {
                var cell = new Border
                {
                    Style = (Style)this.Resources["CalendarCellStyle"],
                    Background = Brushes.White // Default background color
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
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 17, // Set font size
                    FontWeight = FontWeights.Normal // Set font weight
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

                bool isCurrentDate = date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month && day == DateTime.Now.Day;
                if (isCurrentDate)
                {
                    cell.Background = Brushes.Lavender; // Highlight current date
                }

                Grid.SetRow(cell, row);
                Grid.SetColumn(cell, col);

                var contextMenu = new ContextMenu();

                // Add color selection menu items on right click
                var colorsMenu = new MenuItem { Header = "Избери цвят" };
                AddColorMenuItem(colorsMenu, "Червен", Brushes.Red);
                AddColorMenuItem(colorsMenu, "Син", Brushes.LightBlue);
                AddColorMenuItem(colorsMenu, "Желен", Brushes.LightGreen);
                AddColorMenuItem(colorsMenu, "Жълт", Brushes.Yellow);

                contextMenu.Items.Add(colorsMenu);

                var addNoteMenuItem = new MenuItem { Header = "Добави бележка" };
                addNoteMenuItem.Click += (s, e) => AddOrEditNote_Click(cell, noteIndicator, false);
                contextMenu.Items.Add(addNoteMenuItem);

                var editNoteMenuItem = new MenuItem { Header = "Редактирай бележка" };
                editNoteMenuItem.Click += (s, e) => AddOrEditNote_Click(cell, noteIndicator, true);
                contextMenu.Items.Add(editNoteMenuItem);

                var deleteNoteMenuItem = new MenuItem { Header = "Изтрий бележка" };
                deleteNoteMenuItem.Click += (s, e) => DeleteNote_Click(noteIndicator);
                contextMenu.Items.Add(deleteNoteMenuItem);

                var removeColorMenuItem = new MenuItem { Header = "Премахни цвят" };
                removeColorMenuItem.Click += (s, e) => RemoveColor_Click(cell, isCurrentDate);
                contextMenu.Items.Add(removeColorMenuItem);

                cell.ContextMenu = contextMenu;
                UpdateContextMenu(cell, isCurrentDate);

                CalendarGrid.Children.Add(cell);

                col++;
                if (col > 6)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private void AddColorMenuItem(MenuItem parentMenu, string colorName, Brush colorBrush)
        {
            var colorMenuItem = new MenuItem();

            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            var colorRectangle = new Rectangle
            {
                Width = 16,
                Height = 16,
                Fill = colorBrush,
                Margin = new Thickness(0, 0, 5, 0)
            };
            var textBlock = new TextBlock { Text = colorName };

            stackPanel.Children.Add(colorRectangle);
            stackPanel.Children.Add(textBlock);

            colorMenuItem.Header = stackPanel;
            colorMenuItem.Click += ColorMenuItem_Click;

            parentMenu.Items.Add(colorMenuItem);
        }

        private void UpdateCurrentDate()
        {
            OnPropertyChanged(nameof(IsCurrentDate));
        }

        private void AddOrEditNote_Click(Border cell, TextBlock noteIndicator, bool isEdit)
        {
            var noteWindow = new Window
            {
                Title = isEdit ? "Редактирай бележка" : "Добави бележка",
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

            var saveButton = new Button { Content = "Запази", Margin = new Thickness(10) };
            saveButton.Click += (s, e) =>
            {
                var note = textBox.Text;
                noteIndicator.Text = "Бележката е добавена";
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

        private void RemoveColor_Click(Border cell, bool isCurrentDate)
        {
            if (!isCurrentDate)
            {
                cell.Background = Brushes.White;
                UpdateContextMenu(cell, isCurrentDate);
            }
        }

        private void ColorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is MenuItem parentMenu)
            {
                Border border = ((ContextMenu)parentMenu.Parent).PlacementTarget as Border;
                if (border != null)
                {
                    var stackPanel = (StackPanel)menuItem.Header;
                    var textBlock = (TextBlock)stackPanel.Children[1];
                    var colorName = textBlock.Text;

                    switch (colorName)
                    {
                        case "Червен":
                            border.Background = Brushes.Red;
                            break;
                        case "Син":
                            border.Background = Brushes.LightBlue;
                            break;
                        case "Желен":
                            border.Background = Brushes.LightGreen;
                            break;
                        case "Жълт":
                            border.Background = Brushes.Yellow;
                            break;
                    }

                    UpdateContextMenu(border, false);
                }
            }
        }

        private void UpdateContextMenu(Border cell, bool isCurrentDate)
        {
            var contextMenu = cell.ContextMenu;
            var removeColorMenuItem = contextMenu.Items[contextMenu.Items.Count - 1] as MenuItem;
            if (removeColorMenuItem != null)
            {
                removeColorMenuItem.Visibility = cell.Background != Brushes.White && !isCurrentDate ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsCurrentDate
        {
            get { return DateTime.Today == _currentDate; }
        }
    }
}
