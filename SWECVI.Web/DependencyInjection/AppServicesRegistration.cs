using SWECVI.ApplicationCore.Interfaces;
using SWECVI.Infrastructure.Repositories;
using SWECVI.ApplicationCore.DomainServices;
using SWECVI.Infrastructure.Services;
using SWECVI.ApplicationCore.Common;
using SWECVI.ApplicationCore.Interfaces.Elasticsearch;
using SWECVI.Infrastructure.Services.ElasticSearch;
using Elasticsearch.Net;
using Nest;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.Interfaces.Services;

namespace SWECVI.Web.DependencyInjection
{
    public static class AppServicesRegistration
    {
        //public static IServiceCollection AddDicomJobService(this IServiceCollection services)
        //{
        //    services.AddSingleton<DicomJobService>();
        //    services.AddHostedService(provider => provider.GetService<DicomJobService>());
        //    return services;
        //}

        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IDataSourceProvider, DataSourceProvider>();
            services.AddScoped(typeof(ApplicationCore.Interfaces.Repositories.IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IStudyRepository, StudyRepository>();
            services.AddScoped<IDicomTagRepository, DicomTagRepository>();
            services.AddScoped<IManufacturerDicomParametersRepository, ManufacturerDicomParametersRepository>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IFindingStructureService, FindingStructureService>();
            services.AddScoped<IStudyFindingService, StudyFindingService>();
            services.AddScoped<IFindingStructureRepository, FindingStructureRepository>();
            services.AddScoped<IStudyFindingRepository, StudyFindingRepository>();
            services.AddScoped<IReferenceRepository, ReferenceRepository>();
            services.AddScoped<IParameterSettingRepository, ParameterSettingRepository>();
            services.AddScoped<IStudyFindingRepository, StudyFindingRepository>();
            services.AddScoped<IValveRepository, ValveRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<ISystemLogRepository, SystemLogRepository>();
            services.AddScoped<IPythonCodeRepository, PythonCodeRepository>();
            services.AddScoped<IPythonDefaultRepository, PythonDefaultRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IAssessmentRepository, AssessmentRepository>();
            services.AddScoped<IParameterReferenceRepository, ParameterReferenceRepository>();
            services.AddScoped<IParameterSettingRepository, ParameterSettingRepository>();

            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IStudyService, StudyService>();
            services.AddScoped<IHospitalService, HospitalService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAssessmentService, AssessmentService>();
            services.AddScoped<IParameterReferenceService, ParameterReferenceService>();
            services.AddScoped<IParameterSettingService, ParameterSettingService>();

            services.AddScoped(typeof(IElasticSearchBaseService<>), typeof(ElasticSearchBase<>));
            services.AddTransient<IPatientElasticSearchService, ElasticSearchPatient>();
            services.AddTransient<IStudyElasticSearchService, ElasticSearchStudy>();
            services.AddTransient<IStudyParameterElasticSearchService, ElasticSearchStudyParameter>();
            services.AddScoped<ISuperAdminParameterService, SuperAdminParameterService>();
            services.AddScoped<IPythonCodeService, PythonCodeService>();



            // for caching
            services.AddScoped<ICacheProvider, CacheProvider>();
            services.AddScoped<ICacheService, CacheService>();

            var connectionSettings = new ConnectionSettings(new Uri("https://localhost:9200"))
                                                .BasicAuthentication("elastic", "etN4r6nKb987kl_l=hm2")
                                                .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
                                                .EnableApiVersioningHeader();

            var client = new ElasticClient(connectionSettings);
            services.AddSingleton(client);
        }
    }
}