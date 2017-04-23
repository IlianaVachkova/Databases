using System;
using System.IO;
using System.Xml;
using System.Text;

namespace XMLFromFile
{
    class CreateXMLFromFile
    {
        static void Main()
        {
            using (var writer=new XmlTextWriter("../../people.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar='\t';
                writer.Indentation = 1;
                writer.WriteStartDocument();
                writer.WriteStartElement("people");
                writer.WriteAttributeString("name", "phonebook");

                using (var reader=new StreamReader("../../people.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string name = reader.ReadLine();
                        string address = reader.ReadLine();
                        string number = reader.ReadLine();
                        WritePersonDetails(writer, name, address, number);
                        reader.ReadLine();
                    }
                }
                writer.WriteEndElement();
            }
        }

        static void WritePersonDetails(XmlWriter writer, string name, string address, string phoneNumber)
        {
            writer.WriteStartElement("person");
            writer.WriteElementString("name", name);
            writer.WriteElementString("address", address);
            writer.WriteElementString("number", phoneNumber);
            writer.WriteEndElement();
        }
    }
}
