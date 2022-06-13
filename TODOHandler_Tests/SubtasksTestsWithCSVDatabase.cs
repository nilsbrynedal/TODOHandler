using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TODOHandler;
using TODOHandler_Tests.BaseTestClasses;

namespace TODOHandler_Tests
{
    [TestClass]
    public class SubtasksTestsWithCSVDatabase : SubtasksTestsBase
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
