using Newtonsoft.Json;
using System.Collections.Generic;

namespace ComicConundrum.Services.Models {
    public class ComicListing {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("digitalId")]
        public int? DigitalId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("issueNumber")]
        public int? IssueNumber { get; set; }
        [JsonProperty("variantDescription")]
        public string VariantDescription { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("isbn")]
        public string Isbn { get; set; }
        [JsonProperty("upc")]
        public string Upc { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
        [JsonProperty("resourceUri")]
        public string ResourceUri { get; set; }
        [JsonProperty("urls")]
        public IEnumerable<ComicUrl> Urls { get; set; }
        [JsonProperty("images")]
        public IEnumerable<ComicImage> Images { get; set; }
        [JsonProperty("thumbnail")]
        public ComicImage Thumbnail { get; set; }
    }

    public class ComicUrl {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class ComicImage {
        [JsonProperty("extension")]
        public string Extension { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
