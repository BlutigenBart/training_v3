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
        public static IEnumerable<ContactData> RandomGroupDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData()
                {
                    Firstname = GenerateRandomString(30),
                    Lastname = GenerateRandomString(30),
                    Middlename = GenerateRandomString(30),
                    Nickname = GenerateRandomString(30),
                    Title = GenerateRandomString(30),
                    Company = GenerateRandomString(30),
                    Address = GenerateRandomString(30),
                    Home = GenerateRandomString(30),
                    Mobile = GenerateRandomString(30),
                    Work = GenerateRandomString(30),
                    Fax = GenerateRandomString(30),
                    Email = GenerateRandomString(30),
                    Email2  = GenerateRandomString(30),
                    Email3 = GenerateRandomString(30),
                    Homepage = GenerateRandomString(30)
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            //contact.Photo = "";
            
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