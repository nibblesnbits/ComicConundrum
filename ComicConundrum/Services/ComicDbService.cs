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
        private const string DatbaseName = "comics";
        private const string CollectionName = "collection";
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


        public Task DeleteComicAsync(string id) {
            return _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatbaseName, CollectionName, id), new RequestOptions {
                PartitionKey = new PartitionKey("Comic")
            });
            //var query = _client.CreateDocumentQuery(
            //    UriFactory.CreateDocumentCollectionUri(DatbaseName, CollectionName), new SqlQuerySpec {
            //        QueryText = "SELECT c.id FROM c WHERE c.id = @id",
            //        Parameters = new SqlParameterCollection(new SqlParameter[] { new SqlParameter("@id", id) })
            //    })
            //    .AsEnumerable().SingleOrDefault();
            //if (query != null) {
            //    await _client.DeleteDocumentAsync(query.SelfLink);
            //}
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
