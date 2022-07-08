using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public class XMLDatabaseTests : SomeTasksCompletedBase
    {
        private IDatabase instance;
        private MemoryStream stream;

        public override IDatabase GetInstance()
        {
            if (instance == null)
            {
                stream = new MemoryStream();
                instance = new XMLDatabase(stream);
            }
            return instance;
        }
    }
}
