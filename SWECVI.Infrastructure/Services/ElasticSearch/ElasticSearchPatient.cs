using Nest;
using SWECVI.ApplicationCore.Interfaces.Elasticsearch;
using SWECVI.ApplicationCore.ViewModels.ElasticSearch;

namespace SWECVI.Infrastructure.Services.ElasticSearch
{
    public class ElasticSearchPatient : ElasticSearchBase<PatientElasticSearchModel>, IPatientElasticSearchService
    {
        public ElasticSearchPatient(ElasticClient client) : base(client)
        {
        }
    }
}
