using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public class ReadTasks
    {
        [TestMethod]
        public void ReadATask()
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

            handler = new ToDoHandler(fileStream, writer);

            arguments = new string[]
            {
                "list",
                "-s",
                "All"
            };

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());
            Assert.AreEqual("ID: 1\nTask: Complete Application\nDue: 2018-04-01\n", content);
        }

        [TestMethod]
        public void ShallProvideErrorMessageWhenNoArgumentsAreGiven()
        {
            MemoryStream fileStream = new MemoryStream();
            MemoryStream consoleStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(consoleStream);
            ToDoHandler handler = new ToDoHandler(fileStream, writer);

            string[] arguments = new string[]
            {
            };

            handler.Handle(arguments);

            var content = Encoding.ASCII.GetString(consoleStream.ToArray());

            string expected = "Please provide arguments. Usage examples:\ntodo.exe task -t Complete Application -d 2018-04-01\ntodo.exe list -s [All| Incomplete]\n";

            Assert.AreEqual(expected, content);
        }
    }
}
