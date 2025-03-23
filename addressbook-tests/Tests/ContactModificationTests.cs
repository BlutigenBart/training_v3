using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            
            ContactData newData = new ContactData("NEW Firstname", "NEW Lastname");
            newData.Middlename = "NEW Middlename";
            newData.Nickname = "NEW Nickname";

            List<ContactData> oldContacts = app.Contacts.GetContactList(); //Получение списка контактов до создания контактов

            // Перед модификацией проверяет, есть ли хотя бы одна группа
            // Если группы нет, создаем ее
            app.Contacts.ConfirmContactExists();

            //начинается с 2-х в ColtractHelpers метода InitContactModifications прописано в хпасе index + 2
            app.Contacts.Modify(0, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            //список объектов типа ContactData
            //List Контейнер или коллекция, объект который хранит набор других объектов
            List<ContactData> newContacts = app.Contacts.GetContactList(); //Получение списка контактов после создания контакта
            
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts.Sort();
            newContacts.Sort();

            //Перед сравнением упорядочиваем
            Assert.AreEqual(oldContacts, newContacts);

        }

    }
}
