using System;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace XMLReaderXDocument
{
    class XMLReaderXDocument
    {
        static void Main()
        {
            string path = "../../catalog.xml";

            //Task5
            Console.WriteLine("Using XMLReader");
            ReadAllSongsXMLReader(path);
            Console.WriteLine();

            //Task6
            Console.WriteLine("Using XDocument");
            ReadAllSongsXDocument(path);
            Console.WriteLine();
        }

        static void ReadAllSongsXMLReader(string path)
        {
            using (var reader=XmlReader.Create(path))
            {
                while (reader.Read())
                {
                    if (reader.NodeType==XmlNodeType.Element)
                    {
                        if (reader.Name=="album")
                        {
                            reader.Read();
                            Console.WriteLine();
                            Console.WriteLine(reader.ReadElementString());
                        }

                        if (reader.Name=="song")
                        {
                            XmlReader subReader = reader.ReadSubtree();
                            while (subReader.Read())
                            {
                                if (subReader.Name=="title")
                                {
                                    Console.WriteLine(subReader.ReadElementString());
                                }
                            }
                        }
                    }
                }
            }
        }

        static void ReadAllSongsXDocument(string path)
        {
            XDocument doc = XDocument.Load(path);

            var albums =
                from album in doc.Descendants("album")
                from song in album.Descendants("song")
                select new 
                { 
                    album=album.Element("name").Value,
                    song=song.Element("title").Value
                };

            Console.WriteLine(string.Join("\n", albums));
        }
    }
}
