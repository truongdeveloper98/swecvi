using Nest;
using SWECVI.ApplicationCore.Interfaces.Elasticsearch;
using SWECVI.ApplicationCore.ViewModels.ElasticSearch;

namespace SWECVI.Infrastructure.Services.ElasticSearch
{
    public class ElasticSearchStudy : ElasticSearchBase<StudyElasticsearchModel>, IStudyElasticSearchService
    {
        public ElasticSearchStudy(ElasticClient client) : base(client)
        {
        }
    }
}
