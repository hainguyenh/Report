using ApplyBDDFramework.Page;
using ApplyBDDFramework.Utils;
using NUnit.Framework;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ApplyBDDFramework.Script.Scenario
{
    [Binding]
    public class TestSteps
    {
        ScenarioContext _scearioContext;

        public TestSteps(ScenarioContext scenarioContext)
        {
            this._scearioContext = scenarioContext;
        }

        [Given(@"I navigate to the log in page")]
        public void GivenINavigateToTheLogInPage()
        {
            PracticePage.Instance.GoToLogInPage();
        }

        [When(@"I input the Username and Password")]
        public void WhenIInputTheUsernameAndPassword(Table table)
        {
            var _user = table.CreateSet<User>();
            PracticePage.Instance.InputUserAccount(_user.FirstOrDefault().Username, _user.FirstOrDefault().Password);
            System.Threading.Thread.Sleep(1000);
        }


        [When(@"I click on the Sign in button")]
        public void WhenIClickOnTheSignInButton()
        {
            PracticePage.Instance.SignIn_Btn.Click();
        }


        [Then(@"The Page is signed successfully with Title ""(.*)""")]
        public void ThenThePageIsSignedSuccessfullyWithTitle(string p0)
        {
            Assert.AreEqual(p0, PracticePage.Instance.GetTitle);
            ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(), "The title of page is displayed as expected");
        }

        [When(@"I input ""(.*)"" to the textbox search")]
        public void WhenIInputToTheTextboxSearch(string p0)
        {
            // Search value "VN-2" then Enter
            PracticePage.Instance.SearchingValue(p0);
        }


        [Then(@"The Page is search with the value and the title is displayed ""(.*)""")]
        public void ThenThePageIsSearchWithTheValueAndTheTitleIsDisplayed(string p0)
        {
            // Verify 
            Assert.AreEqual(p0, PracticePage.Instance.GetTitle);
            ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(), "The title of page is displayed as expected");
        }

        [Then(@"I log out")]
        public void ThenILogOut()
        {
            PracticePage.Instance.LogOut();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "------------------------------");
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
