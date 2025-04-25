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
    public class RemoveContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemoveContactFromGroupTest()
        {
            app.Contacts.ConfirmContactExists();
            app.Groups.ConfirmGroupExists();
            GroupData group = GroupData.GetAll()[0];
            app.Contacts.ConfirmContactToGroupExists(group);

            List<ContactData> oldList = group.GetContacts();

            ContactData contactToRemove = oldList.First();

            app.Contacts.RemoveContactFromGroup(contactToRemove, group);

            List<ContactData> newList = group.GetContacts();
            Assert.AreEqual(oldList.Count - 1, newList.Count);
        }
    }
}
