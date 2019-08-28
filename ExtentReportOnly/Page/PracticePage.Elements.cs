using ExtentReportOnly.Controls;
using OpenQA.Selenium;

namespace ExtentReportOnly.Page
{
    public partial class PracticePage
    {
        public Textbox SearchBox => new Textbox(WrappedDriver.FindElement(By.Id("search_query_top")));
        public Textbox UserName_Txt => new Textbox(WrappedDriver.FindElement(By.Id("email")));
        public Textbox Password_Txt => new Textbox(WrappedDriver.FindElement(By.Id("passwd")));
        public Button SignIn_Btn => new Button(WrappedDriver.FindElement(By.Id("SubmitLogin")));
        public Button SignInPage => new Button(WrappedDriver.FindElement(By.XPath("//*[@id='header']/div[2]/div/div/nav/div[1]/a")));

    }
}
