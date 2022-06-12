using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TODOHandler;

namespace TODOHandler_Tests
{
    /// <summary>
    /// Tests for CSVDatabase. Uses all the tests in DatabaseTester.
    /// </summary>
    [TestClass]
    public class CSVDatabaseTests : DatabaseTestsBase
    {
        private IDatabase instance;

        public override IDatabase GetInstance()
        {
            if(instance == null)
            {
                instance = new CSVDatabase(new MemoryStream());
            }
            return instance;
        }
    }
}
