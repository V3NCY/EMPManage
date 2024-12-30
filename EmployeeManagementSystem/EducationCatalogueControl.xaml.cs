using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32; 

namespace EmployeeManagementSystem
{
    public partial class EducationCatalogueControl : UserControl
    {
    public ListBox ArchivedCatalogsListBox;
        public EducationCatalogueControl()
        {
            InitializeComponent();
        }

        private void OnAddNewCatalogClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|PDF Files (*.pdf)|*.pdf|Word Documents (*.docx)|*.docx|Excel Files (*.xlsx)|*.xlsx|PowerPoint Files (*.ppt;*.pptx)|*.ppt;*.pptx",
                Multiselect = false
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                VisualizeFile(filePath); 
            }
        }

        private void VisualizeFile(string filePath)
        {
            ContentViewer.Content = null; 

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("The specified file does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string extension = Path.GetExtension(filePath).ToLower();

            switch (extension)
            {
                case ".png":
                case ".jpg":
                case ".jpeg":
                    DisplayImage(filePath);
                    break;

                case ".pdf":
                    DisplayPdf(filePath);
                    break;

                case ".docx":
                case ".xlsx":
                case ".ppt":
                case ".pptx":
                    DisplayDocumentPlaceholder();
                    break;

                default:
                    MessageBox.Show("File type not supported for visualization.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void DisplayImage(string filePath)
        {
            try
            {
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(filePath, UriKind.Absolute)),
                    Stretch = Stretch.Uniform,
                    MaxWidth = ContentViewer.ActualWidth,
                    MaxHeight = ContentViewer.ActualHeight
                };

                ContentViewer.Content = image;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisplayPdf(string filePath)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisplayDocumentPlaceholder()
        {
            var documentTextBlock = new TextBlock
            {
                Text = "Document content cannot be displayed directly. Please open the document using the appropriate application.",
                Foreground = Brushes.Gray,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = ContentViewer.ActualWidth
            };

            ContentViewer.Content = documentTextBlock;
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Търси...")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Търси...";
                textBox.Foreground = Brushes.Gray;
            }
        }

        


    }
}
