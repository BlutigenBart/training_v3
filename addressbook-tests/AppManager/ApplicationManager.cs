using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Assert = NUnit.Framework.Assert;

namespace WebAddressbookTests
{
    public class ApplicationManager
    {

        protected IWebDriver driver; //protected означает что оно внутреннее но наследники получают к нему доступ
        protected StringBuilder verificationErrors;
        protected string baseURL;
        protected bool acceptNextAlert = true;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigator;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            //driver = new FirefoxDriver();
            string geckoDriverPath = @"C:\Windows\SysWOW64\geckodriver.exe";
            driver = new FirefoxDriver(geckoDriverPath);
            baseURL = "http://localhost";
            verificationErrors = new StringBuilder();

            loginHelper = new LoginHelper(this);
            navigator = new NavigationHelper(this, baseURL);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }
        //Диструктор, вызывается автоматически
         ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        //Глобальный метод static реализован как Singleton, возвращает всегда один и тот же объект
        public static ApplicationManager GetInstance()
        {
            //если менеджер равен нулю
            if (! app.IsValueCreated)
            {
                //нужно создать
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigator.OpenHomePage();
                app.Value = newInstance;
            }
            //если создан то ничего делать не нужно

            return app.Value;
        }

        public IWebDriver Driver { get { return driver; } }

        public LoginHelper Auth
        { get { return loginHelper; } }
        public NavigationHelper Navigator
        { get { return navigator; } }
        public GroupHelper Groups
        { get { return groupHelper; } }
        public ContactHelper Contacts
        { get { return contactHelper; } }

    }
}
