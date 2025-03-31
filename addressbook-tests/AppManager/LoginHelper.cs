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
    public class LoginHelper : HelperBase
    {

        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {   ///Если залогинены но пользователь не тот который нужен
            if (IsLoggedIn())
            {
                //Если уже залогинен под нужным юзером
                if (IsLoggedIn(account))
                {
                    //ничего не происходит
                    return;
                }
                ///выполняется логаут
                Logout();
            }
            Type(By.Name("user"), account.Username);
            Type(By.Name("pass"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                // Выход из системы
                driver.FindElement(By.LinkText("Logout")).Click();
            }

        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            // Залогинен и имя текущее пользователя в скобках равно тому тексту что указан в элементе
            return IsLoggedIn()
                && GetLoggetUserName() == account.Username;
        }

        public string GetLoggetUserName()
        {
           //сохранение в текст
           string text = driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text;
           //извлеч на 2 меньше чем общая длинна строки, отрезаем 1 и последний элемент строки
           return text.Substring(1, text.Length - 2);
        }
    }
}
