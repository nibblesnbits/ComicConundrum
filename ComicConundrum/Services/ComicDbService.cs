using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicConundrum.Services.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace ComicConundrum.Services {
    public class ComicDbService : IDisposable {
        private readonly DocumentClient _client;
        private const string DatabaseName = "comics";
        private const string CollectionName = "collection";
        private readonly Uri _collectionUri =
            UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);

        public ComicDbService(DocumentClient client) {
            _client = client;
            _client.CreateDatabaseIfNotExistsAsync(new Database() { Id = DatabaseName });
            _client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(DatabaseName),
                new DocumentCollection { Id = CollectionName });
        }

        public async Task<ComicListing> GetComicById(string id) {
            var comic = await _client.ReadDocumentAsync<ComicListing>(
                UriFactory.CreateDocumentUri(DatabaseName, CollectionName, id));
            return comic;
        }

        public async Task SaveComicAsync(ComicListing comic) {
            await _client.CreateDocumentAsync(_collectionUri, comic);
        }


        public Task DeleteComicAsync(string id) {
            return _client.DeleteDocumentAsync(
                UriFactory.CreateDocumentUri(DatabaseName, CollectionName, id));
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
