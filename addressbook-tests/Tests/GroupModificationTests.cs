using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using NUnit.Framework;
using WebAddressbookTests;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            // Открытие главной страницы и авторизация в TestBase
            GroupData newData = new GroupData("123");
            newData.Header = "346";
            newData.Footer = "789";
            app.Groups.Modify(1, newData);
        }
    }
}
