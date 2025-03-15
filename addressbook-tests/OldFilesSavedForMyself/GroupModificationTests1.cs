//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading;
//using addressbook_tests;
//using NUnit.Framework;
//using WebAddressbookTests;

//namespace WebAddressbookTests
//{
//    [TestFixture]
//    public class GroupModificationTests1 : AuthTestBase
//    {
//        [Test]
//        public void GroupModificationTest()
//        {
//            // Открытие главной страницы и авторизация в TestBase
//            GroupData newData = new GroupData("123");
//            newData.Header = "346";
//            newData.Footer = "789";
//            app.Groups.Modify(1, newData);
//        }


//        [Test]
//        public void EmptyGroupModificationTest()
//        {
//            // Открытие главной страницы и авторизация в TestBase
//            GroupData newData = new GroupData("789");
//            newData.Header = "987";
//            newData.Footer = "777";

//            // Перед модификацией проверяет, есть ли хотя бы одна группа
//            // Если группы нет, создаем ее
//            app.Groups.ConfirmGroupExists();

//            // Модифицируем первую группу (если она есть)
//            app.Groups.Modify(1, newData);
//        }
//    }
//}
