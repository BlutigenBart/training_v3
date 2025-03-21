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
        [Test]
        public void GroupCreationTest()
        {
            
            // Вариант передачи полей если данных нужно много, более понятен
            GroupData group = new GroupData("New Group Name");
            group.Header = "newHeader2";
            group.Footer = "newFooter3";
            List<GroupData> oldGroups = app.Groups.GetGroupList(); //Получение списка групп до создания группы
            app.Groups.Create(group); //Создание группы
            //список объектов типа GroupData
            //List Контейнер или коллекция, объект который хранит набор других объектов
            List<GroupData> newGroups = app.Groups.GetGroupList(); //Получение списка групп после создания группы
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count); //кол-во элементов в списке увеличилось на 1
            // Конструктор если полей не так много и понятно что и куда вводить
            //FillGroupForm(new GroupData("Имя группы", "new2", "new3"));
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            List<GroupData> newGroups = app.Groups.GetGroupList(); //Получение списка групп после создания группы
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count); //кол-во элементов в списке увеличилось на 1
        }


        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("a'a");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            List<GroupData> newGroups = app.Groups.GetGroupList(); //Получение списка групп после создания группы
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count); //Проверка кол-во элементов в списке увеличилось на 1
        }

    }
}
