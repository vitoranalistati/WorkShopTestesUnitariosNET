using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace MyClassesTest
{
    [TestClass]
    public class StringAssertClassTest
    {

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void ContainsTest()
        {
            string str1 = "Teste Inicial";
            string str2 = "Inicial";

            StringAssert.Contains(str1, str2);
        }

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void StartWithTest()
        {
            string str1 = "Teste Inicial Alto";
            string str2 = "Teste Inicial";

            StringAssert.StartsWith(str1, str2);
        }

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void IsAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");

            StringAssert.Matches("teste inicial", reg);
        }

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void IsNotAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");

            StringAssert.DoesNotMatch("Teste Inicial", reg);
        }

    }
}
