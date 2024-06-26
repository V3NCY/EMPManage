using System.Collections.Generic;
using System.IO;
using System;
using System.Windows;
using System.Windows.Controls;
using OfficeOpenXml;

namespace EmployeeManagementSystem
{
    public partial class AddReferenceWindow : Window
    {
        public AddReferenceWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ReferenceData.xlsx");
                var fileInfo = new FileInfo(filePath);

                using (var package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet;
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        worksheet = package.Workbook.Worksheets.Add("References");
                    }
                    else
                    {
                        worksheet = package.Workbook.Worksheets[0];
                    }

                    worksheet.Cells["A1"].Value = "Reference Type";
                    worksheet.Cells["B1"].Value = "Period";
                    worksheet.Cells["C1"].Value = "Box 1";
                    worksheet.Cells["D1"].Value = "Box 2";
                    worksheet.Cells["E1"].Value = "Box 3";

                    worksheet.Cells["A2"].Value = ReferenceTypeComboBox.SelectedItem.ToString();
                    worksheet.Cells["B2"].Value = PeriodCalendar.SelectedDate?.ToString("yyyy-MM-dd") ?? "Not Selected";
                    worksheet.Cells["C2"].Value = CheckBox1.IsChecked ?? false;
                    worksheet.Cells["D2"].Value = CheckBox2.IsChecked ?? false;
                    worksheet.Cells["E2"].Value = CheckBox3.IsChecked ?? false;

                    package.Save();
                }

                MessageBox.Show("Data saved to Excel file on desktop.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string excelFilePath = @"C:\Users\orak\Desktop\Reference.xlsx"; 

            if (File.Exists(excelFilePath))
            {
                System.Diagnostics.Process.Start(excelFilePath);
            }
            else
            {
                MessageBox.Show("The Excel file does not exist or could not be found.");
            }
        }
    }
}
