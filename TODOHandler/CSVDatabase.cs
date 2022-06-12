using System.Collections.Generic;
using System.IO;

namespace TODOHandler
{
    public class CSVDatabase : IDatabase
    {
        private readonly Stream stream;

        public CSVDatabase(Stream stream)
        {
            this.stream = stream;
        }

        public void Add(string description, string due)
        {
            int count = CountLines();
            StreamWriter s = new StreamWriter(stream);
            string toAdd = count.ToString() + ";" + description + ";" + due;
            s.WriteLine(toAdd);
            s.Flush();
        }

        public List<Task> ReadAllTasks()
        {
            var tasks = new List<Task>();

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                string[] parts = reader.ReadLine().Split(';');

                Task task = new Task
                {
                    ID = int.Parse(parts[0]),
                    Description = parts[1],
                    Due = parts[2]
                };

                tasks.Add(task);
            }
            reader.Close();
            return tasks;
        }

        private int CountLines()
        {
            int count = 1;
            int value;
            stream.Position = 0;
            do
            {
                value = stream.ReadByte();
                if (value == 13) // new line character
                {
                    count++;
                }
            } while (value != -1);
            return count;
        }

        public bool CompleteTask(int ID)
        {
            bool found = false;

            List<string> allLines = new List<string>();
            stream.Position = 0;
            var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line.StartsWith(ID.ToString()))
                {
                    found = true;

                    string[] parts = line.Split(';');
                    parts[2] = "Complete";
                    line = parts[0] + ";" + parts[1] + ";" + parts[2];
                }
                allLines.Add(line);
            }

            if (found)
            {
                stream.SetLength(0);
                StreamWriter writer = new StreamWriter(stream);
                allLines.ForEach(line => writer.WriteLine(line));
                writer.Flush();
            }

            return found;
        }
    }
}
