using System;
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
    public class GroupModificationTests : AuthTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            // Открытие главной страницы и авторизация в TestBase
            GroupData newData = new GroupData("NEW NAME");
            newData.Header = "NEW HEADER";
            newData.Footer = "NEW FOOTER";

            // Перед модификацией проверяет, есть ли хотя бы одна группа
            // Если группы нет, создаем ее
            app.Groups.ConfirmGroupExists();

            List<GroupData> oldGroups = app.Groups.GetGroupList(); //Получение списка групп до создания группы

            // Модифицируем первую группу (если она есть)
            app.Groups.Modify(0, newData);

            List<GroupData> newGroups = app.Groups.GetGroupList(); //Получение списка групп после создания группы
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            //Перед сравнением упорядочиваем
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
