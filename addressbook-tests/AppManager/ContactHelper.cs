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
using System.Text.RegularExpressions;
using System.Linq;
using OpenQA.Selenium.BiDi.Communication;
using Google.Protobuf.WellKnownTypes;

namespace WebAddressbookTests
// Когда вызывается в этом хелпере метод в результате возвращается ссылка на него же самого
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        //public ContactHelper InitContactModification(int index)
        //{
        //    //Мой старый вариант
        //    //driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr["+ (index+2) +"]/td[8]/a/img")).Click();
        //    //Вариант из урока 5.3
        //    driver.FindElements(By.Name("entry"))[index]
        //          .FindElements(By.TagName("td"))[7]
        //          .FindElement(By.TagName("a")).Click();
        //    return this;
        //}

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                  .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;

            string allEmails = cells[4].Text;

            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones,
            };
        }
        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string home = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobile = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string work = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Home = home,
                Mobile = mobile,
                Work = work,
                Email = email,
                Email2 = email2,
                Email3 = email3,
            };
        }

        public ContactData GetContactInformationFromEditForms(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middlName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");

            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");


            string home = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobile = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string work = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Firstname = firstName,
                Middlename = middlName,
                Lastname = lastName,
                Nickname = nickName,
                Title = title,
                Company = company,
                Address = address,
                Home = home,
                Mobile = mobile,
                Work = work,
                Fax = fax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Homepage = homepage,

            };
        }

        public ContactData GetContactInformationFromDetailsForm()
        {
            manager.Navigator.GoToHomePage();
            InitContactDetails(0);

            string fullText = driver.FindElement(By.XPath("//div[@id = 'content']")).Text;
            string normText = fullText.Replace("\r\n", "\n").Replace("<br>", "");
            string[] lines = normText.Split('\n');
            string fullName = lines[0];
            string allInformationOnDetails = "";

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] != "")
                {
                    allInformationOnDetails = allInformationOnDetails + lines[i] + "\r\n";
                }
            }

            return new ContactData()
            {
                FullName = fullName,
                AllInformationOnDetails = allInformationOnDetails.Trim(),
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d +").Match(text);
            return Int32.Parse(m.Value);
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
            manager.Navigator.GoToHomePage();
            InitContactModifications(i);
            Thread.Sleep(2000);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }
        /// <summary>
        /// Новая модификация по уроку 7.2
        /// </summary>
        public ContactHelper Modify(ContactData contact, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            InitContactModifications(contact.Id);
            Thread.Sleep(2000);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int i)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(i);
            RemoveContact();
            return this;
        }
        /// <summary>
        /// Новое удаления из урока 7.2
        /// </summary>
        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(contact.Id);
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

        // Для домашнего задания №8
        // Метод для проверки наличия хотя бы одной группы и создания её, если её нет
        public ContactHelper ConfirmContactExistsBD()
        {
            if (!ContactData.GetAll().Any())
            {
                manager.Navigator.GoToContactPage();

                ContactData contact = new ContactData
                {
                    Firstname = "Vasiliy",
                    Lastname = "Dmitrievich",
                    Middlename = "Andreevich",
                    Nickname = "Nosok"
                };

                Create(contact);
            }
            return this;
        }
        //public bool IsContactDetection(string id)
        //{
        //    manager.Navigator.GoToHomePage();
        //    // Проверка наличия хотя бы одной группы на странице
        //    return IsElementPresent(By.XPath("//input[@id = '" + id + "']"));
        //}


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

        public ContactHelper InitContactDetails(int index)
        {
            //Вариант из урока 5.3
            driver.FindElements(By.Name("entry"))[index]
                  .FindElements(By.TagName("td"))[6]
                  .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            //Вариант из урока 5.3
            driver.FindElements(By.Name("entry"))[index]
                  .FindElements(By.TagName("td"))[7]
                  .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModifications(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index + 2) + "]/td[8]/a/img")).Click();
            return this;
        }
        public ContactHelper InitContactModifications(String id)
        {
            driver.FindElement(By.XPath("//input[@value = '" + id + "']/parent::td/following-sibling::td[7]/a/img")).Click();
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
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index + 2) + "]/td/input")).Click();
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
                manager.Navigator.GoToHomePage();
                // получение элементов представлющих контакты
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//table/tbody/tr[@name = 'entry']"));

                foreach (IWebElement element in elements) //Для каждого элемента в такой то коллекции нужно выполнить какие то действия
                {
                    // XPath для извлечения имени и фамилии
                    string lastname = element.FindElement(By.XPath("./td[2]")).Text;
                    string firstname = element.FindElement(By.XPath("./td[3]")).Text;
                    //Добавляем контакт в список
                    contactCache.Add(new ContactData(firstname, lastname));

                    //contactCache.Add(new ContactData(firstname, lastname) //по уровку 4.5
                    //{
                    //    Id = element.FindElement(By.TagName("td")).GetAttribute("id")
                    //});
                }
            }
            return new List<ContactData>(contactCache);
            //ICollection<IWebElement> более общий тип
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("table tbody tr[name='entry']")).Count;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void SelectContact(string contactid)
        {
            driver.FindElement(By.Id(contactid)).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void GoToGroup(GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupInFilter(group.Id);
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupInFilter(group.Id);
            SelectContact(contact.Id);
            CommitDeletingContactToGroup();
            GoToGroupAfterDeletingContact();
            //new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            //    .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void SelectGroupInFilter(string id)
        {
            driver.FindElement(By.XPath("//select[@name = 'group']/option[@value ='" + id + "']")).Click();
        }

        public void CommitDeletingContactToGroup()
        {
            driver.FindElement(By.XPath("//input[@name = 'remove']")).Click();
        }

        public void GoToGroupAfterDeletingContact()
        {
            driver.FindElement(By.XPath("//a[contains(., 'group page')]")).Click();
        }

        /// <summary>
        /// Проверка наличия хотоя бы одного контакта в группе
        /// </summary>
        public ContactHelper ConfirmContactToGroupExists(GroupData group)
        {
            // Проверяем, есть ли хотя бы одна группа
            if (!IsContactDetectionInGroup(group))
            {
                manager.Navigator.GoToContactPage();
                ContactData contact = ContactData.GetAll().First();
                AddContactToGroup(contact, group);
            }
            return this;
        }
        public bool IsContactDetectionInGroup(GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupInFilter(group.Id);
            // Проверка наличия хотя бы одной группы на странице
            return IsElementPresent(By.XPath("//td[@class = 'center']/input[@type = 'checkbox']"));
        }

        /// <summary>
        /// Проверка наличия хотоя бы одного контакта не в группе
        /// </summary>
        public ContactHelper ConfirmContactWithGroupNotExists(GroupData group)
        {
            if (IsContactDetectionInGroup(group))
            {
                ContactData contact = group.GetContacts().First();
                RemoveContactFromGroup(contact, group);
            }
            return this;
        }

        public bool IsContactDetectionInHome(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            ContactInList(contact.Id);
            // Проверка наличия хотя бы одной группы на странице
            return IsElementPresent(By.XPath("//td[@class = 'center']/input[@type = 'checkbox']"));
        }

        public void ContactInList(string id)
        {
            driver.FindElement(By.XPath("//input[@id = '" + id + "']")).Click();
        }

        //В этом задании основную сложность представляет проверка и обеспечение предусловий.

        //Во-первых, контактов или групп может не быть вообще, в этом случае нужно их создать.

        //Во-вторых, для добавления контакта в группу нужно найти такую пару контакт и группа,
        //что контакт в группу не входит. Потому что "добавление контакта в группу, в которую он уже входит"
        //это совершенно отдельный тест. При этом нужно учесть, что все контакты могут быть уже добавлены во все группы.
        //Например, попробуйте запустить тест в ситуации, когда есть один контакт и одна группа,
        //в которую этот контакт уже добавлен.

        //Аналогично, для удаления контакта из группы нужно найти подходящую пару.
        //Нельзя взять первую попавшуюся группу и первый попавшийся контакт.
        //При этом может быть ситуация, что найти такую пару невозможно.
        //Например, попробуйте запустить тест в ситуации, когда есть один контакт и одна группа, в которую контакт не добавлен.

        //А ещё в тесте для удаления контакта есть вот такая строчка, явно лишняя:
        //ContactData contact = ContactData.GetAll().Except(oldList).First();
        //потому что переменная contcat дальше нигде не используется.

    }
}

