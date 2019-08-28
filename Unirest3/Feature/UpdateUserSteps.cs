using TechTalk.SpecFlow;
using unirest_net.http;
using NUnit.Framework;
using Unirest3.Utility;
using Newtonsoft.Json.Linq;

namespace Unirest3.Feature
{
    [Binding]
    public class UpdateUserSteps
    {
        JSonConverter jsonRead = new JSonConverter();
        Excel readExcelReader = new Excel();
        string response;
        string responseStatus;
        ReadExcel filePath = new ReadExcel();
        ExtentReporting extentReporting = new ExtentReporting("Unirest_UPDATE_Request_TestReport", "APITestResultsDoc");
        [OneTimeSetUp]


        [Given(@"I have the API url to update a user's details")]
        public string GivenIHaveTheAPIUrlToUpdateAUserSDetails()
        {
            return readExcelReader.readExcel(filePath.filePathToExcel(), 1, 1, 4);
        }
        
        [When(@"I call the API with update parameters for updating the user details")]
        public void WhenICallTheAPIWithUpdateParametersForUpdatingTheUserDetails()
        {
            HttpResponse<string> jsonResponseUpdateUser = Unirest.put(GivenIHaveTheAPIUrlToUpdateAUserSDetails()).header("Content-Type", "application/json").header("cache-control", "no-cache").body(jsonRead.JSonConvertUpdate()).asJson<string>();

            response = jsonResponseUpdateUser.Body.ToString();
            responseStatus = jsonResponseUpdateUser.Code.ToString();
        }
        
        [Then(@"User details are updated")]
        public void ThenUserDetailsAreUpdated()
        {
            extentReporting.createTest("UPDATE_Request_Test");
            dynamic results = JObject.Parse(response);

            try
            {
                if (results != null)
                {
                    // If the updated details are returned
                    Assert.AreEqual((string)results.name, readExcelReader.readExcel(filePath.filePathToExcel(), 6, 2, 2));
                    Assert.AreEqual((string)results.job, readExcelReader.readExcel(filePath.filePathToExcel(), 6, 2, 3));
                    
                    //Verify the response code
                    Assert.AreEqual(responseStatus, readExcelReader.readExcel(filePath.filePathToExcel(), 6, 3, 2));
                }
                else
                {
                    // If the updated detals are not returned
                    Assert.AreEqual(responseStatus, readExcelReader.readExcel(filePath.filePathToExcel(), 6, 3, 2));
                }

                extentReporting.testStatusWithMsg("Pass", "UPDATE_Request_TestPassed");
            }
            catch (AssertionException e)
            {
                extentReporting.logReportStatement(AventStack.ExtentReports.Status.Error, e.Message);
                extentReporting.testStatusWithMsg("Fail", "UPDATE_Request_TestFailed");
            }

            extentReporting.flushReport();

        }
    }
}
