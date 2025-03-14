using System;
using System.Diagnostics.Contracts;
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
            app.Contacts.Create(contact);
        }


        [Test]
        public void EmptyContactCreationTest()
        {

            ContactData contact = new ContactData("Василий", "Петрович");
            contact.Middlename = "Петренко";
            contact.Nickname = "Питер Пен";
            //contact.Photo = "";
            //contact.Title = "";
            //contact.Company = "";
            //contact.Address = "";
            //contact.Home = "";
            //contact.Mobile = "";
            //contact.Work = "";
            //contact.Fax = "";
            //contact.Email = "";
            //contact.Email2 = "";
            //contact.Email3 = "";
            //contact.Homepage = "";

            //contact.Bday = "10";
            //contact.Bmonth = "December";
            //contact.Byear = "1998";

            //contact.Aday = "20";
            //contact.Amonth = "December";
            //contact.Ayear = "2025";

            //contact.New_group = "New Group Name";
            app.Contacts.Create(contact);
        }
    }
}
