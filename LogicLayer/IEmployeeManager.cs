using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    internal interface IEmployeeManager
    {
        EmployeeVM LoginEmployee(string email, string password);
        string HashSha256(string source);
        bool AuthenticateEmployee(string email, string passwordHash);
        EmployeeVM GetEmployeeByEmail(string email);
        List<string> GetRolesByEmployeeID(int employeeID);
        bool ResetPassword(string email, string oldPassword, string newPassword);
    }
}
