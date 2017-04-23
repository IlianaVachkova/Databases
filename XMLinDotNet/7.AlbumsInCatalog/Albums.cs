using System;
using System.Xml;
using System.Text;

namespace AlbumsInCatalog
{
    class Albums
    {
        static void Main()
        {
            var writer = new XmlTextWriter("../../albums.xml", Encoding.UTF8);
            var reader = new XmlTextReader("../../catalog.xml");
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = '\t';
            writer.Indentation = 1;

            writer.WriteStartDocument();
            writer.WriteStartElement("albums");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "album")
                    {
                        reader.Read();
                        string name = reader.ReadElementString();
                        string artist = reader.ReadElementString();
                        WriteAlbumData(writer, name, artist);
                    }
                }
            }

            writer.WriteEndDocument();
            writer.Close();
            reader.Close();

            Console.WriteLine("The albums catalogue is ready. Check in the albums.xml file.");
        }

        static void WriteAlbumData(XmlWriter writer, string name, string artist)
        {
            writer.WriteStartElement("album");
            writer.WriteElementString("name", name);
            writer.WriteElementString("artist", artist);
            writer.WriteEndElement();
        }
    }
}
