using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{

    public class EmployeeAccessorFake : IEmployeeAccessor
    {
        private List<EmployeeVM> fakeEmployees = new List<EmployeeVM>();
        private List<string> passwordHashes = new List<string>();

        public EmployeeAccessorFake()
        {
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 1,
                Email = "one@company.com",
                GivenName = "One",
                FamilyName = "User",
                PhoneNumber = "1234567890",
                Active = true,
                Roles = new List<string>()
            });

            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 2,
                Email = "two@company.com",
                GivenName = "Two",
                FamilyName = "User",
                PhoneNumber = "1234567890",
                Active = true,
                Roles = new List<string>()
            });

            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 3,
                Email = "three@company.com",
                GivenName = "Three",
                FamilyName = "User",
                PhoneNumber = "1234567890",
                Active = true,
                Roles = new List<string>()
            });
                         
            passwordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            passwordHashes.Add("bad hash");
            passwordHashes.Add("bad hash");

            fakeEmployees[0].Roles.Add("Programmer");
            fakeEmployees[0].Roles.Add("Manager");
        }
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int rows = 0;

            for (int i = 0; i < fakeEmployees.Count; i++)
            {
                if (fakeEmployees[i].Email == email)
                {
                    if (passwordHashes[i] == passwordHash && fakeEmployees[i].Active)
                    {
                        rows++;
                        continue;
                    }
                }
            }
            return rows;
        }

        public EmployeeVM SelectEmployeeByEmail(string email)
        {
            EmployeeVM employeeVM = null;

            foreach (var employee in fakeEmployees)
            {
                if (employee.Email == email)
                {
                    employeeVM = employee;
                    break;
                }
            }
            if (employeeVM == null)
            {
                throw new ArgumentException("Email address not found.");
            }
            return employeeVM;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            foreach (var employee in fakeEmployees)
            {
                if(employee.EmployeeID == employeeID)
                {
                    roles = employee.Roles;
                    break;
                }
            }
            if(roles.Count == 0)
            {
                throw new ArgumentException("No roles found.");
            }
            return roles;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rows = 0;

            for (int i = 0; i < fakeEmployees.Count; i++)
            {
                if (fakeEmployees[i].Email == email)
                {
                    if (passwordHashes[i] == oldPasswordHash)
                    {
                        passwordHashes[i] = newPasswordHash;
                        rows++;
                    }
                }

            }
            if(rows != 1)
            {
                throw new ArgumentException("Bad email or password.");
            }
            return rows;
        }

    }
}

