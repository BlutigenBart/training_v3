using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework;
using System.Linq;

namespace WebAddressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {

            app.Groups.ConfirmGroupExists();
            app.Contacts.ConfirmContactExistsBD();

            GroupData group = GroupData.GetAll()[0];

            app.Contacts.ConfirmContactWithGroupNotExists(group);

            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();
            

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
