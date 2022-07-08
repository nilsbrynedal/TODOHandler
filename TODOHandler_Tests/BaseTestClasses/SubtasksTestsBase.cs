using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace TODOHandler_Tests.BaseTestClasses
{
    [TestClass]
    public abstract class SubtasksTestsBase : ToDoHandlerTestClassBase
    {
        [TestMethod]
        public void AddSubTaskToExistingTask_ShallShowSubtaskAdded()
        {
            // given a handler with one task
            AddOneTask();
            consoleStream.Position = 0;

            // when one subtask is added:
            string[] arguments = new string[]
            {
                "subtask",
                "-t",
                "My subtask",
                "-d",
                "2018-04-01",
                "-p",
                "1"
            };
            GetHandler().Handle(arguments);

            // then
            var contents = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Subtask added.\n", contents);
        }

        [TestMethod]
        public void AddSubTaskToExistingTask_ShallActuallyAddTheSubtask()
        {
            // given a handler with one task
            AddOneTask();

            // when one subtask is added:
            string[] arguments = new string[]
            {
                "subtask",
                "-t",
                "My subtask",
                "-d",
                "2018-01-01",
                "-p",
                "1"
            };
            GetHandler().Handle(arguments);
            consoleStream.Position = 0;

            // and when one lists all tasks
            arguments = new string[]
            {
                "list",
                "All"
            };
            GetHandler().Handle(arguments);

            // then
            var contents = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("ID: 1\nTask: Complete Application\nDue: 2018-04-01\n< Subtasks >\nID: 2\nSubtask: My subtask\nDue: 2018-01-01\n", contents);
        }

        [TestMethod]
        public void AddSubTaskToExistingTaskWhileAnotherTaskExists_ShallShowSubtaskAdded()
        {
            // given a handler with one task
            AddOneTask();

            // add another
            AddSecondTask();

            // when one subtask is added:
            string[] arguments = new string[]
            {
                "subtask",
                "-t",
                "My subtask",
                "-d",
                "2018-04-01",
                "-p",
                "2"
            };
            GetHandler().Handle(arguments);

            // then
            var contents = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Task added.\nTask added.\nSubtask added.\n", contents);
        }

        private void AddSecondTask()
        {
            string[] arguments = new string[]
            {
                "task",
                "-t",
                "Stuff",
                "-d",
                "2018-04-01"
            };
            GetHandler().Handle(arguments);
        }

        private string[] AddOneTask()
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
            return arguments;
        }

        [DataTestMethod]
        [DataRow("2")]
        [DataRow("3")]
        public void AddSubTaskToNonexistingTask_ShallShowErrorMessage(string ID)
        {
            // given a handler with one task
            AddOneTask();

            // when one subtask is added, but with an ID that does not exist:
            string[] arguments = new string[]
            {
                "subtask",
                "-t",
                "My subtask",
                "-d",
                "2018-04-01",
                "-p",
                ID
            };
            consoleStream.Position = 0;
            GetHandler().Handle(arguments);

            // then
            var contents = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Error: Task ID does not exist.\n", contents);
        }
    }
}
