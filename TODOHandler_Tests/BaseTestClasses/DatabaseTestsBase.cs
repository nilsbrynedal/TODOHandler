using Microsoft.VisualStudio.TestTools.UnitTesting;
using TODOHandler;

namespace TODOHandler_Tests
{
    /// <summary>
    /// Tests for any instance of IDatabase.
    /// </summary>
    [TestClass]
    public abstract class DatabaseTestsBase
    {
        /// <summary>
        /// Override this method to implement the tests
        /// </summary>
        /// <returns></returns>
        public abstract IDatabase GetInstance();

        [TestMethod]
        public void ShallHaveNoTasksInitially()
        {
            var tasks = GetInstance().ReadAllTasks();
            Assert.AreEqual(0, tasks.Count);
        }

        [TestMethod]
        public void ShallAddOneTask()
        {
            GetInstance().Add("Test", "2022-01-12");

            var result = GetInstance().ReadAllTasks();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Test", result[0].Description);
            Assert.AreEqual("2022-01-12", result[0].Due);
        }

        [TestMethod]
        public void ShallAddTwoTasks()
        {
            GetInstance().Add("Test", "2022-01-12");
            GetInstance().Add("Test2", "2022-01-13");

            var result = GetInstance().ReadAllTasks();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Test2", result[1].Description);
            Assert.AreEqual("2022-01-13", result[1].Due);
        }

        [TestMethod]
        public void ShallCompleteOneTask()
        {
            GetInstance().Add("Test", "2022-01-12");
            GetInstance().Add("Test2", "2022-01-13");
            GetInstance().CompleteTask(1);

            var result = GetInstance().ReadAllTasks();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Test", result[0].Description);
            Assert.AreEqual("Complete", result[0].Due);

            //should not have changed:
            Assert.AreEqual("Test2", result[1].Description);
            Assert.AreEqual("2022-01-13", result[1].Due);
        }
    }
}
