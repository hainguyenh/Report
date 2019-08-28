using ExtentReportWithKlov.BaseClass;
using ExtentReportWithKlov.Page;
using NUnit.Framework;

namespace ExtentReportWithKlov.Script
{
    [TestFixture]
    public class Test : TestFixture
    {
        [Test]
        public void TestExtentReportWithKlov()
        {
            PracticePage.Instance.GoToLogInPage();
            PracticePage.Instance.InputUserAccount("truyentranhtuan@gmail.com", "123456");

            System.Threading.Thread.Sleep(1000);
            PracticePage.Instance.SignIn_Btn.Click();

            // Verify 
            Assert.AreEqual("My account - My Store", PracticePage.Instance.GetTitle);

            // Search value "VN-2" then Enter
            PracticePage.Instance.SearchingValue("T-Shirts");

            // Verify 
            Assert.AreEqual("Search - My Store", PracticePage.Instance.GetTitle);
            
            PracticePage.Instance.LogOut();
        }
    }
}
