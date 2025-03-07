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
    public class NavigationHelper : HelperBase
    {
        private string baseURL;
        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
            //(IWebDriver driver, string baseURL) :base(driver)
        {
            this.baseURL = baseURL;
        }
        public void OpenHomePage()
        {
            // Открытие домажней страницы
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        public void GoToGroupPage()
        {
            // Переход на страницу групп
            driver.FindElement(By.LinkText("groups")).Click();
        }

        public void GoToContactPage()
        {
            // Переход на страницу создания контактов
            driver.FindElement(By.LinkText("add new")).Click();
        }
    }
}
