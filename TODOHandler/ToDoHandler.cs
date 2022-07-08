using System.IO;

/// <summary>
/// Test driven implementation of http://www.tddbuddy.com/katas/Todo%20List.pdf
/// 
/// TODO:
/// * Add tests for arbitrary implementation of IDatabase. DONE (more or less...)
/// * Create tests for some other implementation of IDatabase, e.g. based on Excel. DONE (some tests added)
/// * Add some other implementation of IDatabase, e.g. based on Excel, STARTED
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
                if (args[2].Contains("|"))
                {
                    consoleWriter?.Write("Task description contains forbidden character '|'\n");
                }
                else
                {
                    AddTask(args[2], args[4]);
                    consoleWriter?.Write("Task added.\n");
                }
            }
            else if (args[0] == "subtask")
            {
                if (args[2].Contains("|"))
                {
                    consoleWriter?.Write("Task description contains forbidden character '|'\n");
                }
                else
                {
                    int taskID = int.Parse(args[6]);
                    AddSubTask(taskID, args[2], args[4]);
                }
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

        public void AddSubTask(int taskID, string description, string due)
        {
            var tasks = database.ReadAllTasks();
            int ID = tasks.FindIndex(t => t.ID == taskID);

            // check for missing main Task
            if (ID == -1)
            {
                consoleWriter?.Write("Error: Task ID does not exist.\n");
                return;
            }

            // A task with a description starting with "|<id number>|" will become a subtask of the task
            // with that id number

            AddTask("|" + taskID.ToString() + "|" + description, due);
            consoleWriter?.Write("Subtask added.\n");
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
            var subtasks = tasks.FindAll(t => t.Description.StartsWith("|"));
            
            foreach (var subtask in subtasks)
            {
                tasks.Remove(subtask);

                var splits = subtask.Description.Split('|');

                int taskID = int.Parse(splits[1]);
                subtask.Description = splits[2];
                tasks.Find(t => t.ID == taskID).AddSubTask(subtask);
            }

            if (!allTasks)
            {
                tasks = tasks.FindAll(task => task.Due != "Complete");
            }

            foreach (var task in tasks)
            {
                consoleWriter?.Write(task.Print());
            }
        }

        private void AddTask(string description, string due)
        {
            database.Add(description, due);
        }
    }
}