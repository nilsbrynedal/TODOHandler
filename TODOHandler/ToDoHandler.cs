using System.IO;

/// <summary>
/// Test driven implementation of http://www.tddbuddy.com/katas/Todo%20List.pdf
/// 
/// TODO:
/// * Add tests for arbitrary implementation of IDatabase, DONE (more or less...)
/// * Add some other implementation of IDatabase, e.g. based on Excel
/// * Add subtasks
/// 
/// </summary>
namespace TODOHandler
{
    public class ToDoHandler
    {
        private readonly TextWriter consoleWriter;
        private readonly IDatabase database;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database">Any working implementation of the IDatabase interface</param>
        /// <param name="consoleWriter">TextWriter from the console (for output to user)</param>
        public ToDoHandler(IDatabase database, TextWriter consoleWriter = null)
        {
            this.database = database;
            this.consoleWriter = consoleWriter;
        }

        public void Handle(string[] args)
        {
            if(args.Length == 0)
            {
                consoleWriter?.Write("Please provide arguments. Usage examples:\ntodo.exe task -t Complete Application -d 2018-04-01\ntodo.exe list -s [All| Incomplete]\n");
                consoleWriter?.Flush();
                return;
            }

            if (args[0] == "task")
            {
                AddTask(args[2], args[4]);
            }
            else if(args[0] == "-c")  // completed task
            {
                CompleteTask(args[1]);
            }
            else
            {
                ReadTasks(ShowAllTasks(args));
            }
            consoleWriter?.Flush();
        }

        private static bool ShowAllTasks(string[] args)
        {
            bool allTasks = true;
            if (args.Length > 1)
            {
                allTasks = args[1] == "All";
            }

            return allTasks;
        }

        private void CompleteTask(string ID)
        {
            bool found = database.CompleteTask(int.Parse(ID));
            string message = found ? "Task completed.\n" : "Error: Task ID not found.\n";
            consoleWriter.Write(message);
        }

        private void ReadTasks(bool allTasks)
        {
            var tasks = database.ReadAllTasks();

            if(!allTasks)
            {
                tasks = tasks.FindAll(task => task.Due != "Complete");
            }

            foreach (var task in tasks)
            {
                string toPrint = "ID: " + task.ID.ToString() + "\nTask: " + task.Description + "\nDue: " + task.Due + "\n";
                consoleWriter?.Write(toPrint);
            }
        }

        private void AddTask(string description, string due)
        {
            database.Add(description, due);
            consoleWriter?.Write("Task added.\n");
        }
    }
}