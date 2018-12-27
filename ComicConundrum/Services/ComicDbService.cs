using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicConundrum.Services.Models;
using Microsoft.Azure.Documents.Client;

namespace ComicConundrum.Services {
    public class ComicDbService : IDisposable {
        private readonly DocumentClient _client;
        private const string DatbaseName = "comics";
        private const string CollectionName = "owned";
        private readonly Uri _collectionUri =
            UriFactory.CreateDocumentCollectionUri(DatbaseName, CollectionName);

        public ComicDbService(DocumentClient client) {
            _client = client;
        }

        public async Task<ComicListing> GetComicById(string id) {
            var comic = await _client.ReadDocumentAsync<ComicListing>(
                UriFactory.CreateDocumentUri(DatbaseName, CollectionName, id));
            return comic;
        }

        public async Task SaveComicAsync(ComicListing comic) {
            await _client.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(DatbaseName, CollectionName), comic);
        }


        public async Task DeleteComicAsync(int id) {
            await _client.DeleteDocumentAsync(
                UriFactory.CreateDocumentUri(DatbaseName, CollectionName, id.ToString()));
        }

        public IEnumerable<ComicListing> GetAllComics() {
            var lines = _client.CreateDocumentQuery<ComicListing>(_collectionUri)
                .AsEnumerable();
            return lines;
        }

        #region Disposal
        private bool _disposed = false;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (_disposed) {
                return;
            }

            if (disposing) {
                _client.Dispose();
            }
            _disposed = true;
        }
        #endregion
    }
}
