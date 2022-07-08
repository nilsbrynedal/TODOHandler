using System.Collections.Generic;

namespace TODOHandler
{
    public class Task
    {
        private readonly List<Task> subtasks = new List<Task>();

        public int ID { get; set; }
        public string Description { get; set; }
        public string Due { get; set; }

        public string Print() => Print(false);

        private string Print(bool asSubtask)
        {
            string taskString = asSubtask ? "\nSubtask: " : "\nTask: ";
            string toReturn = "ID: " + ID.ToString() + taskString + Description + "\nDue: " + Due + "\n";
            if (subtasks.Count != 0)
            {
                toReturn += "< Subtasks >\n";

                subtasks.ForEach(subtask => toReturn += subtask.Print(true));                
            }
            return toReturn;
        }

        public void AddSubTask(Task task) => subtasks.Add(task);
    }
}
