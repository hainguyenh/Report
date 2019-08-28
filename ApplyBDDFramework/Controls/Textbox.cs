using ApplyBDDFramework.BaseClass;
using OpenQA.Selenium;

namespace ApplyBDDFramework.Controls
{
    public class Textbox : BaseControl
    {
        public Textbox(IWebElement control)
          : base(control)
        {
        }

        public void SendKeys(string text)
        {
            if (WrappedControl is null)
            {
                throw new NotFoundException("Element not found.");
            }
            WrappedControl.Clear();
            WrappedControl.SendKeys(text);
            if (!text.Contains(Keys.Tab) & !text.Contains(Keys.Enter) & !text.Contains(Keys.Escape))
                CaptureStepAndLogInfo(WrappedControl, $"Input '{text}' to the textbox.");
        }

        public void Clear()
        {
            if (WrappedControl is null)
            {
                throw new NotFoundException("Element not found.");
            }
            WrappedControl.Clear();
            CaptureStepAndLogInfo(WrappedControl, "Clear value on the textbox.");
        }

    }
}
