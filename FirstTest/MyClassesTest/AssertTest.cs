using System;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using MyClasses.PersonClasses;

namespace MyClassesTest
{
    [TestClass]
    public class AssertTest
    {
        #region AreEqual/AreNotEqual Tests
                
        [TestMethod]        
        [Owner("QuemDesenvolveu")]        
        public void AreEqualTest()
        {
            string str1 = "Teste";
            string str2 = "Teste";

            Assert.AreEqual(str1, str2);
        }

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void AreNotEqualTest()
        {
            string str1 = "Teste";
            string str2 = "Teste2";

            Assert.AreNotEqual(str1, str2);
        }

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AreEqualCaseSensitiveTest()
        {
            string str1 = "Teste";
            string str2 = "teste";

            Assert.AreEqual(str1, str2, false);
        }

        #endregion

        #region AreSame/AreNotSame Tests

        [TestMethod]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x;

            Assert.AreSame(x, y);
        }

        [TestMethod]
        public void AreNotSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            Assert.AreNotSame(x, y);
        }

        #endregion

        #region IsInstanceOfType Tests

        [TestMethod]
        public void IsInstanceOfTypeTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("TestFirstName", "TestLastName", true);

            Assert.IsInstanceOfType(per, typeof(Supervisor));
        }

        [TestMethod]
        public void IsNullTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("", "TestLastName", true);

            Assert.IsNull(per);
        }

        #endregion

    }
}
