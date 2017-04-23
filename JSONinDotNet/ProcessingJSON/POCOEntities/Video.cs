namespace ProcessingJSON_Homework.POCOEntities
{
    using Newtonsoft.Json;

    public class Video
    {
        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("link")]
        public virtual Link Link { get; set; }

        [JsonProperty("yt:videoId")]
        public virtual string VideoId { get; set; }
    }
}