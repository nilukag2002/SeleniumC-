using TechTalk.SpecFlow;
using unirest_net.http;
using NUnit.Framework;
using Unirest3.Utility;

namespace Unirest3.Feature
{
    [Binding]
    public class DeleteUserSteps
    {
        JSonConverter jsonRead = new JSonConverter();
        Excel readExcelReader = new Excel();
        string response;
        string responseStatus;
        ReadExcel filePath = new ReadExcel();
        ExtentReporting extentReporting = new ExtentReporting("Unirest_DELETE_Request_TestReport", "APITestResultsDoc");
        [OneTimeSetUp]


        [Given(@"I have the API url to delete a user's details")]
        public string GivenIHaveTheAPIUrlToDeleteAUserSDetails()
        {
            return readExcelReader.readExcel(filePath.filePathToExcel(), 1, 1, 5);
        }
        
        [When(@"I call the API to delete the user's details")]
        public void WhenICallTheAPIToDeleteTheUserSDetails()
        {
            HttpResponse<string> jsonResponse = Unirest.delete(GivenIHaveTheAPIUrlToDeleteAUserSDetails()).asString();
            responseStatus = jsonResponse.Code.ToString();
        }
        
        [Then(@"User details are deleted")]
        public void ThenUserDetailsAreDeleted()
        {
            extentReporting.createTest("DELETE_Request_Test");

            try
            {
                Assert.AreEqual(responseStatus, readExcelReader.readExcel(filePath.filePathToExcel(), 7, 1, 2));

                extentReporting.testStatusWithMsg("Pass", "DELETE_Request_TestPassed");
            }
            catch (AssertionException e)
            {
                extentReporting.logReportStatement(AventStack.ExtentReports.Status.Error, e.Message);
                extentReporting.testStatusWithMsg("Fail", "DELETE_Request_TestFailed");
            }

            extentReporting.flushReport();

        }
    }
}
