using ComicConundrum.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ComicConundrum.Services {
    public class MarvelComicsApi {
        private const string ClientName = "marvelApi";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _publicKey;
        private readonly string _privateKey;

        public MarvelComicsApi(IHttpClientFactory httpClientFactory, string publicKey, string privateKey) {
            _httpClientFactory = httpClientFactory;
            _publicKey = publicKey;
            _privateKey = privateKey;
        }

        public async Task<IEnumerable<ComicListing>> SearchByTitleAsync(string title) {
            var client = _httpClientFactory.CreateClient(ClientName);
            var parameters = new Dictionary<string, string> {
                { "format", "comic" },
                { "formatType", "comic" },
                { "noVariants", "true" },
                { "orderBy", "title" },
                { "limit", "10" },
                { "title", title },
            };
            var queryString = CreateRequestParams(parameters);
            var response = await client.GetAsync($"comics?{queryString}");
            if (!response.IsSuccessStatusCode) {
                throw new MarvelApiException(await response.Content.ReadAsAsync<MarvelApiError>());
            }

            var result = await response.Content.ReadAsAsync<MarvelApiResponse<ComicListing>>();
            return result.Data.Results;
        }

        public async Task<ComicListing> GetById(int id) {
            var client = _httpClientFactory.CreateClient(ClientName);
            var queryString = CreateRequestParams();
            var response = await client.GetAsync($"comics/{id}?{queryString}");
            if (!response.IsSuccessStatusCode) {
                throw new MarvelApiException(await response.Content.ReadAsAsync<MarvelApiError>());
            }
            var result = await response.Content.ReadAsAsync<MarvelApiResponse<ComicListing>>();
            return result.Data.Results.FirstOrDefault();
        }

        private string CreateRequestParams(IEnumerable<KeyValuePair<string,string>> parameters = null) {
            var ts = DateTime.UtcNow.ToString("o");
            var hash = CalculateMD5Hash(ts + _privateKey + _publicKey).ToLower();
            return string.Join("&",
                (parameters ?? new KeyValuePair<string, string>[0]).Select(p => $"{p.Key}={p.Value}")
                .Union(new Dictionary<string, string> {
                    { "apikey", _publicKey },
                    { "hash", hash },
                    { "ts", ts },
                }.Select(p => $"{p.Key}={p.Value}")));
        }

        private string CalculateMD5Hash(string input) {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();

            for (var i = 0; i < hash.Length; i++) {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
