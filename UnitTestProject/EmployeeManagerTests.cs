using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicLayer;
using DataAccessFakes;
using DataAccessInterfaces;
using DataObjects;

namespace UnitTestProject
{
    [TestClass]
    public class EmployeeManagerTests
    {
        EmployeeManager _employeeManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _employeeManager = new EmployeeManager(new EmployeeAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsAValidHash()
        {
            string testString = "newuser";
            string expectedResult = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            string actualResult = null;

            actualResult = _employeeManager.HashSha256(testString);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAuthenticateEmployeePassesWithCorretEmailAndPassword()
        {
            string email = "one@company.com";
            string passwordHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            bool expectedResult = true;
            bool acutalResult = false;

            acutalResult = _employeeManager.AuthenticateEmployee(email, passwordHash);

            Assert.AreEqual(expectedResult, acutalResult);
        }

        [TestMethod]
        public void TestAuthenticateEmplyeePassesWithBadEmailAndPassword()
        {
            string email = "tne@company.com";
            string passwordHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            bool expectedResult = false;
            bool acutalResult = true;

            acutalResult = _employeeManager.AuthenticateEmployee(email, passwordHash);

            Assert.AreEqual(expectedResult, acutalResult);
        }

        [TestMethod]
        public void TestGetEmployeeByEmailReturnsCorrectEmployee()
        {
            string email = "one@company.com";
            int expectedID = 1;
            int actualID = 0;

            actualID = _employeeManager.GetEmployeeByEmail(email).EmployeeID;

            Assert.AreEqual(expectedID, actualID);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetEmployeeByEmailFailsForBadEmailWithApplicationException()
        {
            string email = "tne@company.com";
            int actualID = 0;

            actualID = _employeeManager.GetEmployeeByEmail(email).EmployeeID;
        }

        [TestMethod]
        public void TestGetEmployeeByEmployeeIDReturnsCorrectEmployee()
        {
            int  employeeID = 1;
            string expectedName = "OneUser";
            string actualName = "";

            Employee employee = _employeeManager.GetEmployeeByEmployeeID(employeeID);
            actualName = employee.GivenName + employee.FamilyName;

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetEmployeeByEmployeeIDFailsForBadEmailWithApplicationException()
        {
            int employeeID = 10;
            string actualName = "";

            Employee employee = _employeeManager.GetEmployeeByEmployeeID(employeeID);
            actualName = employee.GivenName + employee.FamilyName;
        }

        [TestMethod]
        public void TestGetRolesByEmployeeIDReutnrsCorrectRoles()
        {
            int testID = 1;
            int expectedRoleCount = 2;
            int actualRoleCount = 0;

            actualRoleCount = _employeeManager.GetRolesByEmployeeID(testID).Count;

            Assert.AreEqual(expectedRoleCount, actualRoleCount);
        }

        [TestMethod]
        [ExpectedException (typeof(ApplicationException))]
        public void TestGetRolesForEmployeeWithoutRolesRetunrsError()
        {
            int testID = 2;

            _employeeManager.GetRolesByEmployeeID(testID);
        }

        [TestMethod]
        public void TestResetPasswordWorksCorrectly()
        {
            string email = "one@company.com";
            string password = "newuser";
            string newPassword = "password";
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _employeeManager.ResetPassword(email, password, newPassword);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestResetPasswordFailsWithBadPassword()
        {
            string email = "one@company.com";
            string password = "newloser";
            string newPassword = "password";
            bool actualResult = false;

            actualResult = _employeeManager.ResetPassword(email, password, newPassword);
        }

        [TestMethod]
        public void TestGetAllEmployeesReturnsCorrectList()
        {
            int expectedCount = 3;
            int actualCount = 0;

            actualCount = _employeeManager.GetAllEmployees().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

    }
}
