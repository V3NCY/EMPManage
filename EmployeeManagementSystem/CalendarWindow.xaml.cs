using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.Json;
using Newtonsoft.Json;

namespace EmployeeManagementSystem
{
    public partial class CalendarWindow : Window
    {
        private DateTime _currentDate = DateTime.Now;
        private DateTime _lastClickTime;
        private Dictionary<int, List<TaskModel>> _tasks = new Dictionary<int, List<TaskModel>>();

        public CalendarWindow()
        {
            InitializeComponent();
            LoadTasks();
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

            if (_tasks.ContainsKey(day))
            {
                foreach (var task in _tasks[day])
                {
                    AddTaskToContainer(taskContainer, task.Text, task.Category);
                }
            }

            // Add context menu for right-click
            var contextMenu = new ContextMenu();

            var viewTasksMenuItem = new MenuItem { Header = "View Tasks" };
            viewTasksMenuItem.Click += ViewTasks_Click;
            contextMenu.Items.Add(viewTasksMenuItem);

            var addTaskMenuItem = new MenuItem { Header = "Add Task" };
            addTaskMenuItem.Click += AddTask_Click;
            contextMenu.Items.Add(addTaskMenuItem);

            var editTasksMenuItem = new MenuItem { Header = "Edit Tasks" };
            editTasksMenuItem.Click += EditTasks_Click;
            contextMenu.Items.Add(editTasksMenuItem);

            var deleteTaskMenuItem = new MenuItem { Header = "Delete All Tasks" };
            deleteTaskMenuItem.Click += DeleteTasks_Click;
            contextMenu.Items.Add(deleteTaskMenuItem);

            dayCell.ContextMenu = contextMenu;

            stackPanel.Children.Add(taskContainer);
            dayCell.Child = stackPanel;

            return dayCell;
        }

        private void ViewTasks_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu && contextMenu.PlacementTarget is Border dayCell)
            {
                if (dayCell.Child is StackPanel stackPanel && stackPanel.Children[1] is ItemsControl taskContainer)
                {
                    var tasks = new List<string>();
                    foreach (var item in taskContainer.Items)
                    {
                        if (item is Border taskBorder && taskBorder.Child is TextBlock taskText)
                        {
                            tasks.Add(taskText.Text);
                        }
                    }

                    if (tasks.Count > 0)
                    {
                        MessageBox.Show(string.Join(Environment.NewLine, tasks), $"Tasks for Day {dayCell.Tag}", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("No tasks for this day.", $"Tasks for Day {dayCell.Tag}", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu && contextMenu.PlacementTarget is Border dayCell)
            {
                if (dayCell.Child is StackPanel stackPanel && stackPanel.Children[1] is ItemsControl taskContainer)
                {
                    AddTask((int)dayCell.Tag, taskContainer);
                }
            }
        }

        private void EditTasks_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu && contextMenu.PlacementTarget is Border dayCell)
            {
                if (dayCell.Child is StackPanel stackPanel && stackPanel.Children[1] is ItemsControl taskContainer)
                {
                    foreach (var item in taskContainer.Items)
                    {
                        if (item is Border taskBorder && taskBorder.Child is TextBlock taskText)
                        {
                            string updatedText = Microsoft.VisualBasic.Interaction.InputBox("Edit Task:", "Edit Task", taskText.Text);
                            if (!string.IsNullOrWhiteSpace(updatedText))
                            {
                                taskText.Text = updatedText;
                            }
                        }
                    }
                }
            }
        }

        private void DeleteTasks_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu && contextMenu.PlacementTarget is Border dayCell)
            {
                if (dayCell.Child is StackPanel stackPanel && stackPanel.Children[1] is ItemsControl taskContainer)
                {
                    taskContainer.Items.Clear();
                    _tasks.Remove((int)dayCell.Tag);
                    MessageBox.Show("All tasks have been deleted.", "Delete Tasks");
                }
            }
        }

        // Add a task to a specific day
        private void AddTask(int day, ItemsControl taskContainer)
        {
            // Show input dialog for task description
            string taskText = Microsoft.VisualBasic.Interaction.InputBox($"Add a note for {day} {_currentDate:MMMM yyyy}:", "Add Task", "");
            if (string.IsNullOrWhiteSpace(taskText)) return;

            // Prompt category selection dynamically
            string category = SelectTaskCategory();
            if (string.IsNullOrWhiteSpace(category)) return;

            AddTaskToContainer(taskContainer, taskText, category);

            if (!_tasks.ContainsKey(day))
            {
                _tasks[day] = new List<TaskModel>();
            }

            _tasks[day].Add(new TaskModel { Day = day, Text = taskText, Category = category });
        }

        private string SelectTaskCategory()
        {
            // Popup dialog to select category
            Window categoryWindow = new Window
            {
                Title = "Select Task Category",
                Width = 300,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                Owner = this
            };

            StackPanel panel = new StackPanel { Margin = new Thickness(10) };

            ListBox categoryList = new ListBox
            {
                ItemsSource = new[] { "Personal", "Work", "Urgent" },
                Margin = new Thickness(0, 0, 0, 10)
            };
            categoryList.SelectedIndex = 0;
            panel.Children.Add(categoryList);

            Button okButton = new Button
            {
                Content = "OK",
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            okButton.Click += (s, e) => categoryWindow.Close();
            panel.Children.Add(okButton);

            categoryWindow.Content = panel;
            categoryWindow.ShowDialog();

            return categoryList.SelectedItem as string;
        }

        private void AddTaskToContainer(ItemsControl taskContainer, string taskText, string category)
        {
            // Assign a style based on the category
            string styleKey;
            if (category == "Personal")
            {
                styleKey = "PersonalTaskStyle";
            }
            else if (category == "Work")
            {
                styleKey = "WorkTaskStyle";
            }
            else if (category == "Urgent")
            {
                styleKey = "UrgentTaskStyle";
            }
            else
            {
                styleKey = "TaskStyle";
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

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            string filePath = "tasks.json";
            var tasksToSave = new List<TaskModel>();

            foreach (var day in _tasks)
            {
                tasksToSave.AddRange(day.Value);
            }

            string json = JsonConvert.SerializeObject(tasksToSave);
            File.WriteAllText(filePath, json);

        }

        private void LoadTasks()
        {
            string filePath = "tasks.json";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var loadedTasks = JsonConvert.DeserializeObject<List<TaskModel>>(json);


                foreach (var task in loadedTasks)
                {
                    if (!_tasks.ContainsKey(task.Day))
                    {
                        _tasks[task.Day] = new List<TaskModel>();
                    }
                    _tasks[task.Day].Add(task);
                }
            }
        }
    }

    public class TaskModel
    {
        public int Day { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
    }
}
