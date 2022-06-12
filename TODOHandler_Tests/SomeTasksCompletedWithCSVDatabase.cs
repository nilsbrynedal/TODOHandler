using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public class SomeTasksCompletedWithCSVDatabase : SomeTasksCompletedBase
    {
        private IDatabase instance;

        public override IDatabase GetInstance()
        {
            if (instance == null)
            {
                instance = new CSVDatabase(new MemoryStream());
            }

            return instance;
        }
    }
}
