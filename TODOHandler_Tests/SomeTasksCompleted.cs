using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace TODOHandler_Tests
{
    [TestClass]
    public class SomeTasksCompleted : CompleteTasks
    {
        [TestMethod]
        public void CompletedTaskSkallShowUpAsCompleted()
        {
            AddAnotherTaskAndCompleteTheFirstOne();

            consoleStream.Position = 0;
            string[] arguments = new string[]
            {
                "list",
                "All"
            };
            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("ID: 1\nTask: Complete Application\nDue: Complete\nID: 2\nTask: Clean windows\nDue: 2022-10-01\n", content);
        }

        [TestMethod]
        public void ListOnlyIncompleteTasks()
        {
            AddAnotherTaskAndCompleteTheFirstOne();

            consoleStream.Position = 0;
            string[] arguments = new string[]
            {
                "list",
                "Incomplete"
            };
            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("ID: 2\nTask: Clean windows\nDue: 2022-10-01\n", content);
        }

        private void AddAnotherTaskAndCompleteTheFirstOne()
        {
            string[] arguments = new string[]
                        {
                "task",
                "-t",
                "Clean windows",
                "-d",
                "2022-10-01"
                        };

            handler.Handle(arguments);

            arguments = new string[]
            {
                "-c",
                "1"
            };
            handler.Handle(arguments);
        }

        [TestMethod]
        public void ShallAllowListWithoutMoreArgumets()
        {
            AddAnotherTaskAndCompleteTheFirstOne();

            string[] arguments = new string[]
            {
                "list"
            };
            consoleStream.Position = 0;
            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("ID: 1\nTask: Complete Application\nDue: Complete\nID: 2\nTask: Clean windows\nDue: 2022-10-01\n", content);
        }
    }
}
