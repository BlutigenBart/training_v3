using System;
using System.Collections.Generic;
using System.Reflection;
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
// Когда вызывается в этом хелпере метод в результате возвращается ссылка на него же самого
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        //хпас по которому можно определить, создан контакт или нет, чек-бокс напротив контакта
        //td[@class = 'center']/input[@type = 'checkbox']

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactPage();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int i, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            InitContactModifications(i);
            Thread.Sleep(2000);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }


        public ContactHelper Remove(int i)
        {
            manager.Navigator.OpenHomePage();
            SelectContact(i);
            RemoveContact();
            return this;
        }

        // Для домашнего задания №8
        public bool IsContactDetection()
        {
            // Проверка наличия хотя бы одной группы на странице
            return IsElementPresent(By.XPath("//td[@class = 'center']/input[@type = 'checkbox']"));
        }

        // Для домашнего задания №8
        // Метод для проверки наличия хотя бы одной группы и создания её, если её нет
        public ContactHelper ConfirmContactExists()
        {
            // Проверяем, есть ли хотя бы одна группа
            if (!IsContactDetection())
            {
                //Переход на страницу создания контактов
                manager.Navigator.GoToContactPage();
                // Если групп нет, создаем одну
                ContactData contact = new ContactData("Vasiliy", "Dmitrievich");
                contact.Middlename = "Andreevich";
                contact.Nickname = "Nosok";
                //contact.Photo = "";
                //contact.Title = "1";
                //contact.Company = "2";
                //contact.Address = "3";
                //contact.Home = "4";
                //contact.Mobile = "5";
                //contact.Work = "6";
                //contact.Fax = "7";
                //contact.Email = "8";
                //contact.Email2 = "9";
                //contact.Email3 = "10";
                //contact.Homepage = "11";

                //contact.Bday = "12";
                //contact.Bmonth = "February";
                //contact.Byear = "1900";

                //contact.Aday = "15";
                //contact.Amonth = "June";
                //contact.Ayear = "2025";
                Create(contact);  // Вызываем метод для создания группы
            }
            return this;
        }


        public ContactHelper FillContactForm(ContactData contact)
        {
            // заполнение формы создания контакта
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);
            //driver.FindElement(By.Name("photo")).Click();
            //driver.FindElement(By.Name("photo")).Clear();
            //driver.FindElement(By.Name("photo")).SendKeys("C:\\fakepath\\Gollum.jpg");
            //driver.FindElement(By.Name("bday")).Click();
            //new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.Bday);
            //driver.FindElement(By.Name("bmonth")).Click();
            //new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.Bmonth);
            //Type(By.Name("byear"), contact.Byear);

            //driver.FindElement(By.Name("aday")).Click();
            //new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText(contact.Aday);
            //driver.FindElement(By.Name("amonth")).Click();
            //new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText(contact.Amonth);
            //Type(By.Name("ayear"), contact.Ayear);

            //driver.FindElement(By.Name("new_group")).Click();
            //new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contact.New_group);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            // Создание контакта
            driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            // Возврат на домашнюю страницу
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper InitContactModifications(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr["+ (index+2) +"]/td[8]/a/img")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index+2) +"]/td/input")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectAllContacts()
        {
            driver.FindElement(By.Id("MassCB")).Click();
            return this;
        }
        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                // получение элементов представлющих контакты
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//table/tbody/tr[@name = 'entry']"));

                foreach (IWebElement element in elements) //Для каждого элемента в такой то коллекции нужно выполнить какие то действия
                {
                    // XPath для извлечения имени и фамилии
                    string lastname = element.FindElement(By.XPath(".//td[2]")).Text;
                    string firstname = element.FindElement(By.XPath(".//td[3]")).Text;

                    // Добавляем контакт в список
                    //contactCache.Add(new ContactData(firstname, lastname));

                    contactCache.Add(new ContactData(firstname, lastname)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }

            return new List<ContactData>(contactCache);

            //ICollection<IWebElement> более общий тип
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("table tbody tr[name='entry']")).Count;
        }

    }
}

