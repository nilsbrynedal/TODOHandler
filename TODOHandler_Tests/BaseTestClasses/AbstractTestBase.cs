using Microsoft.VisualStudio.TestTools.UnitTesting;
using TODOHandler;

namespace TODOHandler_Tests
{
    [TestClass]
    public abstract class AbstractTestBase
    {
        /// <summary>
        /// Override this method to implement the tests.
        /// </summary>
        /// <returns>Shall return an instance of the IDatabase</returns>
        public abstract IDatabase GetInstance();
    }
}