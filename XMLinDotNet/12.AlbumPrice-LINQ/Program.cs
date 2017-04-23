using System;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        XDocument doc = XDocument.Load("../../catalog.xml");
        var albumList =
            from album in doc.Descendants("album")
            where (int.Parse(album.Element("year").Value) <= DateTime.Now.AddYears(-5).Year)
            select new
            {
                Name = album.Element("name").Value,
                Price = album.Element("price").Value
            };

        foreach (var album in albumList)
        {
            Console.WriteLine("Album: {0}, Price: {1}",
                album.Name, album.Price);
        }
    }
}