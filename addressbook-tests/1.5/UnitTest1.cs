﻿//using System;
//using addressbook_tests;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace WebAddressbookTests
//{
//    [TestClass]
//    public class le1
//    {
//        [TestMethod]
//        public void TestMethodSquare()
//        {
//            Square s1 = new Square(5);
//            Square s2 = new Square(10);
//            Square s3 = s1;

//            //После создания метода
//            Assert.AreEqual(s1.Size, 5);
//            Assert.AreEqual(s2.Size, 10);
//            Assert.AreEqual(s3.Size, 5);

//            s3.Size = 15;

//            Assert.AreEqual(s1.Size, 15);

//            s2.Colored = true;

//            //До создания метода public int Size
//            //Assert.AreEqual(s1.getSize(), 5);
//            //Assert.AreEqual(s2.getSize(), 10);
//            //Assert.AreEqual(s3.getSize(), 5);

//            //s3.setSize(15);

//            //Assert.AreEqual(s1.getSize(), 15);
//        }

//        [TestMethod]
//        public void TestMethodCircle()
//        {
//            Circle s1 = new Circle(5);
//            Circle s2 = new Circle(10);
//            Circle s3 = s1;

//            //После создания метода
//            Assert.AreEqual(s1.Radius, 5);
//            Assert.AreEqual(s2.Radius, 10);
//            Assert.AreEqual(s3.Radius, 5);

//            s3.Radius = 15;

//            Assert.AreEqual(s1.Radius, 15);

//            s2.Colored = true;

//        }
//    }
//}
