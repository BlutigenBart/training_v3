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
    public class GroupModificationTests : AuthTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            // Открытие главной страницы и авторизация в TestBase
            GroupData newData = new GroupData("NEW NAME");
            newData.Header = "NEW HEADER";
            newData.Footer = "NEW FOOTER";

            // Перед модификацией проверяет, есть ли хотя бы одна группа
            // Если группы нет, создаем ее
            app.Groups.ConfirmGroupExists();

            // Модифицируем первую группу (если она есть)
            app.Groups.Modify(1, newData);
        }
    }
}
