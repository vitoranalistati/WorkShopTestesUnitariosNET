using System;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        //Usando A A A
        //Arrange - Inicializar variáveis
        //Act - Invocar método para testar
        //Assert - Verificar a ação

        private const string BAD_FILE_NAME = @"C:\BadFileName.bat";
        private const string FILE_NAME = @"FileToDeploy.txt";
        private string _GoodFileName;


        public TestContext TestContext { get; set; }

        #region Test Initialize e Cleanup   

        //Apenas camada local, não executa na camada de DLL
        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName.StartsWith("fileNameDoesExists"))
            {
                SetGoodFileName();
                SimulateTimeOut();
                if (!string.IsNullOrEmpty(_GoodFileName))
                {                    
                    TestContext.WriteLine($"Creating File: {_GoodFileName}");
                    File.AppendAllText(_GoodFileName, "Some Text");
                }
            }
        }

        //Apenas camada local, não executa na camada de DLL
        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName.StartsWith("fileNameDoesExists"))
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Deleting File: {_GoodFileName}");
                    File.Delete(_GoodFileName);
                }
            }
        }

        #endregion

        [TestMethod]
        [Description("Check to see if a file does exists")]
        [Owner("QuemDesenvolveu")]
        [Priority(0)]
        [TestCategory("NoException")]
        public void fileNameDoesExists()
        {
            #region Arrange
            
            FileProcess fp = new FileProcess();

            bool fromCall;

            #endregion

            #region Act

            //Irá executar o TestInitialize
            TestContext.WriteLine("Testing File");

            fromCall = fp.FileExists(_GoodFileName);
            //Irá executar o TestCleanup

            #endregion

            #region Assert

            Assert.IsTrue(fromCall);
            
            #endregion
        }

        [TestMethod]        
        [Owner("QuemDesenvolveu")]      
        [DataSource("System.Data.SqlClient",
            @"Data Source=localhost \SQLExpress;Initial Catalog=TesteUnitario;Integrated Security=True", 
            "FileProcessTest", DataAccessMethod.Sequential)]
        public void FileExistsTestFromDB()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool expectedValue;
            bool causesException;
            bool fromCall;

            fileName = TestContext.DataRow["FileName"].ToString();
            expectedValue = Convert.ToBoolean(TestContext.DataRow["ExpectedValue"]);
            causesException = Convert.ToBoolean(TestContext.DataRow["CausesException"]);

            try
            {
                fromCall = fp.FileExists(fileName);
                Assert.AreEqual(expectedValue, fromCall,
                    $"File: {fileName} has failed. METHOD: FileExistsTestFromDB");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(causesException);                
            }

        }

        [TestMethod]
        [Description("Check to see if a file does not exists")]
        [Owner("QuemDesenvolveu")]
        [Priority(0)]
        [TestCategory("NoException")]
        public void fileNameDoesNotExists()
        {
            #region Arrange
            FileProcess fp = new FileProcess();

            bool fromCall;
            #endregion

            #region Act
            fromCall = fp.FileExists(BAD_FILE_NAME);
            #endregion

            #region Assert
            Assert.IsFalse(fromCall);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Owner("QuemDesenvolveu_3")]
        [Priority(1)]
        [TestCategory("Exception")]
        public void fileNameNullOrEmpty_ThrowArgumentNullException()
        {
            //Exception Handling
            //Especificar o typeof() exception            
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        [Owner("QuemDesenvolveu_2")]
        [Priority(1)]
        [TestCategory("Exception")]
        public void fileNameNullOrEmpty_ThrowArgumentNullException_UsingTryCatch()
        {            
            //Try...Catch melhor em data-driven tests
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException)
            {
                //The test was a success
                return;
            }

            Assert.Fail("Fail expected");
        }

        [TestMethod]
        [Owner("QuemDesenvolveu_2")]
        [DeploymentItem(FILE_NAME)]
        public void FileNameDoesExistsUsingDeploymentItem()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool fromCall;

            fileName = $@"{TestContext.DeploymentDirectory}\{FILE_NAME}";
            TestContext.WriteLine($"Checking File: {fileName}");

            fromCall = fp.FileExists(fileName);
            
            Assert.IsTrue(fromCall);            
        }

        [TestMethod]
        [Timeout(3100)]
        public void SimulateTimeOut()
        {
            System.Threading.Thread.Sleep(3000);
        }

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }
    }
}
