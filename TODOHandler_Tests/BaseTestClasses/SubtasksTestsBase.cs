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

        [TestMethod]
        public void DontAllowForbiddenCharacterInTaskDescription()
        {
            // Given a task with forbidden character
            string[] arguments = new string[]
            {
                "task",
                "-t",
                "|Complete Application",
                "-d",
                "2018-04-01"
            };
            GetHandler().Handle(arguments);

            // when all tasks are read
            arguments = new string[]
            {
                "list",
                "All"
            };
            GetHandler().Handle(arguments);

            // then, correct error message, and no tasks
            var contents = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Task description contains forbidden character '|'\n", contents);
        }

        [TestMethod]
        public void DontAllowForbiddenCharacterInSubtaskDescription()
        {
            // Given a task 
            AddOneTask();
            
            // and a subtask with forbidden character
            string[] arguments = new string[]
            {
                "subtask",
                "-t",
                "Complete Application|",
                "-d",
                "2018-04-01",
                "p",
                "1"
            };
            GetHandler().Handle(arguments);

            // when all tasks are read
            arguments = new string[]
            {
                "list",
                "All"
            };
            GetHandler().Handle(arguments);

            // then, correct error message, and no tasks
            var contents = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Task added.\nTask description contains forbidden character '|'\nID: 1\nTask: Complete Application\nDue: 2018-04-01\n", contents);
        }

        [TestMethod]
        public void AddSubTaskToLastTaskWhileAnotherTaskExists_ShallShowSubtasksCorrectly()
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
                "2022-11-01",
                "-p",
                "2"
            };
            GetHandler().Handle(arguments);

            // and when one lists all tasks
            arguments = new string[]
            {
                "list",
                "All"
            };
            GetHandler().Handle(arguments);

            // then
            var contents = Encoding.ASCII.GetString(consoleStream.ToArray());

            string expected = "Task added.\nTask added.\nSubtask added.\n";
            expected += "ID: 1\nTask: Complete Application\nDue: 2018-04-01\nID: 2\nTask: Stuff\nDue: 2018-04-01\n";
            expected += "< Subtasks >\nID: 3\nSubtask: My subtask\nDue: 2022-11-01\n";

            Assert.AreEqual(expected, contents);
        }

        [TestMethod]
        public void AddSubTaskToFirstTaskWhileAnotherTaskExists_ShallShowSubtasksCorrectly()
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
                "2022-11-01",
                "-p",
                "1"
            };
            GetHandler().Handle(arguments);

            // and when one lists all tasks
            arguments = new string[]
            {
                "list",
                "All"
            };
            GetHandler().Handle(arguments);

            // then
            var contents = Encoding.ASCII.GetString(consoleStream.ToArray());

            string expected = "Task added.\nTask added.\nSubtask added.\n";
            expected += "ID: 1\nTask: Complete Application\nDue: 2018-04-01\n";
            expected += "< Subtasks >\nID: 3\nSubtask: My subtask\nDue: 2022-11-01\n";
            expected += "ID: 2\nTask: Stuff\nDue: 2018-04-01\n";
            
            Assert.AreEqual(expected, contents);
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
