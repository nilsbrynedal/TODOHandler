using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public class CompleteTasks
    {
        protected MemoryStream fileStream;
        protected ToDoHandler handler;
        protected MemoryStream consoleStream;
        protected StreamWriter writer;

        [TestInitialize]
        public void SetUp()
        {
            fileStream = new MemoryStream();
            consoleStream = new MemoryStream();
            writer = new StreamWriter(consoleStream);
            handler = new ToDoHandler(fileStream, writer);
            string[] arguments = new string[]
            {
                "task",
                "-t",
                "Complete Application",
                "-d",
                "2018-04-01"
            };
            handler.Handle(arguments);
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

            handler.Handle(arguments);

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

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(fileStream.ToArray());
            Assert.AreEqual("1;Complete Application;Complete\r\n", content);
        }

        
        [TestMethod]
        public void CompletingAnExistingTaskShallNotAffectOtherTasks()
        {
            string[] arguments = new string[]
            {
                "task",
                "-t",
                "Take a walk",
                "-d",
                "2022-04-01"
            };
            handler.Handle(arguments);
            consoleStream.Position = 0;

            arguments = new string[]
            {
                "-c",
                "1"
            };

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(fileStream.ToArray());
            Assert.AreEqual("1;Complete Application;Complete\r\n2;Take a walk;2022-04-01\r\n", content);
        }


        [TestMethod]
        public void CompletingNonExistingTaskShallNotChangeFileContents()
        {
            string[] arguments = new string[]
            {
                "-c",
                "2"
            };

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(fileStream.ToArray());

            // contents of the file shall be unchanged
            Assert.AreEqual("1;Complete Application;2018-04-01\r\n", content);

            content = Encoding.ASCII.GetString(consoleStream.ToArray());
        }

        [TestMethod]
        public void CompletingNonExistingTaskShallGiveErrorMessage()
        {
            string[] arguments = new string[]
            {
                "-c",
                "2"
            };

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());

            // provide correct output
            Assert.AreEqual("Error: Task ID not found.\n", content);
        }
    }
}
