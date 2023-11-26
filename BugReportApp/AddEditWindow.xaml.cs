using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BugReportApp
{
    /// <summary>
    /// Interaction logic for AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        private BugTicketVM _bugTicketVM = null;
        private int _bugTicketID = 0;
        private BugReportManager _bugReportManager = null;

        public AddEditWindow(int bugTicketID)
        {
            _bugTicketID = bugTicketID;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _bugReportManager = new BugReportManager();
                var employeeManager = new EmployeeManager();
                List<Employee> employees = employeeManager.GetAllEmployees();
                List<string> employeeNames = new List<string>();

                foreach (var employee in employees)
                {
                    employeeNames.Add(employee.EmployeeID + ": " +
                        employee.GivenName + employee.FamilyName);
                }

                cboBugTicketVersionNumber.ItemsSource = _bugReportManager.GetAllVersions();
                cboBugTicketAreaName.ItemsSource = _bugReportManager.GetAllAreas();
                cboBugTicketFeature.ItemsSource = _bugReportManager.GetAllFeatures();
                cboBugTicketStatus.ItemsSource = _bugReportManager.GetAllStatus();
                cboBugTicketAssignedTo.ItemsSource = employeeNames;

                UpdateBugReportDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
            }
        }

        private void UpdateBugReportDisplay()
        {
            try
            {
                _bugTicketVM = _bugReportManager.GetBugTicket(_bugTicketID);

                txtBugTicketID.Text = _bugTicketVM.BugTicketID.ToString();
                txtBugTicketDate.Text = _bugTicketVM.BugDate.ToString();
                txtBugTicketSubmitID.Text = _bugTicketVM.SubmitID.ToString();
                txtBugTicketLWDate.Text = _bugTicketVM.LastWorkedDate.ToString();
                txtBugTicketLWEmployee.Text = _bugTicketVM.LastWorkedEmployee.ToString()
                    + ": " + _bugTicketVM.LastWorkedName;
                txtDescription.Text = _bugTicketVM.Description;
                cboBugTicketAreaName.Text = _bugTicketVM.AreaName;
                cboBugTicketAssignedTo.Text = _bugTicketVM.AssignedTo.ToString()
                    + ": " + _bugTicketVM.AssignedToName;
                cboBugTicketFeature.Text = _bugTicketVM.Feature;
                cboBugTicketStatus.Text = _bugTicketVM.Status;
                cboBugTicketVersionNumber.Text = _bugTicketVM.VersionNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (_bugReportManager != null)
            {
                _bugTicketID--;
                if (_bugTicketID < 0)
                {
                    List<BugTicketVM> bugTickets = _bugReportManager.GetAllBugTickets();
                    _bugTicketID = bugTickets[bugTickets.Count - 1].BugTicketID;
                }
                UpdateBugReportDisplay();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_bugReportManager != null)
            {
                _bugTicketID++;
                if (_bugTicketID > _bugReportManager.GetAllBugTickets().Count - 1)
                {
                    List<BugTicketVM> bugTickets = _bugReportManager.GetAllBugTickets();
                    _bugTicketID = bugTickets[0].BugTicketID;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Close window?",
                "Are you sure you want to cancel?", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result.Equals(1))
            {
                this.Close();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
