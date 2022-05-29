using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public class AddTasksTests
    {
        [TestMethod]
        public void ShallAddTask()
        {
            MemoryStream stream = new MemoryStream();
            ToDoHandler handler = new ToDoHandler(stream);

            string[] arguments = new string[]
            {
                "task",
                "-t",
                "Complete Application",
                "-d",
                "2018-04-01"
            };

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(stream.ToArray());
            Assert.AreEqual("1;Complete Application;2018-04-01\r\n", content);
        }

        [TestMethod]
        public void ShallPrintConfirmationAfterAddingTask()
        {
            MemoryStream fileStream = new MemoryStream();
            MemoryStream consoleStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(consoleStream);
            ToDoHandler handler = new ToDoHandler(fileStream, writer);

            string[] arguments = new string[]
            {
                "task",
                "-t",
                "Complete Application",
                "-d",
                "2018-04-01"
            };

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("Task added.\n", content);
        }

        [TestMethod]
        public void ShallAddTask_DifferentDate_DifferentTask()
        {
            MemoryStream stream = new MemoryStream();
            ToDoHandler handler = new ToDoHandler(stream);

            string[] arguments = new string[]
            {
                "task",
                "-t",
                "Do dishes",
                "-d",
                "2098-07-12"
            };

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(stream.ToArray());
            Assert.AreEqual("1;Do dishes;2098-07-12\r\n", content);
        }

        [TestMethod]
        public void AddOneTaskAfterAnother()
        {
            MemoryStream stream = new MemoryStream();
            ToDoHandler handler1 = new ToDoHandler(stream);
            string[] arguments1 = new string[]
            {
                "task",
                "-t",
                "Do dishes",
                "-d",
                "2098-07-12"
            };
            handler1.Handle(arguments1);

            ToDoHandler handler2 = new ToDoHandler(stream);

            string[] arguments2 = new string[]
            {
                "task",
                "-t",
                "Buy stuff",
                "-d",
                "2099-01-13"
            };

            handler2.Handle(arguments2);

            var content = Encoding.ASCII.GetString(stream.ToArray());
            Assert.AreEqual("1;Do dishes;2098-07-12\r\n2;Buy stuff;2099-01-13\r\n", content);
        }

        [TestMethod]
        public void ThreeTasks()
        {
            MemoryStream stream = new MemoryStream();
            ToDoHandler handler = new ToDoHandler(stream);
            string[] arguments = new string[]
            {
                "task",
                "-t",
                "Do dishes",
                "-d",
                "2098-07-12"
            };
            handler.Handle(arguments);

            arguments = new string[]
            {
                "task",
                "-t",
                "Buy stuff",
                "-d",
                "2099-01-13"
            };

            handler.Handle(arguments);

           arguments = new string[]
           {
                "task",
                "-t",
                "Walk",
                "-d",
                "2090-02-23"
            };

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(stream.ToArray());
            Assert.AreEqual("1;Do dishes;2098-07-12\r\n2;Buy stuff;2099-01-13\r\n3;Walk;2090-02-23\r\n", content);
        }

    }
}
