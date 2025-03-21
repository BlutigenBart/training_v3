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
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        //хпас по которому можно определить, создана группа или нет, чек-бокс напротив групы
        //span/input[@type = 'checkbox']
        //так же можно просто через //span

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper Modify(int i, GroupData newData)
        {
            // Если не найден элемент (группа)
            // то группа создается
            // иначе не делаем ничего - продолжается основной тест
            
            manager.Navigator.GoToGroupPage();
            SelectGroup(i);
            InitGroupModifications();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper Remove(int i)
        {
            manager.Navigator.GoToGroupPage();
            SelectGroup(i);
            RemoveGroup();
            ReturnToGroupPage();
            return this;
        }

        // Для домашнего задания №8
        public bool IsGroupDetection()
        {
            // Проверка наличия хотя бы одной группы на странице
            return IsElementPresent(By.XPath("//span/input[@type = 'checkbox']"));
        }

        // Для домашнего задания №8
        // Метод для проверки наличия хотя бы одной группы и создания её, если её нет
        public GroupHelper ConfirmGroupExists()
        {
            // Проверяем, есть ли хотя бы одна группа
            if (!IsGroupDetection())
            {
                // Если групп нет, создаем одну
                GroupData group = new GroupData("Test Group");
                group.Header = "Test Header";
                group.Footer = "Test Footer";
                Create(group);  // Вызываем метод для создания группы
            }
            return this; //Возвращает GroupHelper так как в нем еще есть методы и вызывает их цепочкой
        }

        public GroupHelper InitNewGroupCreation()
        {
            // Инициализируем создание новой группы
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            //Локальные переменные, создавались для быстрой генерации метода
            //By locator = By.Name("group_name");
            //string text = group.Name;

            // Заполнение формы создания группы
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            // Создание группы
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupPage()
        {
            // Возврат на страницу создания групп
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/span[" + (index+1) + "]/input")).Click();
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper InitGroupModifications()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> groups = new List<GroupData>();//Пустой список
            manager.Navigator.GoToGroupPage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
            foreach (IWebElement element in elements) //Для каждого элемента в такой то коллекции нужно выполнить какие то действия
            {
                groups.Add(new GroupData(element.Text));

                //Можно не делать промежуточную переменную а сразу добавить, см выше
                //GroupData group = new GroupData(element.Text);
                //groups.Add(group);
            }
            return groups;

            //ICollection<IWebElement> более общий тип
        }
    }
}
