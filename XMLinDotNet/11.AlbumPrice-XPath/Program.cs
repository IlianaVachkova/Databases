using System;
using System.Xml;

class Program
{
    static void Main()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load("../../catalog.xml");

        string xPathQuery = "/catalogue/albums/album";
        XmlNodeList albumsList = doc.SelectNodes(xPathQuery);

        foreach (XmlElement album in albumsList)
        {
            int year = int.Parse(album.SelectSingleNode("year").InnerText);
            string price = album.SelectSingleNode("price").InnerText;
            string name = album.SelectSingleNode("name").InnerText;
            if (year <= DateTime.Now.Year - 5)
            {
                Console.WriteLine("Album: {0}, Price: {1}", name, price);
            }
        }


    }
}