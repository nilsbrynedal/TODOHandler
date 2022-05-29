using System;
using System.IO;

namespace TODOHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "data.csv";

            FileStream stream;

            if(!File.Exists(path))
            {
                stream = File.Create(path);
            }
            else
            {
                stream = File.Open(path, FileMode.Open);
            }

            ToDoHandler handler = new ToDoHandler(new CSVDatabase(stream), Console.Out);
            handler.Handle(args);
        }
    }
}
