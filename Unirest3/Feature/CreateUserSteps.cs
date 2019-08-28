using TechTalk.SpecFlow;
using unirest_net.http;
using NUnit.Framework;
using Unirest3.Utility;
using Newtonsoft.Json.Linq;

namespace Unirest3.Feature
{
    [Binding]
    public class CreateUserSteps
    {
        JSonConverter jsonRead = new JSonConverter();
        Excel readExcelReader = new Excel();
        string response;
        string responseStatus;
        ReadExcel filePath = new ReadExcel();
        ExtentReporting extentReporting = new ExtentReporting("Unirest_CREATE_Request_TestReport", "APITestResultsDoc");
        [OneTimeSetUp]


        [Given(@"I have the API url to create a user")]
        public string GivenIHaveTheAPIUrlToCreateAUser()
        {
            return readExcelReader.readExcel(filePath.filePathToExcel(), 1, 1, 3);
        }

        [When(@"I call the API with post parameters for user creation")]
        public void WhenICallTheAPIWithPostParametersForUserCreation()
        {
            HttpResponse<string> jsonResponseCreateUser = Unirest.post(GivenIHaveTheAPIUrlToCreateAUser()).header("Content-Type", "application/json").body(jsonRead.JSonConvertCreate()).asJson<string>();

            response = jsonResponseCreateUser.Body.ToString();
            responseStatus = jsonResponseCreateUser.Code.ToString();
        }

        [Then(@"User details are created")]
        public void ThenUserDetailsAreCreated()
        {
            extentReporting.createTest("CREATE_Request_Test");
            dynamic results = JObject.Parse(response);

            try
            {
                if (results != null)
                {
                    // If the created details are returned
                    Assert.AreEqual((string)results.name, readExcelReader.readExcel(filePath.filePathToExcel(), 4, 2, 2));
                    Assert.AreEqual((string)results.job, readExcelReader.readExcel(filePath.filePathToExcel(), 4, 2, 3));
                   
                    //Verify the response code
                    Assert.AreEqual(responseStatus, readExcelReader.readExcel(filePath.filePathToExcel(), 4, 3, 2));
                }
                else
                {
                    // If the created detals are not returned
                    Assert.AreEqual(responseStatus, readExcelReader.readExcel(filePath.filePathToExcel(), 4, 3, 2));
                }

                extentReporting.testStatusWithMsg("Pass", "CREATE_Request_TestPassed");

            }
            catch (AssertionException e)
            {
                extentReporting.logReportStatement(AventStack.ExtentReports.Status.Error, e.Message);
                extentReporting.testStatusWithMsg("Fail", "CREATE_Request_TestFailed");
            }

            extentReporting.flushReport();

        }
    }
}
