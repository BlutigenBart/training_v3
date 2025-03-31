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
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);

            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void TestContactInformationDetails()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromEditForms(0);

            ContactData fromDetailsForm = app.Contacts.GetContactInformationFromDetailsForm(0);

            Assert.AreEqual(fromTable.FullName, fromDetailsForm.FullName);
            Assert.AreEqual(fromTable.Nickname, fromDetailsForm.Nickname);
            Assert.AreEqual(fromTable.Title, fromDetailsForm.Title);
            Assert.AreEqual(fromTable.Company, fromDetailsForm.Company);
            Assert.AreEqual(fromTable.Address, fromDetailsForm.Address);

            Assert.AreEqual(fromTable.Home, fromDetailsForm.Home);
            Assert.AreEqual(fromTable.Mobile, fromDetailsForm.Mobile);
            Assert.AreEqual(fromTable.Work, fromDetailsForm.Work);
            Assert.AreEqual(fromTable.Fax, fromDetailsForm.Fax);

            Assert.AreEqual(fromTable.Email, fromDetailsForm.Email);
            Assert.AreEqual(fromTable.Email2, fromDetailsForm.Email2);
            Assert.AreEqual(fromTable.Email3, fromDetailsForm.Email3);

            Assert.AreEqual(fromTable.Homepage, fromDetailsForm.Homepage);
        }
    }
}