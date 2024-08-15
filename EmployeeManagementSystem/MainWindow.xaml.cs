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
            sickLeaveWindow.Show();
            CloseDocumentsPopup();
            DocumentsToggleButton.IsChecked = false;
        }

        private void OnOtherLeaveRequestsButtonClick(object sender, RoutedEventArgs e)
        {
            OtherLeaveWindow specialRequestsWindow = new OtherLeaveWindow();
            specialRequestsWindow.Show();
            CloseDocumentsPopup();
            DocumentsToggleButton.IsChecked = false;
        }

        private void OnPaidLeaveRequestsButtonClick(object sender, RoutedEventArgs e)
        {
            PaidLeaveWindow paidLeaveWindow = new PaidLeaveWindow();
            paidLeaveWindow.Show();
            CloseDocumentsPopup();
            DocumentsToggleButton.IsChecked = false;
        }

        // Employee Management Features
        private void OnAddEmployeeButtonClick(object sender, RoutedEventArgs e)
        {
            AddEmployeesWindow addEmployeesWindow = new AddEmployeesWindow();
            addEmployeesWindow.Show();
            CloseEmployeesPopup();
            EmployeesToggleButton.IsChecked = false;
        }

        private void OnEmployeeListButtonClick(object sender, RoutedEventArgs e)
        {
            EmployeeListWindow employeeListWindow = new EmployeeListWindow();
            employeeListWindow.Show();
            CloseEmployeesPopup();
            EmployeesToggleButton.IsChecked = false;
        }

        // Archive Employee Management Features
        private void OnArchiveListButtonClick(object sender, RoutedEventArgs e)
        {
            ArchiveListWindow archiveListWindow = new ArchiveListWindow();
            archiveListWindow.Show();
            CloseEmployeesPopup();
            EmployeesToggleButton.IsChecked = false;
        }

        private void OnPersonalFilesButtonClick(object sender, RoutedEventArgs e)
        {
            PersonalFilesWindow personalFilesWindow = new PersonalFilesWindow();
            personalFilesWindow.Show();
            CloseEmployeesPopup();
            EmployeesToggleButton.IsChecked = false;
        }

        // Documents Features
        private void OnDocumentsListButtonClick(object sender, RoutedEventArgs e)
        {
            DocumentsListWindow documentsListWindow = new DocumentsListWindow();
            documentsListWindow.Show();
            CloseDocumentsPopup();
        }

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
            addVehicleWindow.Show();
            CloseVehiclesPopup();
        }

        private void OnVehicleListButtonClick(object sender, RoutedEventArgs e)
        {
            VehicleListWindow vehicleListWindow = new VehicleListWindow();
            vehicleListWindow.Show();
            CloseVehiclesPopup();
        }

        private void OnVehicleArchiveListButtonClick(object sender, RoutedEventArgs e)
        {
            ArchiveVehicleWindow archiveVehicleWindow = new ArchiveVehicleWindow();
            archiveVehicleWindow.Show();
            CloseVehiclesPopup();
        }
        //References Features

        private void ReferencesToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ReferencesPopup.IsOpen = true;
        }

        private void OnAddReferenceButtonClick(object sender, RoutedEventArgs e)
        {
            AddReferenceWindow addReferenceWindow = new AddReferenceWindow();
            addReferenceWindow.Show();
            CloseReferencesPopup();
        }

        private void OnReferencesListButtonClick(object sender, RoutedEventArgs e)
        {
            ReferencesWindow referencesWindow = new ReferencesWindow();
            referencesWindow.Show();
            CloseReferencesPopup();
        }

        //Contracts Features

        private void ContractsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ContractsPopup.IsOpen = true;
        }

        private void OnAddContractButtonClick(object sender, RoutedEventArgs e)
        {
            AddContractWindow addContractWindow = new AddContractWindow();
            addContractWindow.Show();
            CloseContractsPopup();
        }

        private void OnContractListButtonClick(object sender, RoutedEventArgs e)
        {
            ContractListWindow contractsWindow = new ContractListWindow();
            contractsWindow.Show();
            CloseContractsPopup();
        }

        //Departments Features

        private void OnHoReCaButtonClick(object sender, RoutedEventArgs e)
        {
            HoReCaDepartmentWindow horecaWindow = new HoReCaDepartmentWindow();
            horecaWindow.Show();
            CloseDepartmentsPopup();
        }
        private void DepartmentsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            DepartmentsPopup.IsOpen = true;
        }

        private void OnAdministrativeDepartmentButtonClick(object sender, RoutedEventArgs e)
        {
            AdministrativeDepartmentWindow administrativeWindow = new AdministrativeDepartmentWindow();
            administrativeWindow.Show();
            CloseDepartmentsPopup();
        }

        private void OnEducationDepartmentButtonClick(object sender, RoutedEventArgs e)
        {
            EducationDepartmentWindow educationWindow = new EducationDepartmentWindow();
            educationWindow.Show();
            CloseDepartmentsPopup();
        }

        private void OnCorporateDepartmentButtonClick(object sender, RoutedEventArgs e)
        {
            CorporateDepartmentWindow corporateWindow = new CorporateDepartmentWindow();
            corporateWindow.Show();
            CloseDepartmentsPopup();
        }
        private void OnDevDepartmentButtonClick(object sender, RoutedEventArgs e)
        {
            DevDepartmentWindow devWindow = new DevDepartmentWindow();
            devWindow.Show();
            CloseDepartmentsPopup();
        }

        //HR Features

        private void HRToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            HRPopup.IsOpen = true;
        }

        private void OnSurveyButtonClick(object sender, RoutedEventArgs e)
        {
            SurveyHRWindow surveyWindow = new SurveyHRWindow();
            surveyWindow.Show();
            CloseHRPopup();
        }

        private void OnCandidatesButtonClick(object sender, RoutedEventArgs e)
        {
            CandidatesHRWindow candidatesWindow = new CandidatesHRWindow();
            candidatesWindow.Show();
            CloseHRPopup();
        }

        private void OnEventsButtonClick(object sender, RoutedEventArgs e)
        {
            EventsHRWindow eventsWindow = new EventsHRWindow();
            eventsWindow.Show();
            CloseHRPopup();
        }
        private void OnJobOffersButtonClick(object sender, RoutedEventArgs e)
        {
            JobOfferHRWindow jobOfferWindow = new JobOfferHRWindow();
            jobOfferWindow.Show();
            CloseHRPopup();
        }
        private void OnJobButtonClick(object sender, RoutedEventArgs e)
        {
            OfficialJobHRWindow officialJobOfferWindow = new OfficialJobHRWindow();
            officialJobOfferWindow.Show();
            CloseHRPopup();
        }
        private void OnCVsButtonClick(object sender, RoutedEventArgs e)
        {
            CVsHRWindow cvsWindow = new CVsHRWindow();
            cvsWindow.Show();
            CloseHRPopup();
        }
        private void OnInternalRulesButtonClick(object sender, RoutedEventArgs e)
        {
            InternalRulesHRWindow internalRulesWindow = new InternalRulesHRWindow();
            internalRulesWindow.Show();
            CloseHRPopup();
        }
        private void OnCertificatesButtonClick(object sender, RoutedEventArgs e)
        {
            CertificatesHRWindow certificatesWindow = new CertificatesHRWindow();
            certificatesWindow.Show();
            CloseHRPopup();
        }
        private void OnHealthDocsButtonClick(object sender, RoutedEventArgs e)
        {
            HealthDocsHRWindow healthDocsWindow = new HealthDocsHRWindow();
            healthDocsWindow.Show();
            CloseHRPopup();
        }

        private void OnHRButtonClick(object sender, RoutedEventArgs e)
        {
            HRWindow hrWindow = new HRWindow();
            hrWindow.Show();
            CloseHRPopup();
        }
        private void OnAddCandidateButtonClick(object sender, RoutedEventArgs e)
        {
            AddCandidateHRWindow addCandidateWindow = new AddCandidateHRWindow();
            addCandidateWindow.Show();
            CloseHRPopup();
        }
        private void OnAddEventButtonClick(object sender, RoutedEventArgs e)
        {
            AddEventHRWindow addEventWindow = new AddEventHRWindow();
            addEventWindow.Show();
            CloseHRPopup();
        }

        private void OnAddJobOfferButtonClick(object sender, RoutedEventArgs e)
        {
            AddJobOfferHRWindow addJobOfferWindow = new AddJobOfferHRWindow();
            addJobOfferWindow.Show();
            CloseHRPopup();
        }

        private void OnAddSurveyButtonClick(object sender, RoutedEventArgs e)
        {
            AddSurveyHRWindow addSurveyWindow = new AddSurveyHRWindow();
            addSurveyWindow.Show();
            CloseHRPopup();
        }


        //Payments Features

        private void PaymentsToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            PaymentsPopup.IsOpen = true;
        }

        private void OnReceiptsButtonClick(object sender, RoutedEventArgs e)
        {
            ReceiptsWindow receiptsWindow = new ReceiptsWindow();
            receiptsWindow.Show();
            ClosePaymentsPopup();
        }

        private void OnPayrollsButtonClick(object sender, RoutedEventArgs e)
        {
            PayrollsWindow payrollsWindow = new PayrollsWindow();
            payrollsWindow.Show();
            ClosePaymentsPopup();
        }

        //Calendar Features

        private void CalendarToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = true;
        }

        private void OnCalendarButtonClick(object sender, RoutedEventArgs e)
        {
            CalendarWindow calendarWindow = new CalendarWindow();
            calendarWindow.Show();
            CloseCalendarPopup();
        }
        private void EventCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("SelectedDatesChanged event fired!");

            foreach (var date in e.AddedItems)
            {
                Debug.WriteLine($"Selected Date: {date}");
            }
        }

        //Analysis and Report Features

        private void AnalysisReportToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            AnalysisReportPopup.IsOpen = true;
        }

        private void OnAnalysisReportButtonClick(object sender, RoutedEventArgs e)
        {
            AnalysisReportWindow analysisReportWindow = new AnalysisReportWindow();
            analysisReportWindow.Show();
            CloseAnalysisReportPopup();
        }
        //Assessment

        private void AssessmentToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            AssessmentPopup.IsOpen = true;
        }

        private void OnAddAssessmentButtonClick(object sender, RoutedEventArgs e)
        {
            AddAssessmentWindow addAssessmentWindow = new AddAssessmentWindow();
            addAssessmentWindow.Show();
            CloseAssessmentPopup();
        }
        private void OnAssessmentListButtonClick(object sender, RoutedEventArgs e)
        {
            AssessmentListWindow assessmentListWindow = new AssessmentListWindow();
            assessmentListWindow.Show();
            CloseAssessmentPopup();
        }

        //Rewards and bonuses
        private void OnAddRewardButtonClick(object sender, RoutedEventArgs e)
        {
            AddRewardWindow addRewardWindow = new AddRewardWindow();
            addRewardWindow.Show();
            CloseEmployeesPopup();
        }

        private void OnAddBonusButtonClick(object sender, RoutedEventArgs e)
        {
            AddBonusWindow addBonusWindow = new AddBonusWindow();
            addBonusWindow.Show();
            CloseEmployeesPopup();
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

        // Close specific popups

        private void CloseVehiclesPopup()
        {
            VehiclesPopup.IsOpen = false;
        }

        private void CloseReferencesPopup()
        {
            ReferencesPopup.IsOpen = false;
        }

        private void CloseContractsPopup()
        {
            ContractsPopup.IsOpen = false;
        }

        private void CloseDepartmentsPopup()
        {
            DepartmentsPopup.IsOpen = false;
        }

        private void CloseHRPopup()
        {
            HRPopup.IsOpen = false;
        }

        private void ClosePaymentsPopup()
        {
            PaymentsPopup.IsOpen = false;
        }

        private void CloseCalendarPopup()
        {
            CalendarPopup.IsOpen = false;
        }

        private void CloseAnalysisReportPopup()
        {
            AnalysisReportPopup.IsOpen = false;
        }

        private void CloseAssessmentPopup()
        {
            AssessmentPopup.IsOpen = false;
        }

        
    }
}