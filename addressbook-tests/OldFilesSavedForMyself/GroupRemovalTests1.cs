//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading;
//using addressbook_tests;
//using NUnit.Framework;

//namespace WebAddressbookTests
//{
//    [TestFixture]
//    public class GroupRemovalTests1 : AuthTestBase
//    {
//        [Test]
//        public void GroupRemovalTest()
//        {
//            // Открытие главной страницы и авторизация в TestBase
//            app.Groups.Remove(1);
//        }


//        [Test]
//        public void EmptyGroupRemovalTest()
//        {
//            // Перед удалением проверяет, есть ли хотя бы одна группа
//            // Если группы нет, создаем ее
//            app.Groups.ConfirmGroupExists();

//            // Модифицируем первую группу (если она есть)
//            app.Groups.Remove(1);
//        }
//    }
//}
