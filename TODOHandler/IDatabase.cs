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
        /// Adds a line at the end. ID becomes the next unused one.
        /// </summary>
        /// <param name="line"></param>
        void Add(List<string> line);
    }
}