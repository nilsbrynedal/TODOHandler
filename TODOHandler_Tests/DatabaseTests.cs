using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TODOHandler;

namespace TODOHandler_Tests
{
    /// <summary>
    /// Tests for any instance of IDatabase.
    /// </summary>
    [TestClass]
    public abstract class DatabaseTests
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
    }
}
