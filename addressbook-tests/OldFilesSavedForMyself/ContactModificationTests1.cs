//using System;
//using System.Diagnostics.Contracts;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading;
//using addressbook_tests;
//using NUnit.Framework;


//namespace WebAddressbookTests
//{
//    [TestFixture]
//    public class ContactModificationTests1 : AuthTestBase
//    {
//        [Test]
//        public void ContactModificationTest()
//        {
            
//            ContactData newData = new ContactData("Petr", "Petrenko");
//            newData.Middlename = "Petrovich";
//            newData.Nickname = "Volosok";
//            //contact.Photo = "";
//            newData.Title = "1";
//            newData.Company = "2";
//            newData.Address = "3";
//            newData.Home = "4";
//            newData.Mobile = "+7 921 93 93";
//            newData.Work = "WorkTest";
//            newData.Fax = "FaxTest";
//            newData.Email = "EmailTest";
//            newData.Email2 = "5";
//            newData.Email3 = "6";
//            newData.Homepage = "7";

//            newData.Bday = "27";
//            newData.Bmonth = "February";
//            newData.Byear = "1900";

//            newData.Aday = "29";
//            newData.Amonth = "June";
//            newData.Ayear = "2025";

//            //newData.New_group = "";
//            //Цифру ставить от 2-х
//            app.Contacts.Modify(2, newData);

//        }

//        [Test]
//        public void EmptyContactModificationTest()
//        {
//            ContactData newData = new ContactData("Melkiq", "Mal");
//            newData.Middlename = "Cred";
//            newData.Nickname = "Volosok";

//            // Перед модификацией проверяет, есть ли хотя бы одна группа
//            // Если группы нет, создаем ее
//            app.Contacts.ConfirmContactExists();

//            // Модифицируем первую группу (если она есть)
//            app.Contacts.Modify(2, newData);
//        }
//    }
//}
