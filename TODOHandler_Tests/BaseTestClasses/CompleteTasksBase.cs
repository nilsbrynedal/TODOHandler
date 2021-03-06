using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace TODOHandler_Tests
{
    [TestClass]
    public abstract class CompleteTasksBase : ToDoHandlerTestClassBase
    {
        [TestInitialize]
        public void SetUp()
        {
            string[] arguments = new string[]
            {
                "task",
                "-t",
                "Complete Application",
                "-d",
                "2018-04-01"
            };
            GetHandler().Handle(arguments);
            consoleStream.Position = 0;
        }

        [TestMethod]
        public void CompletingAnExistingTaskShallShowConfirmation()
        {
            string[] arguments = new string[]
            {
                "-c",
                "1"
            };

            GetHandler().Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Task completed.\n", content);
        }

        [TestMethod]
        public void CompletingAnExistingTaskShallMarkItAsCompleted()
        {
            string[] arguments = new string[]
            {
                "-c",
                "1"
            };

            GetHandler().Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Task completed.\n", content);
        }

        
        [TestMethod]
        public void CompletingAnExistingTaskShallNotAffectOtherTasks()
        {
            // add a task
            string[] arguments = new string[]
            {
                "task",
                "-t",
                "Take a walk",
                "-d",
                "2022-04-01"
            };
            GetHandler().Handle(arguments);
            consoleStream.Position = 0;

            // complete one task
            arguments = new string[]
            {
                "-c",
                "1"
            };
            GetHandler().Handle(arguments);
            
            // list tasks
            arguments = new string[]
            {
                "list",
                "All"
            };
            GetHandler().Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Task completed.\nID: 1\nTask: Complete Application\nDue: Complete\nID: 2\nTask: Take a walk\nDue: 2022-04-01\n", content);
        }


        [TestMethod]
        public void CompletingNonExistingTaskShallNotChangeFileContents()
        {
            // arguments to complete a task that does not exist:
            string[] arguments = new string[]
            {
                "-c",
                "2"
            };
            GetHandler().Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Error: Task ID not found.\n", content);
            consoleStream.Position = 0; // remove any previously read contet

            // arcuments to list everything
            arguments = new string[]
            {
                "list",
                "all"
            };
            GetHandler().Handle(arguments);

            content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("ID: 1\nTask: Complete Application\nDue: 2018-04-01\n", content);
        }

        [TestMethod]
        public void CompletingNonExistingTaskShallGiveErrorMessage()
        {
            string[] arguments = new string[]
            {
                "-c",
                "2"
            };

            GetHandler().Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());

            // provide correct output
            Assert.AreEqual("Error: Task ID not found.\n", content);
        }
    }
}
