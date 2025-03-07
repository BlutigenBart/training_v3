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
    public class ContactCreationTests : TestBase
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

            contact.Bday = "25";
            contact.Bmonth = "February";
            contact.Byear = "1900";

            contact.Aday = "21";
            contact.Amonth = "June";
            contact.Ayear = "2010";

            contact.New_group = "New Group Name";
            app.Contacts.Create(contact);
        }


        [Test]
        public void EmptyContactCreationTest()
        {

            ContactData contact = new ContactData("Ulala", "Petrovich");
            app.Contacts.Create(contact);
        }
    }
}
