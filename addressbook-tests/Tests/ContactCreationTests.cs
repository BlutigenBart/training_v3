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
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {

            ContactData contact = new ContactData("Boris", "Moiseev");
            contact.Middlename = "Mikhailovich";
            contact.Nickname = "Light";
            //contact.Photo = "";
            contact.Title = "TitleTest";
            contact.Company = "CompanyTest";
            contact.Address = "AddressTest";
            contact.Home = "HomeTest";
            contact.Mobile = "MobileTest";
            contact.Work = "WorkTest";
            contact.Fax = "FaxTest";
            contact.Email = "EmailTest";
            contact.Email2 = "Email2Test";
            contact.Email3 = "Email3Test";
            contact.Homepage = "HomepageTest";

            //contact.Bday = "25";
            //contact.Bmonth = "February";
            //contact.Byear = "1900";

            //contact.Aday = "21";
            //contact.Amonth = "June";
            //contact.Ayear = "2010";

            //contact.New_group = "New Group Name";

            List<ContactData> oldContacts = app.Contacts.GetContactList(); //Получение списка контактов до создания контактов
            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount()); // проверяет список на появление нового объекта

            //список объектов типа ContactData
            //List Контейнер или коллекция, объект который хранит набор других объектов
            List<ContactData> newContacts = app.Contacts.GetContactList(); //Получение списка контактов после создания контакта
            oldContacts.Add(contact);

            oldContacts.Sort();
            newContacts.Sort();
            //Перед сравнением упорядочиваем
            Assert.AreEqual(oldContacts, newContacts);

        }


        [Test]
        public void BadNameContactCreationTest()
        {

            ContactData contact = new ContactData("a'a", "b'b");
            contact.Middlename = "c'c";

            List<ContactData> oldContacts = app.Contacts.GetContactList(); 

            app.Contacts.Create(contact);
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);

            oldContacts.Sort();
            newContacts.Sort();
            //Перед сравнением упорядочиваем
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}