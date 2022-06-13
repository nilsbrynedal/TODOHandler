using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public abstract class ToDoHandlerTestClassBase : AbstractTestBase
    {
        protected MemoryStream consoleStream;
        protected StreamWriter writer;

        private ToDoHandler instance;

        protected ToDoHandler GetHandler()
        {
            if (instance == null)
            {
                consoleStream = new MemoryStream();
                writer = new StreamWriter(consoleStream);
                instance = new ToDoHandler(GetInstance(), writer);
            }

            return instance;
        }
    }
}