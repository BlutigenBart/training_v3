using System;
using System.IO; // Для работы с с классом File
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System.Linq;
using System.Data;


namespace WebAddressbookTests
{
    [TestFixture]
    //public class GroupCreationTests : AuthTestBase
     public class GroupCreationTests : GroupTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFie()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups1.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2],
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFie()
        {
            //(List<GroupData>) Приведение типа
            return (List<GroupData>)
                // XmlSerializer читает данные типа(List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>))
                // из указанного файла
                .Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFie()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }

        public static IEnumerable<GroupData> GroupDataFromExcelFie()
        {
            //  Создание списка в котором хранятся данные записанные из Excel
            List<GroupData> groups =new  List<GroupData>();
            // Создание объектаApplication, управляющего процессом работы с Excel
            Excel.Application app = new Excel.Application();
            // Установка видимости приложения
            app.Visible = true;
            // Открытие Excel по указанному пути и сохраняем его
            // Directory.GetCurrentDirectory() возвращает текущую рабочую директорию, где запущено приложение
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            // Получение первого листа книги
            Excel.Worksheet sheet = wb.Sheets[1];
            // Получение диапазона всех используемых ячеек
            Excel.Range range = sheet.UsedRange;

            for (int i = 1; i <= range.Rows.Count; i++)
            {
                // Данные преобразуются в строки, так как ячейки Excel могут содержать различные типы данных
                // (например, числа или даты)
                groups.Add(new GroupData()
                {
                    Name = Convert.ToString(range.Cells[i, 1].Value),
                    Header = Convert.ToString(range.Cells[i, 2].Value),
                    Footer = Convert.ToString(range.Cells[i, 3].Value)
                });
            }
            // Закрытие книги
            wb.Close();
            // Скрытие приложения
            app.Visible = false;
            // Завершение работы приложения
            app.Quit();
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromCsvFie")]
        public void GroupCreationTestCsv(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.Create(group);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test, TestCaseSource("GroupDataFromXmlFie")]
        public void GroupCreationTestXml(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.Create(group);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test, TestCaseSource("GroupDataFromJsonFie")]
        public void GroupCreationTestJson(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.Create(group);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test, TestCaseSource("GroupDataFromExcelFie")]
        public void GroupCreationTestExcel(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.Create(group);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList(); //Получение списка групп до создания группы

            app.Groups.Create(group); //Создание группы

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount()); // проверяет список на появление нового объекта

            //список объектов типа GroupData
            //List Контейнер или коллекция, объект который хранит набор других объектов
            List<GroupData> newGroups = app.Groups.GetGroupList(); //Получение списка групп после создания группы
           
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();

            //Перед сравнением упорядочиваем
            Assert.AreEqual(oldGroups, newGroups);
            
            //Assert.AreEqual(oldGroups.Count + 1, newGroups.Count); //кол-во элементов в списке увеличилось на 1
            // Конструктор если полей не так много и понятно что и куда вводить
            //FillGroupForm(new GroupData("Имя группы", "new2", "new3"));
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("a'a");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            app.Groups.Create(group);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
        /// <summary>
        /// Новый тест по уроку 7.2
        /// </summary>
        [Test, TestCaseSource("GroupDataFromJsonFie")]
        public void GroupCreationTestJsonDB(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAll();
            app.Groups.Create(group);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }

        [Test]
        public void TestDBConnectivity2()
        {
            foreach (ContactData contact in GroupData.GetAll()[0].GetContacts()) {
                System.Console.Out.WriteLine(contact);
            }

        }

        //[Test]
        //public void TestDBConnectivity2()
        //{
        //    DateTime start = DateTime.Now;
        //    List<GroupData> fromUi = app.Groups.GetGroupList();
        //    DateTime end = DateTime.Now;
        //    System.Console.Out.WriteLine(end.Subtract(start));

        //    start = DateTime.Now;
        //    using (AddressBookDB db = new AddressBookDB())
        //    { // С конструкцией using метод close вызывается автоматически в конце
        //        List<GroupData> fromDb = (from g in db.Groups select g).ToList();
        //    }
        //    end = DateTime.Now;
        //    System.Console.Out.WriteLine(end.Subtract(start));
        //}

        //[Test]
        //public void EmptyGroupCreationTest()
        //{
        //    GroupData group = new GroupData("");
        //    group.Header = "";
        //    group.Footer = "";

        //    List<GroupData> oldGroups = app.Groups.GetGroupList();

        //    app.Groups.Create(group);
        //    Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

        //    List<GroupData> newGroups = app.Groups.GetGroupList();
        //    oldGroups.Add(group);
        //    oldGroups.Sort();
        //    newGroups.Sort();

        //    Assert.AreEqual(oldGroups, newGroups);
        //}
    }
}
