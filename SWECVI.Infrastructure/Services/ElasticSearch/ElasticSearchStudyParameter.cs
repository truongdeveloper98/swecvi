using Nest;
using SWECVI.ApplicationCore.Interfaces.Elasticsearch;
using SWECVI.ApplicationCore.ViewModels.ElasticSearch;

namespace SWECVI.Infrastructure.Services.ElasticSearch
{
    public class ElasticSearchStudyParameter : ElasticSearchBase<StudyParameterElasticsearchModel>, IStudyParameterElasticSearchService
    {
        public ElasticSearchStudyParameter(ElasticClient client) : base(client)
        {
        }
    }
}
