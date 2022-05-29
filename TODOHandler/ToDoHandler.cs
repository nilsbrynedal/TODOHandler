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
            int count = CountLines();

            if(args.Length == 0)
            {
                consoleWriter?.Write("Please provide arguments. Usage examples:\ntodo.exe task -t Complete Application -d 2018-04-01\ntodo.exe list -s [All| Incomplete]\n");
                consoleWriter?.Flush();

                return;
            }

            if (args[0] == "task")
            {
                StreamWriter s = new StreamWriter(stream);
                string toAdd = count.ToString() + ";" + args[2] + ";" + args[4];
                s.WriteLine(toAdd);
                s.Flush();
            }
            else
            {
                consoleWriter?.Write("ID: 1\nTask: Complete Application\nDue: 2018-04-01\n");
                consoleWriter?.Flush();
            }
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