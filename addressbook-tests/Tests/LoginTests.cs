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
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            //Подготовка
            //Для того что бы попасть в нужную тестовую ситуацию уникальную для конерктного тестового метода
            app.Auth.Logout();

            //Действие
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);

            //Проверка
            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }


        [Test]
        public void LoginWithInvalidCredentials()
        {
            //Подготовка
            //Для того что бы попасть в нужную тестовую ситуацию уникальную для конерктного тестового метода
            app.Auth.Logout();

            //Действие
            AccountData account = new AccountData("admin", "348367834636446");
            app.Auth.Login(account);

            //Проверка
            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
