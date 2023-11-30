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
        private int viewAddEditValue = 0;
        private EmployeeVM _loggedInEmployee = null;
        // 0 view, 1 edit, 2 add

        public AddEditWindow(EmployeeVM loggedInEmployee)
        {
            viewAddEditValue = 2;
            _loggedInEmployee = loggedInEmployee;
            _bugTicketVM = new BugTicketVM();
            InitializeComponent();
        }

        public AddEditWindow(int bugTicketID, EmployeeVM loggedInEmployee)
        {
            _bugTicketID = bugTicketID;
            _loggedInEmployee = loggedInEmployee;
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
                        employee.GivenName + " " + employee.FamilyName);
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
            switch (viewAddEditValue)
            {
                case 0: // view
                    txtBugTicketID.IsEnabled = false;
                    txtBugTicketDate.IsEnabled = false;
                    txtBugTicketSubmitID.IsEnabled = false;
                    cboBugTicketAssignedTo.IsEnabled = false;
                    txtBugTicketLWDate.IsEnabled = false;
                    txtBugTicketLWEmployee.IsEnabled = false;
                    cboBugTicketVersionNumber.IsEnabled = false;
                    cboBugTicketStatus.IsEnabled = false;
                    cboBugTicketAreaName.IsEnabled = false;
                    cboBugTicketFeature.IsEnabled = false;
                    txtDescription.IsEnabled = false;

                    btnNext.Visibility = Visibility.Visible;
                    btnNext.IsEnabled = true;
                    btnPrevious.Visibility = Visibility.Visible;
                    btnPrevious.IsEnabled = true;

                    btnSubmit.Content = "Edit";
                    break;
                case 1: // edit
                    txtBugTicketID.IsEnabled = false;
                    txtBugTicketDate.IsEnabled = true;
                    txtBugTicketSubmitID.IsEnabled = true;
                    cboBugTicketAssignedTo.IsEnabled = true;
                    txtBugTicketLWDate.IsEnabled = false;
                    txtBugTicketLWEmployee.IsEnabled = false;
                    cboBugTicketVersionNumber.IsEnabled = true;
                    cboBugTicketStatus.IsEnabled = true;
                    cboBugTicketAreaName.IsEnabled = true;
                    cboBugTicketFeature.IsEnabled = true;
                    txtDescription.IsEnabled = true;

                    btnNext.Visibility = Visibility.Hidden;
                    btnNext.IsEnabled = false;
                    btnPrevious.Visibility = Visibility.Hidden;
                    btnPrevious.IsEnabled = false;

                    btnSubmit.Content = "Submit";
                    break;
                case 2: // add
                    txtBugTicketID.IsEnabled = false;
                    txtBugTicketDate.IsEnabled = false;
                    txtBugTicketSubmitID.IsEnabled = false;
                    cboBugTicketAssignedTo.IsEnabled = true;
                    txtBugTicketLWDate.IsEnabled = false;
                    txtBugTicketLWEmployee.IsEnabled = false;
                    cboBugTicketVersionNumber.IsEnabled = true;
                    cboBugTicketStatus.IsEnabled = true;
                    cboBugTicketAreaName.IsEnabled = true;
                    cboBugTicketFeature.IsEnabled = true;
                    txtDescription.IsEnabled = true;

                    btnNext.Visibility = Visibility.Hidden;
                    btnNext.IsEnabled = false;
                    btnPrevious.Visibility = Visibility.Hidden;
                    btnPrevious.IsEnabled = false;

                    btnSubmit.Content = "Submit";
                    break;
                default:
                    break;
            }

            if (viewAddEditValue == 2)
            {

            }
            else
            {
                try
                {
                    _bugTicketVM = _bugReportManager.GetBugTicket(_bugTicketID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }

                try
                {
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

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (_bugReportManager != null)
            {
                List<BugTicketVM> bugTickets = _bugReportManager.GetAllBugTickets();
                _bugTicketID--;
                if (_bugTicketID < bugTickets[0].BugTicketID)
                {
                    _bugTicketID = bugTickets[bugTickets.Count - 1].BugTicketID;
                }
                UpdateBugReportDisplay();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_bugReportManager != null)
            {
                List<BugTicketVM> bugTickets = _bugReportManager.GetAllBugTickets();
                _bugTicketID++;
                if (_bugTicketID > bugTickets[bugTickets.Count - 1].BugTicketID)
                {
                    _bugTicketID = bugTickets[0].BugTicketID;
                }
                UpdateBugReportDisplay();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Close window?",
                "Are you sure you want to cancel?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if ((int)result == 6)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            switch (viewAddEditValue)
            {
                case 0: // switch to edit
                    viewAddEditValue = 1;
                    UpdateBugReportDisplay();
                    break;
                case 1: // submit edits

                    break;
                case 2: // submit new ticket
                    bool result = false;
                    try
                    {
                        result = _bugReportManager.AddBugReport(
                        new BugTicket()
                        {
                            BugDate = DateTime.Now,
                            SubmitID = _loggedInEmployee.EmployeeID,
                            LastWorkedDate = DateTime.Now,
                            LastWorkedEmployee = _loggedInEmployee.EmployeeID,
                            Description = txtDescription.Text,
                            AreaName = cboBugTicketAreaName.Text,
                            AssignedTo = int.Parse(cboBugTicketAssignedTo.Text.Remove(cboBugTicketAssignedTo.Text.IndexOf(':'))),
                            Feature = cboBugTicketFeature.Text,
                            Status = cboBugTicketStatus.Text,
                            VersionNumber = cboBugTicketVersionNumber.Text
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem adding the ticket.Pre\n" + ex.Message, "Add Ticket Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (result == true)
                    {
                        MessageBox.Show("Ticket was added to the database.", "Ticket Added",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
