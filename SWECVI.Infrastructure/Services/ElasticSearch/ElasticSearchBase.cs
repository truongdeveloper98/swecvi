using Elasticsearch.Net;
using Nest;
using SWECVI.ApplicationCore.Interfaces.Elasticsearch;
using System.Reflection.Metadata;

namespace SWECVI.Infrastructure.Services.ElasticSearch
{
    public class ElasticSearchBase<T> : IElasticSearchBaseService<T> where T : class
    {
        protected string _indexName { get; set; } = default!;

        protected readonly ElasticClient _client;

        public ElasticSearchBase(ElasticClient client)
        {
            _client = client;
        }
      
        public async Task CreateIndexIfNotExists(string indexName)
        {
            if (!_client.Indices.Exists(indexName).Exists)
            {
               var result = await _client.Indices.CreateAsync(indexName, c => c.Map<dynamic>(m => m.AutoMap()));
            }
        }
        public async Task<bool> AddOrUpdateBulk(IEnumerable<T> documents)
        {
            var indexResponse = await _client.BulkAsync(b => b
                   .Index(_indexName)
                   .UpdateMany(documents, (ud, d) => ud.Doc(d).DocAsUpsert(true))
               );
            return indexResponse.IsValid;
        }
        public async Task<bool> AddOrUpdate(T document)
        {
            var indexResponse = await _client.IndexAsync(document, idx => idx.Index(_indexName)
                                                                             .OpType(OpType.Index));
            return indexResponse.IsValid;
        }
        public async Task<T> Get(string key)
        {
            var response = await _client.GetAsync<T>(key, g => g.Index(_indexName));
            return response.Source;
        }
        public async Task<List<T>?> GetAll()
        {
            var countItem = _client.Count<T>(c => c.Index(_indexName)).Count;

            var searchResponse = await _client.SearchAsync<T>(s => s.Index(_indexName)
                                                                    .Query(q => q.MatchAll())
                                                                    .Size((int?)countItem));
            return searchResponse.IsValid ? searchResponse.Documents.ToList() : default;
        }

        public async Task<List<T>?> Query(QueryContainer query, SortDescriptor<T>? sort = null, ISourceFilter? fields = null,
                                                          IAggregationContainer? aggs = null, int from = 0, int size = 10)
        {

            var searchDescription = new SearchDescriptor<T>()
                             .Index(_indexName)
                             .From(from)
                             .Size(size);

            if (query != null)
            {
                searchDescription = searchDescription.Query(q => query);
            }
            if (sort != null)
            {
                searchDescription = searchDescription.Sort(s => sort);
            }
            if (fields != null)
            {
                searchDescription = searchDescription.Source(s => fields);
            }
            if (aggs != null)
            {
                searchDescription = searchDescription.Aggregations(a => aggs);
            }

            var searchResponse = await _client.SearchAsync<T>(searchDescription);

            return searchResponse.IsValid ? searchResponse.Documents.ToList() : default;
        }

        public async Task<bool> Remove(string key)
        {
            var response = await _client.DeleteAsync<T>(key, d => d.Index(_indexName));
            return response.IsValid;
        }
        public async Task<long> RemoveAll()
        {
            var response = await _client.DeleteByQueryAsync<T>(d => d.Index(_indexName).Query(q => q.MatchAll()));
            return response.Deleted;
        }

        public void SetValueInElasticSearch(string indexName)
        {
            _indexName = indexName;
        }

        public int GetCountItem()
        {
            var countResponse = _client.Count<T>(c => c.Index(_indexName));

            return (int)countResponse.Count;
        }
    }
}
