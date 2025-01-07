using System.Collections.Generic;
using System.Data.Odbc;
using System;
using System.Windows;

namespace EmployeeManagementSystem
{
    public partial class ArchiveListWindow : Window
    {
        public ArchiveListWindow()
        {
            InitializeComponent();
            LoadArchivedEmployees(); // Call the method when the window opens
        }

        private void LoadArchivedEmployees()
        {
            string connectionString = "DSN=SSManagement32;Server=ORAK-VMANDULOVA;DatabaseName=DB-Employees;UID=dba;PWD=sql;";
            string query = "SELECT ArchiveId, EmployeeId, FullName, EGN, JobTitle, Department, ArchivedDate FROM ArchivedEmployees";

            var archivedEmployeesList = new List<ArchivedEmployee>();

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                archivedEmployeesList.Add(new ArchivedEmployee
                                {
                                    ArchiveId = reader.GetInt32(0),
                                    EmployeeId = reader.GetInt32(1),
                                    FullName = reader.GetString(2),
                                    EGN = reader.GetString(3),
                                    JobTitle = reader.GetString(4),
                                    Department = reader.GetString(5),
                                    ArchivedDate = reader.GetDateTime(6)
                                });
                            }
                        }
                    }
                }

                // Bind the archived employees list to the ListView in the ArchiveListWindow
                lvArchivedEmployees.ItemsSource = null; // Clear current binding
                lvArchivedEmployees.ItemsSource = archivedEmployeesList; // Bind new list
                lvArchivedEmployees.Items.Refresh(); // Refresh the UI
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading archived employees: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnDeleteEmployeeMenuItemClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnDeleteAllItemsClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
