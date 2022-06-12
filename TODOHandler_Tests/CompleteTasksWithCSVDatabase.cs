using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public class CompleteTasksWithCSVDatabase : CompleteTasksBase
    {
        public override IDatabase GetInstance()
        {
            return new CSVDatabase(new MemoryStream());
        }
    }
}
