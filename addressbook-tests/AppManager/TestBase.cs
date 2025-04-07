using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V131.Tracing;

namespace WebAddressbookTests
{
    public class TestBase
    {

        protected ApplicationManager app;

        [SetUp]
        protected void SetupApplicationManager()
        {
            //Получение доступа к единственному экземпляру который хранится в ApplicationManager
            app = ApplicationManager.GetInstance();
        }

        // 1 генератор который генерирует разные числа после вынесения из метода GenerateRandomString
        public static Random rnd = new Random();
        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 1; i++) 
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65))); // 223 , 65
            }
            return builder.ToString();
        }
    }
}
