using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IStudyService
    {
        Task<ViewModels.ElasticSearch.StudyParameterElasticsearchModel> GetStudy(int id);
        Task<bool> Create(HospitalStudyViewModel model);
        Task<ExamsDto.ExamReport> GetParametersByExam(int hospitalId, int examId);
        Task<List<ExamsDto.ExamsCount>> GetExamsByPeriod(string? period);
        Task<List<ExamsDto.ExamType>> GetExamTypesByPeriod(string? period);
        Task<List<string>> GetCodeMeanings();
        Task<List<ExamsDto.CodeMeaningChart>> GetTextValueByCodeMeaningCharts(string[] codeMeaningSelect, string? period);
    }
}
