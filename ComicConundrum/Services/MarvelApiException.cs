using System;
using System.Runtime.Serialization;

namespace ComicConundrum.Services {
    [Serializable]
    internal class MarvelApiException : Exception {

        public MarvelApiException(Models.MarvelApiError marvelApiError)
            : base($"API error - {marvelApiError.Code}: {marvelApiError.Message}") { }

        public MarvelApiException(string message) : base(message) {
        }

        public MarvelApiException(string message, Exception innerException) : base(message, innerException) {
        }

        protected MarvelApiException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}