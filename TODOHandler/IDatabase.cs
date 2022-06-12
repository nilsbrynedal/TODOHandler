using System.Collections.Generic;

namespace TODOHandler
{
    public interface IDatabase
    {
        /// <summary>
        /// Reads all tasks.
        /// </summary>
        /// <returns></returns>
        List<Task> ReadAllTasks();

        /// <summary>
        /// Completes a task, if it exists.
        /// </summary>
        /// <param name="ID">ID of the task</param>
        /// <returns>Returns true if the task existed</returns>
        bool CompleteTask(int ID);

        /// <summary>
        /// Adds a task at the end. ID becomes the next unused one.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="due"></param>
        void Add(string description, string due);
    }
}