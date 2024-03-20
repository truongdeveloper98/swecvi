using SWECVI.ApplicationCore.Constants;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using DicomTags = SWECVI.ApplicationCore.Entities.DicomTags;

namespace SWECVI.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IParameterSettingRepository _parameterSettingRepository;

        private readonly IReferenceRepository _referenceRepository;

        private readonly IDicomTagRepository _superAdminDicomTagRepository;

        private readonly IManufacturerDicomParametersRepository _superAdminManufacturerDicomParametersRepository;
        private readonly IAssessmentRepository _assessmentRepository;

        private readonly IValveRepository _valveRepository;

        private readonly ICacheProvider _cacheManager;

        private static object _lock = new object();

        public CacheService(ICacheProvider cacheManager, IParameterSettingRepository parameterSettingRepository,
            IDicomTagRepository superAdminDicomTagRepository,
            IAssessmentRepository assessmentRepository,
            IManufacturerDicomParametersRepository superAdminManufacturerDicomParametersRepository,
            IValveRepository valveRepository, IReferenceRepository referenceRepository)
        {
            _cacheManager = cacheManager;
            _parameterSettingRepository = parameterSettingRepository;
            _valveRepository = valveRepository;
            _referenceRepository = referenceRepository;
            _superAdminDicomTagRepository = superAdminDicomTagRepository;
            _superAdminManufacturerDicomParametersRepository = superAdminManufacturerDicomParametersRepository;
            _assessmentRepository = assessmentRepository;
        }

        public IEnumerable<ParameterSetting> GetParameterSettings()
        {
            var results = _cacheManager.GetCache<IEnumerable<ParameterSetting>>(CacheKeys.Parameters);
            if (results == null)
            {
                lock (_lock)
                {
                    var parameters = _parameterSettingRepository.Get().Result;
                    _cacheManager.PutToCache(CacheKeys.Parameters, parameters);
                }
                results = _cacheManager.GetCache<IEnumerable<ParameterSetting>>(CacheKeys.Parameters);
            }
            return results;
        }


        public IEnumerable<DicomTags> GetDicomTags()
        {
            var results = _cacheManager.GetCache<IEnumerable<DicomTags>>(CacheKeys.SuperAdminDicomTag);

            if (results == null)
            {
                lock (_lock)
                {
                    var dicomTags = _superAdminDicomTagRepository.Get().Result;

                    _cacheManager.PutToCache(CacheKeys.SuperAdminDicomTag, dicomTags);
                }

                results = _cacheManager.GetCache<IEnumerable<DicomTags>>(CacheKeys.SuperAdminDicomTag);
            }
            return results;
        }

        public IEnumerable<ParameterReference> GetReferences()
        {
            var results = _cacheManager.GetCache<IEnumerable<ParameterReference>>(CacheKeys.References);
            if (results == null)
            {
                lock (_lock)
                {
                    var references = _referenceRepository.Get().Result;
                    _cacheManager.PutToCache(CacheKeys.References, references);
                }
                results = _cacheManager.GetCache<IEnumerable<ParameterReference>>(CacheKeys.References);
            }
            return results;
        }

        public IEnumerable<ValveData> GetValves()
        {
            var results = _cacheManager.GetCache<IEnumerable<ValveData>>(CacheKeys.Valves);
            if (results == null)
            {
                lock (_lock)
                {
                    var valves = _valveRepository.Get().Result;
                    _cacheManager.PutToCache(CacheKeys.Valves, valves);
                }
                results = _cacheManager.GetCache<IEnumerable<ValveData>>(CacheKeys.Valves);
            }
            return results;
        }


        public void RemoveKey(string key)
        {
            _cacheManager.RemoveKey(key);
        }

        public IEnumerable<ManufacturerDicomParameters> GetManufacturerDicomParameters()
        {
            var results = _cacheManager.GetCache<IEnumerable<ManufacturerDicomParameters>>(CacheKeys.ManufacturerDicomParameter);

            if (results == null || results.Count() == 0)
            {
                lock (_lock)
                {
                    var manufacturerDicomParameters = _superAdminManufacturerDicomParametersRepository.Get().Result;

                    _cacheManager.PutToCache(CacheKeys.ManufacturerDicomParameter, manufacturerDicomParameters);
                }

                results = _cacheManager.GetCache<IEnumerable<ManufacturerDicomParameters>>(CacheKeys.ManufacturerDicomParameter);
            }
            return results;
        }

        public IEnumerable<AssessmentTextReference> GetAssessmentText()
        {
            var results = _cacheManager.GetCache<IEnumerable<AssessmentTextReference>>(CacheKeys.AssessmentTextReference);
            if (results == null)
            {
                lock (_lock)
                {
                    var superAdminAssessmentTexts = _assessmentRepository.Get().Result;
                    _cacheManager.PutToCache(CacheKeys.AssessmentTextReference, superAdminAssessmentTexts);
                }
                results = _cacheManager.GetCache<IEnumerable<AssessmentTextReference>>(CacheKeys.AssessmentTextReference);
            }
            return results;
        }
    }
}