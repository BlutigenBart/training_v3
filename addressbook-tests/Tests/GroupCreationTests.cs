using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            
            // Вариант передачи полей если данных нужно много, более понятен
            GroupData group = new GroupData("New Group Name");
            group.Header = "newHeader2";
            group.Footer = "newFooter3";
            app.Groups.Create(group);
            // Конструктор если полей не так много и понятно что и куда вводить
            //FillGroupForm(new GroupData("Имя группы", "new2", "new3"));
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";
            app.Groups.Create(group);
        }

    }
}
