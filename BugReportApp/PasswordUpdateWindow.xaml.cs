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
    /// Interaction logic for PasswordUpdateWindow.xaml
    /// </summary>
    public partial class PasswordUpdateWindow : Window
    {
        string _email = null;

        public PasswordUpdateWindow(string email)
        {
            InitializeComponent();
            _email = email;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        } // end btnCancel_Click

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text != _email)
            {
                MessageBox.Show("Update Aborted.", "Password not changed.",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (pwdNewPassword.Password.ToString() == "newuser" ||
                pwdNewPassword.Password.ToString() == pwdOldPassword.Password.ToString())
            {
                MessageBox.Show("Must use a new password.", "Password not changed.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }
            if (pwdNewPassword.Password != pwdRetypePassword.Password)
            {
                MessageBox.Show("Passwords do not match.", "Password not changed.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }
            if (pwdNewPassword.Password.IsValidPassword())
            {
                MessageBox.Show("Password not complex enough.", "Password not changed.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            string oldPassword = pwdOldPassword.Password;
            string newPassword = pwdNewPassword.Password;

            EmployeeManager employeeManager = new EmployeeManager();

            try
            {
                if (employeeManager.ResetPassword(_email, oldPassword, newPassword) == true)
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reset password failed." + ex.Message + "\n\n" + ex.InnerException.Message, "Password not changed.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }


        } // end btnSubmit_Click

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsDefault = true;
            txtEmail.Focus();
        } // end Window_Loaded

    } // end PasswordUpdateWindow Class
}
