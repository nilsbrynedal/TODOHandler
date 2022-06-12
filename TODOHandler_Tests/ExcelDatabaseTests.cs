using Microsoft.VisualStudio.TestTools.UnitTesting;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public class ExcelDatabaseTests : SomeTasksCompletedBase
    {
        private IDatabase instance;

        public override IDatabase GetInstance()
        {
            if (instance == null)
            {
                instance = new ExcelDatabase();
            }
            return instance;
        }
    }
}
