using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AuthTestBase : TestBase
    {

        [SetUp]
        protected void SetupLogin()
        {
            //Получение доступа к единственному экземпляру который хранится в ApplicationManager
            app.Auth.Login(new AccountData("admin", "secret"));
        }
    }
}
