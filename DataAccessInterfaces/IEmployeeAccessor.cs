﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IEmployeeAccessor
    {
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        EmployeeVM SelectEmployeeByEmail(string email);
        EmployeeVM SelectEmployeeByEmployeeID(int EmployeeID);
        List<string> SelectRolesByEmployeeID(int employeeID);
        int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);
        List<Employee> SelectAllEmployees();

    }
}
