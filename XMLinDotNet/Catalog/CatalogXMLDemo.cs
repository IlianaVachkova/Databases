using System;
using System.Xml;
using System.Collections.Generic;

namespace Catalog
{
    class CatalogXMLDemo
    {
        static IDictionary<string, int> artistAlbums = new Dictionary<string, int>();

        static void Main()
        {
            //Task2-DOM Parser
            XmlDocument doc = new XmlDocument();
            doc.Load("../../catalog.xml");
            XmlNode rootNode = doc.DocumentElement;

            FindArtistsDFS(rootNode);
            PrintArtistsAlbums();

            //Task3-XPath
            string XpathQuery = "catalog/albums/album";
            XmlNodeList artistList = doc.SelectNodes(XpathQuery);
            artistAlbums=new Dictionary<string, int>();

            foreach (XmlNode item in artistList)
            {
                var artist = item.SelectSingleNode("artist").InnerText;
                if (!artistAlbums.ContainsKey(artist))
                {
                    artistAlbums.Add(artist,1);
                }
                else
                {
                    artistAlbums[artist]++;
                }
            }
            PrintArtistsAlbums();

            //Task4-Remove all albums with price greater than $20
            FindPriceInAlbumDFS(rootNode);
            doc.Save("../../itemsNew.xml");

        }

        static void FindArtistsDFS(XmlNode rootNode)
        {
            if (!rootNode.HasChildNodes)
            {
                return;
            }
            else
            {
                if (rootNode.Name == "artist")
                {
                    if (!artistAlbums.ContainsKey(rootNode.InnerText))
                    {
                        artistAlbums.Add(rootNode.InnerText, 1);
                    }
                    else
                    {
                        artistAlbums[rootNode.InnerText]++;
                    }
                }

                foreach (XmlNode node in rootNode)
                {
                    FindArtistsDFS(node);
                }
            }
        }

        private static void PrintArtistsAlbums()
        {
            foreach (var item in artistAlbums)
            {
                Console.WriteLine("Artist: {0}, Albums' count: {1}", item.Key, item.Value);
            }
            Console.WriteLine();
        }

        static void FindPriceInAlbumDFS(XmlNode rootNode)
        {
            if (!rootNode.HasChildNodes)
            {
                return;
            }
            else
            {
                if (rootNode.Name=="album")
                {
                    var childs = rootNode.ChildNodes;
                    foreach (XmlNode child in childs)
                    {
                        if (child.Name=="price")
                        {
                            string priceStr = child.InnerText.TrimStart('$');
                            decimal price = decimal.Parse(priceStr);
                            if (price>20)
                            {
                                rootNode.ParentNode.RemoveChild(rootNode);
                            }
                        }
                    }
                }

                foreach (XmlNode node in rootNode)
                {
                    FindPriceInAlbumDFS(node);
                }
            }
        }
    }
}
