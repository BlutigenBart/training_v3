﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_tests;
using NUnit.Framework;

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
    }
}
