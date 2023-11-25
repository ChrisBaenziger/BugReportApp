using DataAccessInterfaces;
using DataAccessFakes;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class EmployeeManager : IEmployeeManager
    {
        IEmployeeAccessor _employeeAccessor = null;

        public EmployeeManager()
        {
            _employeeAccessor = new EmployeeAccessor();
        }

        public EmployeeManager(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        }
        public bool AuthenticateEmployee(string email, string passwordHash)
        {
            bool result = false;

            try
            {
                result = 1 == _employeeAccessor.AuthenticateUserWithEmailAndPasswordHash(email, passwordHash);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Authentication Failed.", ex);
            }
            return result;
        }

        public EmployeeVM GetEmployeeByEmail(string email)
        {
            EmployeeVM employeeVM = null;

            try
            {
                employeeVM = _employeeAccessor.SelectEmployeeByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not found.", ex);
            }
            return employeeVM;
        }

        public EmployeeVM GetEmployeeByEmployeeID(int employeeID)
        {
            EmployeeVM employeeVM = null;

            try
            {
                employeeVM = _employeeAccessor.SelectEmployeeByEmployeeID(employeeID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not found.", ex);
            }
            return employeeVM;
        }

        public List<string> GetRolesByEmployeeID(int employeeID)
        {
            List<string> roles = null;

            try
            {
                roles = _employeeAccessor.SelectRolesByEmployeeID(employeeID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Roles not found.", ex);
            }

            return roles;
        }

        public string HashSha256(string source)
        {
            string hash = null;

            byte[] data;

            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            hash = s.ToString();
            return hash;
        }

        public EmployeeVM LoginEmployee(string email, string password)
        {
            EmployeeVM employeeVM = null;

            try
            {
                string passwordHash = HashSha256(password);
                if (AuthenticateEmployee(email, passwordHash))
                {
                    employeeVM = _employeeAccessor.SelectEmployeeByEmail(email);
                    employeeVM.Roles = _employeeAccessor.SelectRolesByEmployeeID(employeeVM.EmployeeID);
                }
                else
                {
                    throw new ApplicationException("Login failed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Authentication Failed.", ex);
            }
            return employeeVM;
        }

        public bool ResetPassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            oldPassword = HashSha256(oldPassword);
            newPassword = HashSha256(newPassword);

            try
            {
                result = (1 == _employeeAccessor.UpdatePasswordHash(email, oldPassword, newPassword));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }
            return result;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = null;

            try
            {
                employees = _employeeAccessor.SelectAllEmployees();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Employees not found.", ex);
            }

            return employees;
        }
    }
}
