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
    //public class GroupRemovalTests : AuthTestBase
    public class GroupRemovalTests : GroupTestBase
    {
        /// <summary>
        /// Новый тест по уроку 7.2
        /// </summary>
        [Test]
        public void GroupRemovalTestDB()
        {
            app.Groups.ConfirmGroupExists();
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeRemoved = oldGroups[0];
            app.Groups.Remove(toBeRemoved);
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }

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

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount()); //проверяет список на уменьшение объекта на 1

            //список объектов типа GroupData
            //List Контейнер или коллекция, объект который хранит набор других объектов
            List<GroupData> newGroups = app.Groups.GetGroupList(); //Получение списка групп после создания группы
            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);

            Assert.AreEqual(oldGroups, newGroups);  // сравнение списков, списка до удаления и после удаления

            foreach (GroupData group in newGroups) //проверка на то что удалена была именно та запись
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
