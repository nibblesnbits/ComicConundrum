using System;

namespace ComicConundrum.Configuration {
    public class MarvelApiOptions {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public Uri BaseUri { get; set; }
    }
}
