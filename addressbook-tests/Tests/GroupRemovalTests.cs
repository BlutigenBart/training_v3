using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            // Открытие главной страницы и авторизация в TestBase
            // Перед удалением проверяет, есть ли хотя бы одна группа
            // Если группы нет, создаем ее
            app.Groups.ConfirmGroupExists();

            List<GroupData> oldGroups = app.Groups.GetGroupList(); //Получение списка групп до создания группы

            // Удаляем первую группу (если она есть)
            app.Groups.Remove(0);

            //список объектов типа GroupData
            //List Контейнер или коллекция, объект который хранит набор других объектов
            List<GroupData> newGroups = app.Groups.GetGroupList(); //Получение списка групп после создания группы

            oldGroups.RemoveAt(0);

            Assert.AreEqual(oldGroups, newGroups);  // сравнение списков, списка до удаления и после удаления
        }
    }
}
