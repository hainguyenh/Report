using ApplyBDDFramework.BaseClass;
using ApplyBDDFramework.Page;
using NUnit.Framework;

namespace ApplyBDDFramework.Script
{
    [TestFixture]
    public class Test : TestFixture
    {
        // [Test] I will convert this script to BDD framework please refer the Test.feature
        public void TestExtentReportOnly()
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
