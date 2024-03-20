using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IStudyService
    {
        Task<ViewModels.ElasticSearch.StudyParameterElasticsearchModel> GetStudy(int id);
        Task<bool> Create(HospitalStudyViewModel model);
        Task<ExamsDto.ExamReport> GetParametersByExam(int hospitalId, int examId);
    }
}
