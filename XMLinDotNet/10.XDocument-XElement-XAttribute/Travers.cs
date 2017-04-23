using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

class Travers
{
    static void Main()
    {
        var root = new DirectoryInfo("C:\\xampp");
        var rootElement = new XElement("directories", new XAttribute("attrRoot", "attributeName"));

        TraverseDirDFS(root, rootElement);

        rootElement.Save("../../directories.xml");

        Console.WriteLine("File ready. Look up in directories.xml");
    }

    static void TraverseDirDFS(DirectoryInfo root, XElement rootElement)
    {
        var files = root.GetFiles();
        var directories = root.GetDirectories();

        if (files.Length == 0 && directories.Length == 0)
        {
            var dir = new XElement("dir", root.Name);
            rootElement.Add(dir);
        }
        else
        {
            var dirElement = new XElement("dir");
            dirElement.SetAttributeValue("name", root.Name);
            rootElement.Add(dirElement);

            foreach (var file in files)
            {
                string fullName = file.Name;
                int dotIndex = fullName.LastIndexOf('.');
                if (dotIndex > 0)
                {
                    string name = fullName.Substring(0, dotIndex);
                    string extension = fullName.Substring(dotIndex + 1);

                    var attribute = new XAttribute("type", extension);
                    var fileElement = new XElement("file", attribute, name);
                    dirElement.Add(fileElement);
                }
                else
                {
                    var fileElement = new XElement("file", file.Name);
                    dirElement.Add(fileElement);
                }
            }

            foreach (var dir in directories)
            {
                TraverseDirDFS(dir, dirElement);
            }
        }
    }
}