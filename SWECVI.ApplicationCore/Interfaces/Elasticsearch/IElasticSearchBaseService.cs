using Nest;

namespace SWECVI.ApplicationCore.Interfaces.Elasticsearch
{
    public interface IElasticSearchBaseService<T> where T : class
    {
        Task CreateIndexIfNotExists(string indexName);
        void SetValueInElasticSearch(string indexName);
        Task<bool> AddOrUpdateBulk(IEnumerable<T> documents);
        Task<bool> AddOrUpdate(T document);
        Task<T> Get(string key);
        Task<List<T>?> GetAll();
        int GetCountItem();
        Task<List<T>?> Query(QueryContainer query, SortDescriptor<T> sort, ISourceFilter fields,
                             IAggregationContainer aggs, int from = 0, int size = 10);
        Task<bool> Remove(string key);
        Task<long> RemoveAll();
    }
}
