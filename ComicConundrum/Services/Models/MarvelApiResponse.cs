namespace ComicConundrum.Services.Models {
    public class MarvelApiResponse<T> {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string attributionHtml { get; set; }
        public string Etag { get; set; }
        public MarvelApiResponseData<T> Data { get; set; }
    }
}
