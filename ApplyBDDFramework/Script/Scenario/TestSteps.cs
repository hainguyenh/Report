using System;
using TechTalk.SpecFlow;

namespace ApplyBDDFramework.Script.Scenario
{
    [Binding]
    public class TestSteps
    {
        [Given(@"I navigate to the log in page")]
        public void GivenINavigateToTheLogInPage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I input the ""(.*)"" and ""(.*)""")]
        public void WhenIInputTheAnd(string p0, string p1, Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click on the Sign in button")]
        public void WhenIClickOnTheSignInButton()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I input ""(.*)"" to the textbox search")]
        public void WhenIInputToTheTextboxSearch(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The Page is signed successfully with Title ""(.*)""")]
        public void ThenThePageIsSignedSuccessfullyWithTitle(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The Page is search with the value and the title is displayed ""(.*)""")]
        public void ThenThePageIsSearchWithTheValueAndTheTitleIsDisplayed(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I log out")]
        public void ThenILogOut()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
