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
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void TestSearch()
        {

            // System.Console.Out.Write(app.Contacts.GetNumberOfSearchResults());
            // Можно обратиться к span который имеет id search_count
        }
    }
}