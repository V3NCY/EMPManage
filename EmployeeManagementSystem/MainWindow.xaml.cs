using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
      
        // Document Request Features
        private void OnSickLeaveRequestsButtonClick(object sender, RoutedEventArgs e)
        {
            SickLeaveWindow sickLeaveWindow = new SickLeaveWindow();
            sickLeaveWindow.ShowDialog();
            DocumentsToggleButton.IsChecked = false;
        }

        private void OnOtherLeaveRequestsButtonClick(object sender, RoutedEventArgs e)
        {
            OtherLeaveWindow specialRequestsWindow = new OtherLeaveWindow();
            specialRequestsWindow.ShowDialog();
            DocumentsToggleButton.IsChecked = false;
        }

        private void OnPaidLeaveRequestsButtonClick(object sender, RoutedEventArgs e)
        {
            PaidLeaveWindow paidLeaveWindow = new PaidLeaveWindow();
            paidLeaveWindow.ShowDialog();
            DocumentsToggleButton.IsChecked = false;
        }

        // Employee Management Features
        private void OnAddEmployeeButtonClick(object sender, RoutedEventArgs e)
        {
            AddEmployeesWindow addEmployeesWindow = new AddEmployeesWindow();
            addEmployeesWindow.ShowDialog();
            EmployeesToggleButton.IsChecked = false;
        }

        private void OnEmployeeListButtonClick(object sender, RoutedEventArgs e)
        {
            EmployeeListWindow employeeListWindow = new EmployeeListWindow();
            employeeListWindow.ShowDialog();
            EmployeesToggleButton.IsChecked = false;
        }

        // Archive Employee Management Features
        private void OnArchiveListButtonClick(object sender, RoutedEventArgs e)
        {
            ArchiveListWindow archiveListWindow = new ArchiveListWindow();
            archiveListWindow.ShowDialog();
            EmployeesToggleButton.IsChecked = false;
        }

        private void OnPersonalFilesButtonClick(object sender, RoutedEventArgs e)
        {
            PersonalFilesWindow personalFilesWindow = new PersonalFilesWindow();
            personalFilesWindow.ShowDialog();
            EmployeesToggleButton.IsChecked = false;
        }

        // Documents Features
        private void DocumentsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            CloseEmployeesPopup();
            DocumentsPopup.IsOpen = true;
        }

        private void DocumentsToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            DocumentsPopup.IsOpen = false;
        }

        private void EmployeesToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            CloseDocumentsPopup();
            EmployeesPopup.IsOpen = true;
        }

        private void EmployeesToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            EmployeesPopup.IsOpen = false;
        }

        // Employee Management Features
        private void VehiclesToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            VehiclesPopup.IsOpen = true;
        }

        private void VehiclesToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            VehiclesPopup.IsOpen = false;
        }
        // Vehicles Features
        private void OnAddVehicleButtonClick(object sender, RoutedEventArgs e)
        {
            AddVehicleWindow addVehicleWindow = new AddVehicleWindow();
            addVehicleWindow.ShowDialog();
        }

        private void OnVehicleListButtonClick(object sender, RoutedEventArgs e)
        {
            VehicleListWindow vehicleListWindow = new VehicleListWindow();
            vehicleListWindow.ShowDialog();
        }

        private void OnVehicleArchiveListButtonClick(object sender, RoutedEventArgs e)
        {
            ArchiveVehicleWindow archiveVehicleWindow = new ArchiveVehicleWindow();
            archiveVehicleWindow.ShowDialog();
        }
        //References Features

        private void ReferencesToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ReferencesPopup.IsOpen = true;
        }

        private void OnAddReferenceButtonClick(object sender, RoutedEventArgs e)
        {
            AddReferenceWindow addReferenceWindow = new AddReferenceWindow();
            addReferenceWindow.ShowDialog();
        }

        private void OnReferencesListButtonClick(object sender, RoutedEventArgs e)
        {
            ReferencesWindow referencesWindow = new ReferencesWindow();
            referencesWindow.ShowDialog();
        }

        //Contracts Features

        private void ContractsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ContractsPopup.IsOpen = true;
        }

        private void OnAddContractButtonClick(object sender, RoutedEventArgs e)
        {
            AddContractWindow addContractWindow = new AddContractWindow();
            addContractWindow.ShowDialog();
        }

        private void OnContractListButtonClick(object sender, RoutedEventArgs e)
        {
            ContractListWindow contractsWindow = new ContractListWindow();
            contractsWindow.ShowDialog();
        }

       
        //Departments Features

        private void DepartmentsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            DepartmentsPopup.IsOpen = true;
        }

        private void OnAdministrativeDepartmentButtonClick(object sender, RoutedEventArgs e)
        {
            AdministrativeDepartmentWindow administrativeWindow = new AdministrativeDepartmentWindow();
            administrativeWindow.ShowDialog();
        }

        private void OnEducationDepartmentButtonClick(object sender, RoutedEventArgs e)
        {
            EducationDepartmentWindow educationWindow = new EducationDepartmentWindow();
            educationWindow.ShowDialog();
        }

        private void OnCorporateDepartmentButtonClick(object sender, RoutedEventArgs e)
        {
            CorporateDepartmentWindow corporateWindow = new CorporateDepartmentWindow();
            corporateWindow.ShowDialog();
        }
        private void OnDevDepartmentButtonClick(object sender, RoutedEventArgs e)
        {
            DevDepartmentWindow devWindow = new DevDepartmentWindow();
            devWindow.ShowDialog();
        }

        //HR Features

        private void HRToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            HRPopup.IsOpen = true;
        }

        private void OnSurveyButtonClick(object sender, RoutedEventArgs e)
        {
            SurveyHRWindow surveyWindow = new SurveyHRWindow();
            surveyWindow.ShowDialog();
        }

        private void OnCandidatesButtonClick(object sender, RoutedEventArgs e)
        {
            CandidatesHRWindow candidatesWindow = new CandidatesHRWindow();
            candidatesWindow.ShowDialog();
        }

        private void OnEventsButtonClick(object sender, RoutedEventArgs e)
        {
            EventsHRWindow eventsWindow = new EventsHRWindow();
            eventsWindow.ShowDialog();
        }
        private void OnJobOffersButtonClick(object sender, RoutedEventArgs e)
        {
            JobOfferHRWindow jobOfferWindow = new JobOfferHRWindow();
            jobOfferWindow.ShowDialog();
        }
        private void OnJobButtonClick(object sender, RoutedEventArgs e)
        {
            OfficialJobHRWindow officialJobOfferWindow = new OfficialJobHRWindow();
            officialJobOfferWindow.ShowDialog();
        }
        private void OnCVsButtonClick(object sender, RoutedEventArgs e)
        {
            CVsHRWindow cvsWindow = new CVsHRWindow();
            cvsWindow.ShowDialog();
        }
        private void OnInternalRulesButtonClick(object sender, RoutedEventArgs e)
        {
            InternalRulesHRWindow internalRulesWindow = new InternalRulesHRWindow();
            internalRulesWindow.ShowDialog();
        }
        private void OnCertificatesButtonClick(object sender, RoutedEventArgs e)
        {
            CertificatesHRWindow certificatesWindow = new CertificatesHRWindow();
            certificatesWindow.ShowDialog();
        }
        private void OnHealthDocsButtonClick(object sender, RoutedEventArgs e)
        {
            HealthDocsHRWindow healthDocsWindow = new HealthDocsHRWindow();
            healthDocsWindow.ShowDialog();
        }

        //Payments Features

        private void PaymentsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            PaymentsPopup.IsOpen = true;
        }

        private void OnReceiptsButtonClick(object sender, RoutedEventArgs e)
        {
            ReceiptsWindow receiptsWindow = new ReceiptsWindow();
            receiptsWindow.ShowDialog();
        }

        private void OnPayrollsButtonClick(object sender, RoutedEventArgs e)
        {
            PayrollsWindow payrollsWindow = new PayrollsWindow();
            payrollsWindow.ShowDialog();
        }

        //Calendar Features

        private void CalendarToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = true;
        }

        private void OnCalendarButtonClick(object sender, RoutedEventArgs e)
        {
            CalendarWindow calendarWindow = new CalendarWindow();
            calendarWindow.ShowDialog();
        }
        private void EventCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("SelectedDatesChanged event fired!");

            foreach (var date in e.AddedItems)
            {
                Debug.WriteLine($"Selected Date: {date}");
            }
        }

        //Close Features
        private void CloseAllPopups()
        {
            DocumentsPopup.IsOpen = false;
            EmployeesPopup.IsOpen = false;
        }

        private void CloseDocumentsPopup()
        {
            DocumentsPopup.IsOpen = false;
        }

        private void CloseEmployeesPopup()
        {
            EmployeesPopup.IsOpen = false;
        }
    }
}