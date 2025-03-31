using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
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
