using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList(); //Получение списка контактов до создания контактов

            // Перед удалением проверяет, есть ли хотя бы один контакт
            // Если контакта нет, создаем его
            app.Contacts.ConfirmContactExists();

            // Открытие главной страницы и авторизация в TestBase
            //начинается с 2-х в ColtractHelpers метода SelectContact прописано в хпасе index + 2
            app.Contacts.Remove(0);

            // Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount()); //по уроку 4.5

            //список объектов типа ContactData
            //List Контейнер или коллекция, объект который хранит набор других объектов
            List<ContactData> newContacts = app.Contacts.GetContactList(); //Получение списка контактов после создания контакта

            //ContactData toBeRemoved = oldContacts[0]; //по уроку 4.5

            oldContacts.RemoveAt(0);

            //Перед сравнением упорядочиваем
            Assert.AreEqual(oldContacts, newContacts);

            //по уровку 4.5
            //foreach (ContactData contact in newContacts) //проверка на то что удалена была именно та запись
            //{
            //    Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            //}

        }

    }
}
