using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessFakes;
using LogicLayer;


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


    }
}
