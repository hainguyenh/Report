using OpenQA.Selenium;
using System;

namespace ApplyBDDFramework.BaseClass
{
    public abstract class BaseWebPage<TPage>
        where TPage : new()
    {
        private static readonly Lazy<TPage> _lazyPage = new Lazy<TPage>(() => new TPage());

        protected readonly IWebDriver WrappedDriver;

        protected BaseWebPage() =>
            WrappedDriver = BaseDriver.Browser ?? throw new ArgumentNullException("The wrapped IWebDriver instance is not initialized.");

        public static TPage Instance => _lazyPage.Value;
    }
}
