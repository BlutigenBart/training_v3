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
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            // Открытие главной страницы и авторизация в TestBase
            // цифра от 2-х
            app.Contacts.Remove(2);
        }

        [Test]
        public void EmptyContractRemovalTest()
        {
            // Перед удалением проверяет, есть ли хотя бы один контакт
            // Если контакта нет, создаем его
            app.Contacts.ConfirmContactExists();

            // Модифицируем первую группу (если она есть)
            app.Contacts.Remove(2);
        }
    }
}
