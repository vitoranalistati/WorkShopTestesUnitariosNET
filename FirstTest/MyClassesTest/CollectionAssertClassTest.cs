using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses.PersonClasses;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyClassesTest
{
    [TestClass]
    public class CollectionAssertClassTest
    {

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void AreCollectionEqualFailBecauseNoComparerTest()
        {
            //PersonManager perMgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual;

            peopleExpected.Add(new Person() { FirstName = "Jose", LastName = "Silva" });
            peopleExpected.Add(new Person() { FirstName = "Samuel", LastName = "Andrade" });
            peopleExpected.Add(new Person() { FirstName = "Picles", LastName = "Tomate" });

            //You shall not pass!
            //peopleActual = perMgr.GetPeople();
            peopleActual = peopleExpected;

            CollectionAssert.AreEqual(peopleExpected, peopleActual);
        }

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void AreCollectionEqualWithComparerTest()
        {
            PersonManager perMgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual;

            peopleExpected.Add(new Person() { FirstName = "Jose", LastName = "Silva" });
            peopleExpected.Add(new Person() { FirstName = "Samuel", LastName = "Andrade" });
            peopleExpected.Add(new Person() { FirstName = "Picles", LastName = "Tomate" });

            //You shall not pass!
            peopleActual = perMgr.GetPeople();

            CollectionAssert.AreEqual(peopleExpected, peopleActual, 
                Comparer<Person>.Create((x, y) => x.FirstName == y.FirstName && x.LastName == y.LastName ? 0 : 1));
        }


        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void AreCollectionEquivalentTest()
        {
            PersonManager perMgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual;

            peopleActual = perMgr.GetPeople();

            peopleExpected.Add(peopleActual[1]);
            peopleExpected.Add(peopleActual[2]);
            peopleExpected.Add(peopleActual[0]);

            CollectionAssert.AreEquivalent(peopleExpected, peopleActual);
        }

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void IsCollectionOfTypeTest()
        {
            PersonManager perMgr = new PersonManager();            
            List<Person> peopleActual;

            peopleActual = perMgr.GetSupervisors();

            CollectionAssert.AllItemsAreInstancesOfType(peopleActual, typeof(Supervisor));
        }

    }
}
