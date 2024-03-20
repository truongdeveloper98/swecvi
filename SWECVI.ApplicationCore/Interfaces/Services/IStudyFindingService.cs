using SWECVI.ApplicationCore.ViewModels.MirthConnect;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IStudyFindingService
    {
        Task<bool> Create(StudyFindingViewModel model, int hospitalId);
        Task<bool> Update(StudyFindingViewModel model, int hospitalId);
        Task<List<FingdingStudyItem>> GetStudyFindingByStudyId(int studyId, int hospitalId);
    }
}
