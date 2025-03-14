//using System;
//using System.Diagnostics.Contracts;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Xml.Schema;
//using addressbook_tests;
//using NUnit.Framework;


//namespace WebAddressbookTests
//{
//    [TestFixture]
//    public class ifthenelse
//    {
//        [Test]
//        public void lecture3()
//        {
//            //если пользователь купил товаров на общую стоимость более 100 рублей 
//            //то ему предоставляется скидка иначе скидка не предоставляется

//            //если общая сумма больше 1000 рублей
//            double total = 1500;
//            bool vipClient = false;

//            //if (total > 1000 && vipClient)
//            //если нужно задействовать скидку по одному из двух условий
//            if (total > 1000 || vipClient)
//            //то скидка предоставляется
//            {
//                total = total * 0.9;
//                System.Console.Out.Write("Скидка 10%, общая сумма " + total);
//            }
//            //иначе скидка не предоставляется
//            // если вторая часть не требуется нужно просто удалить else
//            else
//            {
//                System.Console.Out.Write("Скидки нет, общая сумма " + total);
//            }

//        }

//    }

//}
