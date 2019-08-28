using unirest_net.http;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Unirest3.Utility;
using Newtonsoft.Json.Linq;

namespace Unirest3.Feature
{
    [Binding]
    public class GetUserSteps
    {
        Excel readExcelReader = new Excel();
        string response;
        string responseStatus;
        ReadExcel filePath = new ReadExcel();
        ExtentReporting extentReporting = new ExtentReporting("Unirest_GET_Request_TestReport", "APITestResultsDoc");
        [OneTimeSetUp]


        [Given(@"I have the API url to get users details")]
        public string GivenIHaveTheAPIUrlToGetUsersDetails()
        {
            return readExcelReader.readExcel(filePath.filePathToExcel(), 1, 1, 2);
        }

        [When(@"I call the API url")]
        public void WhenICallTheAPIUrl()
        {
            HttpResponse<string> jsonResponse = Unirest.get(GivenIHaveTheAPIUrlToGetUsersDetails()).asString();
            response = jsonResponse.Body.ToString();
            responseStatus = jsonResponse.Code.ToString();

        }

        [Then(@"I get the users details")]
        public void ThenIGetTheUserDetails()
        {
            extentReporting.createTest("GET_Request_Test");
            dynamic results = JObject.Parse(response);

            try
            {
                //For the 1st object
                Assert.AreEqual((string)results.data[0].id, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 2, 2));
                Assert.AreEqual((string)results.data[0].first_name, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 2, 3));
                Assert.AreEqual((string)results.data[0].last_name, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 2, 4));
                Assert.AreEqual((string)results.data[0].avatar, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 2, 5));

                //For the 2nd object
                Assert.AreEqual((string)results.data[1].id, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 3, 2));
                Assert.AreEqual((string)results.data[1].first_name, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 3, 3));
                Assert.AreEqual((string)results.data[1].last_name, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 3, 4));
                Assert.AreEqual((string)results.data[1].avatar, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 3, 5));

                //For the 3rd object
                Assert.AreEqual((string)results.data[2].id, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 4, 2));
                Assert.AreEqual((string)results.data[2].first_name, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 4, 3));
                Assert.AreEqual((string)results.data[2].last_name, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 4, 4));
                Assert.AreEqual((string)results.data[2].avatar, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 4, 5));

                //Verify the response code
                Assert.AreEqual(responseStatus, readExcelReader.readExcel(filePath.filePathToExcel(), 2, 5, 2));

                extentReporting.testStatusWithMsg("Pass", "GET_Request_TestPassed");
            }
            catch (AssertionException e)
            {
                extentReporting.logReportStatement(AventStack.ExtentReports.Status.Error, e.Message);
                extentReporting.testStatusWithMsg("Fail", "GET_Request_TestFailed");
            }

            extentReporting.flushReport();

        }
    }
}
