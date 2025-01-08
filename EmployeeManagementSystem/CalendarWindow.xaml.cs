using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmployeeManagementSystem
{
    public partial class CalendarWindow : Window
    {
        private DateTime _currentDate = DateTime.Now; 
        private DateTime _lastClickTime; 

        public CalendarWindow()
        {
            InitializeComponent();
            GenerateCalendar();
        }

        // Generate the calendar for the current month
        private void GenerateCalendar()
        {
            DateTime firstDayOfMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            // Clear the grid before generating days
            CalendarGrid.Children.Clear();

            // Add empty cells for days before the first day of the month
            for (int i = 0; i < startDayOfWeek; i++)
            {
                CalendarGrid.Children.Add(CreateEmptyDayCell());
            }

            // Add cells for each day of the month
            for (int day = 1; day <= daysInMonth; day++)
            {
                var dayCell = CreateDayCell(day);
                CalendarGrid.Children.Add(dayCell);
            }
        }

        // Create an empty cell for padding before the first day of the month
        private Border CreateEmptyDayCell()
        {
            return new Border
            {
                Style = (Style)FindResource("CalendarDayStyle"),
                Background = new SolidColorBrush(Color.FromRgb(240, 240, 240))
            };
        }

        // Create a cell for a specific day
        private Border CreateDayCell(int day)
        {
            var dayCell = new Border
            {
                Style = (Style)FindResource("CalendarDayStyle"),
                Tag = day 
            };

            // Highlight the current day
            if (day == _currentDate.Day)
            {
                dayCell.Background = new SolidColorBrush(Color.FromRgb(230, 245, 255)); // Light blue background
                dayCell.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 150, 243)); // Blue border
                dayCell.BorderThickness = new Thickness(2);
            }

            var stackPanel = new StackPanel();
            stackPanel.Children.Add(new TextBlock
            {
                Text = day.ToString(),
                FontWeight = FontWeights.Bold,
                FontSize = 16
            });

            var taskContainer = new ItemsControl
            {
                AllowDrop = true
            };

            // Add context menu for right-click
            var contextMenu = new ContextMenu();

            var addTaskMenuItem = new MenuItem { Header = "Добави бележка" };
            addTaskMenuItem.Click += (s, e) => AddTask(day, taskContainer);
            contextMenu.Items.Add(addTaskMenuItem);

            var deleteTaskMenuItem = new MenuItem { Header = "Изтрий всички бележки" };
            deleteTaskMenuItem.Click += (s, e) => taskContainer.Items.Clear();
            contextMenu.Items.Add(deleteTaskMenuItem);

            dayCell.ContextMenu = contextMenu;

            stackPanel.Children.Add(taskContainer);
            dayCell.Child = stackPanel;

            return dayCell;
        }

        // Add a task to a specific day
        private void AddTask(int day, ItemsControl taskContainer)
        {
            // Show input dialog for task description
            string taskText = Microsoft.VisualBasic.Interaction.InputBox($"Добави бележка към дата: {day} {_currentDate:MMMM yyyy}:", "Добави бележка", "");
            if (string.IsNullOrWhiteSpace(taskText)) return;

            // Create a popup window to select category
            Window categorySelector = new Window
            {
                Title = "Избери категория",
                Width = 300,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                Owner = this
            };

            var panel = new StackPanel { Margin = new Thickness(10) };

            // Category Dropdown
            var categoryDropdown = new ComboBox
            {
                Width = 200,
                Margin = new Thickness(0, 0, 0, 10),
                ItemsSource = new string[] { "Лично", "Работно", "Спешно работно" },
                SelectedIndex = 0
            };
            panel.Children.Add(categoryDropdown);

            var okButton = new Button
            {
                Content = "OK",
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            panel.Children.Add(okButton);

            categorySelector.Content = panel;

            // Close popup and proceed with selected category
            okButton.Click += (s, e) =>
            {
                string selectedCategory = categoryDropdown.SelectedItem.ToString();
                AddTaskToContainer(taskContainer, taskText, selectedCategory);
                categorySelector.Close();
            };

            categorySelector.ShowDialog();
        }

        private void AddTaskToContainer(ItemsControl taskContainer, string taskText, string category)
        {
            // Assign a style based on the category
            string styleKey;
            switch (category)
            {
                case "Лично":
                    styleKey = "PersonalTaskStyle";
                    break;
                case "Работно":
                    styleKey = "WorkTaskStyle";
                    break;
                case "Спешно работно":
                    styleKey = "UrgentTaskStyle";
                    break;
                default:
                    styleKey = "TaskStyle"; 
                    break;
            }

            // Create a task element
            var task = new Border
            {
                Style = (Style)FindResource(styleKey),
                Child = new TextBlock { Text = taskText }
            };

            // Enable drag-and-drop functionality
            task.PreviewMouseMove += Task_PreviewMouseMove;
            task.MouseLeftButtonUp += (s, e) => HandleMouseClick(task);

            taskContainer.Items.Add(task);
        }

        // Detect single and double-clicks
        private void HandleMouseClick(Border task)
        {
            DateTime now = DateTime.Now;
            if ((now - _lastClickTime).TotalMilliseconds < 500)
            {
                EditTask(task); 
            }
            _lastClickTime = now;
        }

        // Edit a task on double-click
        private void EditTask(Border task)
        {
            if (task.Child is TextBlock textBlock)
            {
                string updatedText = Microsoft.VisualBasic.Interaction.InputBox("Edit Task:", "Edit Task", textBlock.Text);
                if (!string.IsNullOrWhiteSpace(updatedText))
                {
                    textBlock.Text = updatedText;
                }
            }
        }

        // Handle dragging tasks
        private void Task_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is Border task)
            {
                DragDrop.DoDragDrop(task, task, DragDropEffects.Move);
            }
        }

        // Handle dropping tasks on a day cell
        private void Task_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Border)) is Border task && sender is ItemsControl targetContainer)
            {
                var parent = VisualTreeHelper.GetParent(task) as ItemsControl;
                parent?.Items.Remove(task);

                targetContainer.Items.Add(task);
            }
        }
    }
}
