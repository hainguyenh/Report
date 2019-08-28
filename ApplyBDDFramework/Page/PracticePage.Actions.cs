using ApplyBDDFramework.BaseClass;
using OpenQA.Selenium;

namespace ApplyBDDFramework.Page
{
    public partial class PracticePage : BaseWebPage<PracticePage>
    {
        public PracticePage()
        {
            // Navigate to PMA
            WrappedDriver.Navigate().GoToUrl("http://automationpractice.com/");
        }


        public void GoToLogInPage()
        {
            SignInPage.Click();
        }

        public void InputUserAccount(string userName, string password)
        {
            UserName_Txt.SendKeys(userName);
            Password_Txt.SendKeys(password);
        }

        public void SearchingValue(string value)
        {
            SearchBox.SendKeys(value);
            SearchBox.SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(1000);
        }

        public void LogOut()
        {
            // Log out
            WrappedDriver.FindElement(By.XPath("//*[@id='header']/div[2]/div/div/nav/div[2]/a")).Click();
        }
    }
}
