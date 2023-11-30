using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessFakes;
using LogicLayer;
using System.Collections.Generic;
using DataObjects;

namespace UnitTestProject
{
    [TestClass]
    public class BugReportManagerTests
    {
        private BugReportManager _bugReportManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _bugReportManager = new BugReportManager(new BugReportAccessorFake(), new EmployeeAccessorFake());
        }

        [TestMethod]
        public void TestGetBugTicketByBugTicketIDReturnsCorrectBugTicket()
        {
            int expectedBugTicketID = 2;
            int actualBugTicketID = 0;

            actualBugTicketID = _bugReportManager.GetBugTicket(2).BugTicketID;

            Assert.AreEqual(expectedBugTicketID, actualBugTicketID);
        }

        [TestMethod]
        public void TestGetAllBugTicketsReturnsCorrectList()
        {
            int expectedCount = 3;
            int actualCount = 0;

            actualCount = _bugReportManager.GetAllBugTickets().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetBugTicketsByVersionReturnsCorrectList()
        {
            int expectedCount = 2;
            int actualCount = 0;

            actualCount = _bugReportManager.GetBugTicketsByVersion("0.1.2").Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetBugTicketsByAreaReturnsCorrectList()
        {
            int expectedCount = 1;
            int actualCount = 0;

            actualCount = _bugReportManager.GetBugTicketsByArea("Core").Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetBugTicketsByStatusReturnsCorrectList()
        {
            int expectedCount = 2;
            int actualCount = 0;

            actualCount = _bugReportManager.GetBugTicketsByStatus("Testing").Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetBugTicketsByFeatureReturnsCorrectList()
        {
            int expectedCount = 1;
            int actualCount = 0;

            actualCount = _bugReportManager.GetBugTicketsByFeature("Test fake 2").Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetBugTicketsByAssignedToReturnsCorrectList()
        {
            int expectedCount = 2;
            int actualCount = 0;

            actualCount = _bugReportManager.GetBugTicketsByAssignedTo(1).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetAllVersionsReturnsCorrectList()
        {
            int expectedCount = 2;
            int actualCount = 0;

            actualCount = _bugReportManager.GetAllVersions().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetAllAreasReturnsCorrectList()
        {
            int expectedCount = 4;
            int actualCount = 0;

            actualCount = _bugReportManager.GetAllAreas().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetAllStatusReturnsCorrectList()
        {
            int expectedCount = 3;
            int actualCount = 0;

            actualCount = _bugReportManager.GetAllStatus().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestGetAllFeaturesReturnsCorrectList()
        {
            int expectedCount = 4;
            int actualCount = 0;

            actualCount = _bugReportManager.GetAllFeatures().Count;

            Assert.AreEqual(expectedCount, actualCount);

        }

        [TestMethod]
        public void TestUpdateBugReportUpdatesBugReport()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _bugReportManager.UpdateBugReport(
                _bugReportManager.GetBugTicket(1),
                new DataObjects.BugTicket()
                {
                    BugTicketID = 1,
                    BugDate = DateTime.Now,
                    SubmitID = 1005,
                    VersionNumber = "0.1.2",
                    AreaName = "Core",
                    Description = "Update bug report test",
                    Status = "Testing",
                    Feature = "Test update bug report",
                    AssignedTo = 2,
                    LastWorkedDate = DateTime.Now,
                    LastWorkedEmployee = 2,
                    Active = true
                });

            Assert.AreEqual(expectedResult, actualResult);  
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateBugReportReturnsError()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _bugReportManager.UpdateBugReport(
                new DataObjects.BugTicket()
                {
                    BugTicketID =6,
                    BugDate = DateTime.Now,
                    SubmitID = 1000,
                    VersionNumber = "0.1.2",
                    AreaName = "Core",
                    Description = "Add bug report test",
                    Status = "Testing",
                    Feature = "Test add bug report",
                    AssignedTo = 1,
                    LastWorkedDate = DateTime.Now,
                    LastWorkedEmployee = 1,
                    Active = true
                },
                new DataObjects.BugTicket(){
                    BugTicketID =2,
                    BugDate = DateTime.Now,
                    SubmitID = 1005,
                    VersionNumber = "0.1.2",
                    AreaName = "Core",
                    Description = "Update bug report test",
                    Status = "Testing",
                    Feature = "Test update bug report",
                    AssignedTo = 1,
                    LastWorkedDate = DateTime.Now,
                    LastWorkedEmployee = 1,
                    Active = true
                });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAddBugReportCreatesNewBugReport()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _bugReportManager.AddBugReport(
                new DataObjects.BugTicket(){
                    BugTicketID = 5,
                    BugDate = DateTime.Now,
                    SubmitID = 1000,
                    VersionNumber = "0.1.2",
                    AreaName = "Core",
                    Description = "Add bug report test",
                    Status = "Testing",
                    Feature = "Test add bug report",
                    AssignedTo = 1,
                    LastWorkedDate = DateTime.Now,
                    LastWorkedEmployee = 1,
                    Active = true
                });

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddBugReportReturnsError()
        {
            bool expectedResult = true;
            bool actualResult = false;

            actualResult = _bugReportManager.AddBugReport(
                new DataObjects.BugTicket()
                {
                    BugTicketID = 4,
                    BugDate = DateTime.Now,
                    SubmitID = 1000,
                    VersionNumber = "0.1.2",
                    AreaName = "Core",
                    Description = "Add bug report test",
                    Status = "Testing",
                    Feature = "Test add bug report",
                    AssignedTo = 1,
                    LastWorkedDate = DateTime.Now,
                    LastWorkedEmployee = 1,
                    Active = true
                }
                );

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetStatisticsReturnsCorrectData()
        {
            string expectedResult = "Value";
            string actualResult = "";

            actualResult = _bugReportManager.GetStatistics()[0].Value;

            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
