using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses.PersonClasses;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyClassesTest
{
    [TestClass]
    public class PersonManagerTest
    {

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void CreatePerson_OfTypeEmployeeTest()
        {
            PersonManager perMgr = new PersonManager();
            Person per;
            
            per = perMgr.CreatePerson("João", "Maria", false);

            Assert.IsInstanceOfType(per, typeof(Employee));
        }

        [TestMethod]
        [Owner("QuemDesenvolveu")]
        public void DoEmployeeExistTest()
        {
            Supervisor super = new Supervisor();
            
            super.Employees = new List<Employee>();
            super.Employees.Add(new Employee()
            {
                FirstName = "Gustavo",
                LastName = "Santos"
            });

            Assert.IsTrue(super.Employees.Count > 0);
            
        }
    }
}
