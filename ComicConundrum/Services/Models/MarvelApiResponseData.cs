using System.Collections.Generic;

namespace ComicConundrum.Services.Models {
    public class MarvelApiResponseData<T> {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}