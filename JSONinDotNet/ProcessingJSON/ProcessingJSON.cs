namespace ProcessingJSON_Homework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Xml;
    using System.Xml.Linq;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using POCOEntities;

    class ProcessingJSON
    {
        static void Main()
        {
            //Task1-The RSS feed
            string rssFeedAddress = @"https://www.youtube.com/feeds/videos.xml?channel_id=UCLC-vbm7OWvpbqzXaoAMGGw";
            string videosSaveAddress = @"../../video.xml";

            //Task2-Download the content of the feed programatically
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(rssFeedAddress, videosSaveAddress);

            //Task3-Parse the XML from the feed to JSON
            string videosAsJson = ConvertXmlToJson(videosSaveAddress);

            //Task4-Using LINQ-to-JSON select all the video titles and print the on the console
            PrintAllTitlesToConsole(videosAsJson);

            // Task5-Parse the videos' JSON to POCO
            IEnumerable<Video> videoCollection = GetVideosAsCollection(videosAsJson);

            // Task6-Using the POCOs create a HTML page that shows all videos from the RSS
            GenerateHTMLFromPOCOs(videoCollection);
        }

        private static void GenerateHTMLFromPOCOs(IEnumerable<Video> videoCollection)
        {
            var doc = new XDocument();
            doc.AddFirst(new XDocumentType("html", null, null, null));

            var html = new XElement("html");
            var head = new XElement("head");

            var meta = new XElement("meta");
            meta.SetAttributeValue("charset", "UTF - 8");
            head.Add(meta);

            var title = new XElement("title", "Teleik videos");
            head.Add(title);

            var styles = new XElement("link");
            styles.SetAttributeValue("rel", "stylesheet");
            styles.SetAttributeValue("href", "videostyles.css");
            head.Add(styles);

            html.Add(head);

            var body = new XElement("body");

            foreach (var video in videoCollection)
            {
                var videoDiv = new XElement("div", new XElement("p", video.Title));
                videoDiv.SetAttributeValue("class", "container");

                var iframe = new XElement("iframe", string.Empty);
                iframe.SetAttributeValue("frameborder", "0");
                iframe.SetAttributeValue("src", @"https://www.youtube.com/embed/" + video.VideoId);
                videoDiv.Add(iframe);

                var link = new XElement("a", "Link to the video on youtube");
                link.SetAttributeValue("href", video.Link);

                videoDiv.Add(link);
                body.Add(videoDiv);
            }

            html.Add(body);
            doc.Add(html);
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            settings.Indent = true;

            var writter = XmlWriter.Create("../../index.html", settings);
            doc.Save(writter);
            writter.Close();

            Console.WriteLine("You can find the generated index.html in the main directory");
        }


        private static string ConvertXmlToJson(string videosSaveAddress)
        {
            XmlDocument videos = new XmlDocument();
            videos.Load(videosSaveAddress);

            return JsonConvert.SerializeXmlNode(videos);
        }

        private static void PrintAllTitlesToConsole(string videosAsJson)
        {
            JObject videosAsJObject = JObject.Parse(videosAsJson);

            var videoTitles = videosAsJObject["feed"]["entry"].Select(i => i["title"]);

            Console.WriteLine("There are {0} video titles", videoTitles.Count());

            int index = 1;
            foreach (var title in videoTitles)
            {
                Console.WriteLine("{0}. {1}", index, title);
                index++;
            }
        }

        private static IEnumerable<Video> GetVideosAsCollection(string videosAsJson)
        {
            var jsonObject = JObject.Parse(videosAsJson);

            return jsonObject["feed"]["entry"]
                   .Select(e => JsonConvert.DeserializeObject<Video>(e.ToString()));
        }
    }
}
