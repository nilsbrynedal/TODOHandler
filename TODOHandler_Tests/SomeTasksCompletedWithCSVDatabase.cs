using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public class SomeTasksCompletedWithCSVDatabase : SomeTasksCompletedBase
    {
        private ToDoHandler instance;

        protected override ToDoHandler GetHandler()
        {
            if (instance == null)
            {
                instance = new ToDoHandler(new CSVDatabase(new MemoryStream()), writer);
            }

            return instance;
        }
    }
}
