using System;
using System.IO;
using System.Xml;
using System.Text;

class Travers
{
    static void Main()
    {
        var root = new DirectoryInfo("C:\\xampp");
        var writer = new XmlTextWriter("../../directories.xml", Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
        writer.IndentChar = '\t';
        writer.Indentation = 1;
        writer.WriteStartDocument();

        TraverseDirDFS(root, writer);

        writer.WriteEndDocument();
        writer.Close();
    }

    static void TraverseDirDFS(DirectoryInfo root, XmlWriter writer)
    {
        var files = root.GetFiles();
        var directories = root.GetDirectories();

        if (files.Length == 0 && directories.Length == 0)
        {
            writer.WriteElementString("dir", root.Name);
        }
        else
        {
            writer.WriteStartElement("dir");
            writer.WriteAttributeString("name", root.Name);

            foreach (var file in files)
            {
                string fullName = file.Name;
                int dotIndex = fullName.LastIndexOf('.');
                if (dotIndex > 0)
                {
                    string name = fullName.Substring(0, dotIndex);
                    string extension = fullName.Substring(dotIndex + 1);
                    writer.WriteStartElement("file");
                    writer.WriteAttributeString("type", extension);
                    writer.WriteString(name);
                    writer.WriteEndElement();
                }
                else
                {
                    writer.WriteElementString("file", file.Name);
                }
            }

            foreach (var dir in directories)
            {
                TraverseDirDFS(dir, writer);
            }
            writer.WriteEndElement();
        }
    }
}