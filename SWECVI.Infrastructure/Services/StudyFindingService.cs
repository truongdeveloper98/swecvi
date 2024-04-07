using SWECVI.ApplicationCore.ViewModels.MirthConnect;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Interfaces.Repositories;

namespace SWECVI.Infrastructure.Services
{
    public class StudyFindingService : IStudyFindingService
    {
        private readonly IStudyFindingRepository _studyFindingRepository;
        private readonly IFindingStructureRepository _findingStructureRepository;
        private readonly IStudyRepository _studyRepository;
        private readonly ILogger<StudyFindingService> _logger;


        public StudyFindingService(IStudyFindingRepository studyFindingRepository,
                IStudyRepository studyRepository,
                IFindingStructureRepository findingStructureRepository,
                ILogger<StudyFindingService> logger
            )
        {
            _logger = logger;
            _studyRepository = studyRepository;
            _studyFindingRepository = studyFindingRepository;
            _findingStructureRepository = findingStructureRepository;
        }

        /// <summary>
        /// Function using create new StudyFinding
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true or false</returns>
        public async Task<bool> Create(StudyFindingViewModel model, int Id)
        {
            // get study by study id
            var study = _studyRepository.FirstOrDefault(x => x.Id == model.StudyId);

            // if study is null, throw new exception
            if (study is null)
            {
                _logger.LogError($"Study not exists with Id : {model.StudyId}");

                throw new Exception($"Study not exists with Id : {model.StudyId}");
            }

            // loop data in model to create new StudyFind
            foreach (var findingItem in model.FingdingStudyItems)
            {

                var findingStructure = await _findingStructureRepository.Get(x => x.Id == findingItem.Id);

                // if findingStructure is null, throw new exception
                if (findingStructure is null)
                {
                    _logger.LogError($"FindingStructure not exists with Id : {findingItem.Id} at time : {DateTime.Now}");
                }

                // create new instance studyFinding type of StudyFinding Entity
                var studyFinding = _studyFindingRepository.FirstOrDefault(x => x.StudyId == model.StudyId
                                                                           && x.FindingStructureId == findingItem.Id);
                // if study finding is null, write log
                if (studyFinding is null)
                {
                    var finding = new StudyFinding()
                    {
                        StudyId = model.StudyId,
                        CreatedAt = DateTime.Now,
                        FindingStructureId = findingItem.Id,
                        IsDeleted = false,
                        SelectOptions = findingItem.Value
                    };

                    await _studyFindingRepository.Add(finding);
                    continue;
                }

                // update data if studyFing exists

                studyFinding.StudyId = model.StudyId;
                studyFinding.UpdatedAt = DateTime.Now;
                studyFinding.FindingStructureId = findingItem.Id;
                studyFinding.SelectOptions = findingItem.Value;

                await _studyFindingRepository.Update(studyFinding);

            }

            return true;
        }

        /// <summary>
        /// Get StudyFinding by study
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="Id"></param>
        /// <returns>studyFindings</returns>
        public async Task<List<FingdingStudyItem>> GetStudyFindingByStudyId(int studyId, int Id)
        {
            // build Expression selector return model
            Expression<Func<StudyFinding, FingdingStudyItem>> selectorExpression = m => new FingdingStudyItem
            {
                Id = m.FindingStructureId,
                Value = m.SelectOptions,
                InputLabel = m.FindingStructure.InputLabel,
                InputType = m.FindingStructure.InputType,
                TabName = m.FindingStructure.TabName
            };

            // call function QueryAndSelectAsync return value
            var studyFindings = await _studyFindingRepository.QueryAndSelectAsync(selectorExpression, x => x.StudyId == studyId, null, "FindingStructure");

            return (List<FingdingStudyItem>)studyFindings;
        }


        /// <summary>
        /// Function using create new StudyFinding
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true or false</returns>
        public async Task<bool> Update(StudyFindingViewModel model, int Id)
        {

            // loop studyFinding to update data
            foreach (var item in model.FingdingStudyItems)
            {
                //get studyFinding by StudyId and FindingStuture
                var studyFinding = _studyFindingRepository.FirstOrDefault(x => x.StudyId == model.StudyId
                                                                          && x.FindingStructureId == item.Id);
                // if study finding is null, write log
                if (studyFinding is null)
                {
                    var study = new StudyFinding()
                    {
                        StudyId = model.StudyId,
                        CreatedAt = DateTime.Now,
                        FindingStructureId = item.Id,
                        IsDeleted = false,
                        SelectOptions = item.Value
                    };

                    await _studyFindingRepository.Add(study);
                    continue;
                }

                // update data if studyFing exists

                studyFinding.StudyId = model.StudyId;
                studyFinding.UpdatedAt = DateTime.Now;
                studyFinding.FindingStructureId = item.Id;
                studyFinding.SelectOptions = item.Value;

                await _studyFindingRepository.Update(studyFinding);

            }

            return true;
        }
    }
}
