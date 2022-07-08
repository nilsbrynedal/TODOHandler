using System.Collections.Generic;
using TODOHandler;
using System.IO;
using System.Xml;
using System.Text;

namespace TODOHandler_Tests
{
    public class XMLDatabase : IDatabase
    {
        private readonly MemoryStream stream;

        public XMLDatabase(MemoryStream stream)
        {
            this.stream = stream;
        }

        private XmlDocument GetDocument()
        {
            XmlDocument document = new XmlDocument();

            try
            {
                stream.Position = 0;
                document.Load(stream);
            }
            catch
            { }

            return document;
        }

        public void Add(string description, string due)
        {
            XmlDocument document = GetDocument();

            int count = document.ChildNodes.Count;

            var element = document.CreateElement("Task");

            element.SetAttribute("ID", (count + 1).ToString());
            element.SetAttribute("Description", description);
            element.SetAttribute("Due", due);

            document.AppendChild(element);

            Save(document);
        }

        private void Save(XmlDocument document)
        {
            stream.Position = 0;
            XmlWriter writer = XmlWriter.Create(stream);
            document.WriteTo(writer);


            var contents = Encoding.ASCII.GetString(stream.ToArray());

            writer.Flush();
        }

        public bool CompleteTask(int ID)
        {
            XmlDocument document = GetDocument();
            bool existed = false;

            for (var child = document.FirstChild; child != null; child = child.NextSibling)
            {
                if(child.Attributes.GetNamedItem("ID").Value == ID.ToString())
                {
                    child.Attributes.GetNamedItem("Due").Value = "Complete";
                    existed = true;
                    break;
                }
            }

            if(existed)
            {
                Save(document);
            }

            return existed;
        }

        public List<Task> ReadAllTasks()
        {
            List<Task> tasks = new List<Task>();
            XmlDocument document = GetDocument();

            for (System.Collections.IEnumerator enumerator = document.ChildNodes.GetEnumerator(); enumerator.MoveNext();)
            {
                tasks.Add(new Task()
                {
                    Description = (enumerator.Current as XmlElement).GetAttribute("Description"),
                    Due = (enumerator.Current as XmlElement).GetAttribute("Due"),
                    ID = int.Parse((enumerator.Current as XmlElement).GetAttribute("ID"))
                });
            }

            return tasks;
        }
    }
}