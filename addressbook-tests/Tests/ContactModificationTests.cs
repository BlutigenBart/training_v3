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
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            
            ContactData newData = new ContactData("NEW Firstname", "NEW Lastname");
            newData.Middlename = "NEW Middlename";
            newData.Nickname = "NEW Nickname";

            // Перед модификацией проверяет, есть ли хотя бы одна группа
            // Если группы нет, создаем ее
            app.Contacts.ConfirmContactExists();

            //Цифру ставить от 2-х
            app.Contacts.Modify(1, newData);

        }

    }
}
