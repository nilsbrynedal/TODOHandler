using System.Collections.Generic;
using System.IO;

namespace TODOHandler
{
    public class ToDoHandler
    {
        private readonly Stream stream;
        private readonly TextWriter consoleWriter;

        public ToDoHandler(Stream stream, TextWriter consoleWriter = null)
        {
            this.stream = stream;
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
                AddTask(args);
            }
            else if(args[0] == "-c")  // completed task
            {
                CompleteTask(args[1]);
            }
            else
            {
                ReadTasks();
            }
            consoleWriter?.Flush();
        }

        private void CompleteTask(string ID)
        {
            bool found = false;

            List<string> allLines = new List<string>();
            stream.Position = 0;
            var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if(line.StartsWith(ID))
                {
                    found = true;

                    string[] parts = line.Split(';');
                    parts[2] = "Complete";
                    line = parts[0] + ";" + parts[1] + ";" + parts[2];
                }
                allLines.Add(line);
            }

            if(found)
            {
                stream.SetLength(0);
                StreamWriter writer = new StreamWriter(stream);
                allLines.ForEach(line => writer.WriteLine(line));
                writer.Flush();

                consoleWriter?.Write("Task completed.\n");
            }
            else
            {
                consoleWriter.Write("Error: Task ID not found.\n");
            }
        }

        private void ReadTasks()
        {
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                string[] parts = reader.ReadLine().Split(';');
                string toPrint = "ID: " + parts[0] + "\nTask: " + parts[1] + "\nDue: " + parts[2] + "\n";
                consoleWriter?.Write(toPrint);
            }
            reader.Close();
        }

        private void AddTask(string[] args)
        {
            int count = CountLines();
            StreamWriter s = new StreamWriter(stream);
            string toAdd = count.ToString() + ";" + args[2] + ";" + args[4];
            s.WriteLine(toAdd);
            s.Flush();

            consoleWriter?.Write("Task added.\n");
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
    }
}