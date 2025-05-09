﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using NUnit.Framework;
using WebAddressbookTests;

namespace WebAddressbookTests
{
    [TestFixture]
    //public class GroupModificationTests : AuthTestBase
    public class GroupModificationTests : GroupTestBase

    {
        /// <summary>
        /// Новый тест по уроку 7.2
        /// </summary>
        [Test]
        public void GroupModificationTestDB()
        {
            app.Groups.ConfirmGroupExists();
            GroupData newData = new GroupData("NEW NAME MODIFY DB");
            newData.Header = "NEW HEADER MODIFY DB";
            newData.Footer = "NEW FOOTER MODIFY DB";
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData oldData = oldGroups[0];

            app.Groups.Modify(oldData, newData);
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount()); 

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(group.Name, newData.Name);
                }
            }
        }

        [Test]
        public void GroupModificationTest()
        {
            // Открытие главной страницы и авторизация в TestBase
            GroupData newData = new GroupData("NEW NAME");
            newData.Header = "NEW HEADER";
            newData.Footer = "NEW FOOTER";

            List<GroupData> oldGroups = app.Groups.GetGroupList(); //Получение списка групп до создания группы
            GroupData oldData = oldGroups[0];

            // Перед модификацией проверяет, есть ли хотя бы одна группа
            // Если группы нет, создаем ее
            app.Groups.ConfirmGroupExists();

            // Модифицируем первую группу (если она есть)
            app.Groups.Modify(0, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount()); //проверяет список на неизменность

            List<GroupData> newGroups = app.Groups.GetGroupList(); //Получение списка групп после создания группы
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            //Перед сравнением упорядочиваем
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id) 
                {
                    Assert.AreEqual(group.Name, newData.Name);
                }
            }
        }
    }
}
