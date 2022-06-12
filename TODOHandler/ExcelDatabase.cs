using System.Collections.Generic;
using TODOHandler;

namespace TODOHandler_Tests
{
    public class ExcelDatabase : IDatabase
    {
        public void Add(string description, string due)
        {
        }

        public bool CompleteTask(int ID)
        {
            return false;
        }

        public List<Task> ReadAllTasks()
        {
            return null;
        }
    }
}