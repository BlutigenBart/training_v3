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
        public void GoToHomePage()
        {
            if (driver.Url == baseURL + "/addressbook/")
            {
                return;
            }
            // Открытие домажней страницы
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        public void GoToGroupPage()
        {
            if (driver.Url == baseURL + "/addressbook/group.php"
                && IsElementPresent(By.Name("new")))
            {
                return;
            }
            // Переход на страницу групп
            driver.FindElement(By.LinkText("groups")).Click();
        }

        public void GoToContactPage()
        {
            if (driver.Url == baseURL + "/addressbook/edit.php"
                && IsElementPresent(By.Name("submit")))
            {
                return;
            }
            // Переход на страницу создания контактов
            driver.FindElement(By.LinkText("add new")).Click();
        }
    }
}
