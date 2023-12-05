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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BugReportApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // !!! after returning from another window refresh the item source !!!

        EmployeeManager _employeeManager = null;
        EmployeeVM _loggedInEmployee = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void hideAllTabs()
        {
            foreach (var tab in tabsetMain.Items)
            {
                ((TabItem)tab).Visibility = Visibility.Collapsed;
                tabContainer.Visibility = Visibility.Collapsed;
            }
        } // end hideAllTabls

        private void showTabsForRoles()
        {
            foreach (var role in _loggedInEmployee.Roles)
            {
                switch (role)
                {
                    case "Admin":
                        tabAdmin.Visibility = Visibility.Visible;
                        break;
                    case "Manager":
                        tabManager.Visibility = Visibility.Visible;
                        tabProgrammer.Visibility = Visibility.Visible;
                        tabSrProgrammer.Visibility = Visibility.Visible;
                        tabProjectLead.Visibility = Visibility.Visible;
                        tabSettings.Visibility = Visibility.Visible;
                        break;
                    case "Programmer":
                        tabProgrammer.Visibility = Visibility.Visible;
                        break;
                    case "SeniorProgrammer":
                        tabSrProgrammer.Visibility = Visibility.Visible;
                        break;
                    case "ProjectLead":
                        tabProjectLead.Visibility = Visibility.Visible;
                        tabSettings.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
            tabContainer.Visibility = Visibility.Visible;
        } // end showTabsForRoles

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content.ToString() == "Log In")
            {
                string email = txtEmailAddress.Text;
                string password = pwdPassword.Password;

                if (email.IsValidEmail())
                {
                    MessageBox.Show("Invalid Email", "Input Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    txtEmailAddress.SelectAll();
                    txtEmailAddress.Focus();
                    return;
                }

                if (password.IsValidPassword())
                {
                    MessageBox.Show("Invalid Password", "Input Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    pwdPassword.SelectAll();
                    pwdPassword.Focus();
                    return;
                }

                try
                {
                    _loggedInEmployee = _employeeManager.LoginEmployee(email, password);
                    if (pwdPassword.Password.ToString() == "newuser")
                    {
                        PasswordUpdateWindow passwordUpdate = new PasswordUpdateWindow(_loggedInEmployee.Email);

                        var result = passwordUpdate.ShowDialog();

                        if (result == true)
                        {
                            MessageBox.Show("Password Updated.", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Update Cancelled. Logging Out.", "Password not changed.",
                                MessageBoxButton.OK, MessageBoxImage.Hand);
                            txtEmailAddress.Text = "";
                            pwdPassword.Password = "";
                            updateUIForLogout();
                            return;
                        }
                    }
                    updateUIForEmployee();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                updateUIForLogout();
            }
        } // end btnLgoin_Click

        private void updateUIForLogout()
        {
            _loggedInEmployee = null;
            lblGreeting.Content = "You are not logged in.";
            lblGreeting.Foreground = Brushes.Red;

            statMessage.Content = "Welcome. Please log in to continue.";

            txtEmailAddress.Visibility = Visibility.Visible;
            lblEmailAddress.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;

            btnLogin.Content = "Log In";
            btnLogin.IsDefault = true;
            hideAllTabs();
            txtEmailAddress.Focus();
        } // end updateUIForLogout

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _employeeManager = new EmployeeManager();
            hideAllTabs();
            txtEmailAddress.Focus();
            btnLogin.IsDefault = true;
        } // end Window_Loaded

        private void updateUIForEmployee()
        {
            string rolesList = "";

            for (int i = 0; i < _loggedInEmployee.Roles.Count; i++)
            {
                rolesList += " " + _loggedInEmployee.Roles[i];
                if (i == _loggedInEmployee.Roles.Count - 2)
                {
                    if (_loggedInEmployee.Roles.Count > 2)
                    {
                        rolesList += ",";
                    }
                    rolesList += " and";
                }
                else if (i < _loggedInEmployee.Roles.Count - 2)
                {
                    rolesList += ",";
                }
            }
            lblGreeting.Content = "Welcome, " + _loggedInEmployee.GivenName +
                ". You are logged in as: \n" + rolesList + ".";
            lblGreeting.Foreground = Brushes.Blue;

            statMessage.Content = "Logged in on " + DateTime.Now.ToLongDateString() +
                " at " + DateTime.Now.ToShortTimeString() +
                ". Please remember to log out before leaving your workstation.";

            // clear the login section
            txtEmailAddress.Text = "";
            txtEmailAddress.Visibility = Visibility.Hidden;
            lblEmailAddress.Visibility = Visibility.Hidden;
            pwdPassword.Password = "";
            pwdPassword.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;

            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;

            showTabsForRoles();
        } // end updateUIForEmployee

        private void mnuChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (_loggedInEmployee == null)
            {
                MessageBox.Show("You must be logged in to update your password",
                    "Login Required", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                try
                {
                    PasswordUpdateWindow passwordUpdate =
                        new PasswordUpdateWindow(_loggedInEmployee.Email);

                    var result = passwordUpdate.ShowDialog();

                    if (result == true)
                    {
                        MessageBox.Show("Password Updated.", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Update Aborted.", "Password not changed.",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Update Aborted.", "Password not changed.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    throw;
                }
            }
        } // end mnuChangePassword_Click

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bug Reporting App\nBy: Chris Baenziger\n\nFinal project for .NET II\n" +
                "Kirkwood Community College", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void tabProgrammer_GotFocus(object sender, RoutedEventArgs e)
        {
            updateProgrammerTicketList();
        }

        private void updateProgrammerTicketList()
        {
            if (datProgrammerBugList.ItemsSource == null)
            {
                var bugReportManager = new BugReportManager();

                try
                {
                    datProgrammerBugList.ItemsSource = bugReportManager.GetAllBugTickets();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }
            }
        }

        private void datProgrammerBugList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var bugTicket = datProgrammerBugList.SelectedItem as BugTicketVM;
            if (bugTicket != null)
            {
                var detailWindow = new AddEditWindow(bugTicket.BugTicketID, _loggedInEmployee);
                detailWindow.ShowDialog();
            }
            //else
            //{
            //    MessageBox.Show("Please select a bug report from the list.", "No report selected.", MessageBoxButton.OK);
            //}
        }

        private void tabSrProgrammer_GotFocus(object sender, RoutedEventArgs e)
        {
            updateSrProgrammerTicketList();
        }

        private void updateSrProgrammerTicketList()
        {
            if (datSrProgrammerBugList.ItemsSource == null)
            {
                var bugReportManager = new BugReportManager();
                try
                {
                    datSrProgrammerBugList.ItemsSource = bugReportManager.GetAllBugTickets();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }
            }
        }

        private void datSrProgrammerBugList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datSrProgrammerBugList.SelectedIndex > 0 /* .SelectedItem != null*/)
            {
                var bugTicket = datSrProgrammerBugList.SelectedItem as BugTicketVM;

                var detailWindow = new AddEditWindow(bugTicket.BugTicketID, _loggedInEmployee);
                detailWindow.ShowDialog();
            }
            else
            {

            }
        }

        private void btnProgAddBugReport_Click(object sender, RoutedEventArgs e)
        {
            AddBugReport(_loggedInEmployee);
        }

        private void btnSrProgAddBugReport_Click(object sender, RoutedEventArgs e)
        {
            AddBugReport(_loggedInEmployee);
        }

        private void AddBugReport(EmployeeVM loggedInEmployee)
        {
            var detailWindow = new AddEditWindow(loggedInEmployee);
            detailWindow.ShowDialog();

            datProgrammerBugList.ItemsSource = null;
            datSrProgrammerBugList.ItemsSource = null;

            updateProgrammerTicketList();
            updateSrProgrammerTicketList();
        }
    } // end main window class
}
