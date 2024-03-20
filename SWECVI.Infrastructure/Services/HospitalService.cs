using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Utilities;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.ViewModels.Hospital;
using SWECVI.ApplicationCore.ViewModels.MirthConnect;
using SWECVI.Infrastructure.Data;
using System.Linq.Expressions;

namespace SWECVI.Infrastructure.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _superAdminHospitalRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUserRepository _userRepo;

        public HospitalService(IHospitalRepository superAdminHospitalRepository,
            IServiceScopeFactory serviceScopeFactory,
            UserManager<AppUser> userManager,
            IUserRepository userRepo,
            IDepartmentRepository departmentRepository,
            RoleManager<AppRole> roleManager)
        {
            _superAdminHospitalRepository = superAdminHospitalRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _userManager = userManager;
            _userRepo = userRepo;
            _roleManager = roleManager;
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> CreateHospital(HopsitalViewModel model)
        {
            if (model.IndexDepartment > 0)
            {
                var department = await _departmentRepository.Get(model.IndexDepartment.Value);

                if (department is null)
                {
                    throw new Exception($"Department not found with id {model.IndexDepartment}");
                }
            }

            var hospital = new Hospital()
            {
                Name =model.HospitalName,
                IndexRegion = model.IndexRegion,
                ConnectionString = model.ConnectionString,
                IsDeleted = false
            };

            await _superAdminHospitalRepository.Add(hospital);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<ManagerHospitalDbContext>();
                dataContext.Database.GetDbConnection().ConnectionString = model.ConnectionString;
                dataContext.Database.Migrate();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var hospitalRepo = scope.ServiceProvider.GetRequiredService<IHospitalRepository>();
                var regionRepo = scope.ServiceProvider.GetRequiredService<IRegionRepository>();

                await DbInitializer.SeedUserByHospital(userManager, roleManager, userRepo, model.AdminEmail, model.AdminPassword, model.IndexDepartment,model, hospitalRepo, regionRepo);
            }
           
            return true;
        }

        public async Task<HopsitalViewModel> GetById(int id)
        {
            var hospital = await _superAdminHospitalRepository.Get(id);

            if (hospital is null)
            {
                throw new Exception($"Hospital not found with Id : {id}");
            }

            var result = new HopsitalViewModel()
            {
                Id = hospital.Id,
                HospitalName = hospital.Name,
                ConnectionString = hospital.ConnectionString
            };

            return result;
        }

        public async Task<PagedResponseDto<HopsitalViewModel>> GetHospitals(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch)
        {
            Expression<Func<Hospital, bool>> filter = i => !i.IsDeleted;

            if (!string.IsNullOrEmpty(textSearch))
            {
                Expression<Func<Hospital, bool>> searchFilter = i => i.Name.Contains(textSearch);

                filter = PredicateBuilder.AndAlso(filter, searchFilter);
            }

            Expression<Func<Hospital, HopsitalViewModel>> selectorExpression = i => new HopsitalViewModel
            {
                Id = i.Id,
                HospitalName = i.Name,
                ConnectionString = i.ConnectionString
            };


            var totalItems = await _superAdminHospitalRepository.Count(filter);

            var items = await _superAdminHospitalRepository
                .QueryAndSelectAsync(
                    selector: selectorExpression,
                    filter,
                    orderBy: m => PredicateBuilder.ApplyOrder(m, sortColumnName, sortColumnDirection),
                    "",
                    pageSize,
                    page: currentPage
                );

            return new PagedResponseDto<HopsitalViewModel>()
            {
                Page = currentPage,
                Limit = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = (List<HopsitalViewModel>)items
            };
        }

        public async Task<bool> UpdateHospital(int id,HopsitalViewModel model)
        {

            if (model.IndexDepartment != null)
            {
                var department = await _departmentRepository.Get(model.IndexDepartment.Value);

                if (department is null)
                {
                    throw new Exception($"Department not found with id {model.IndexDepartment}");
                }
            }

            var hospital = await _superAdminHospitalRepository.Get(id);

            if(hospital == default)
            {
                throw new Exception("Hospital dont exists");
            }

            hospital.Name = model.HospitalName;
            hospital.ConnectionString = model.ConnectionString;
            hospital.UpdatedAt = DateTime.Now;
            hospital.IndexDepartment = model.IndexDepartment;

            await  _superAdminHospitalRepository.Update(hospital);

            return true;
        }

        public async Task<List<SuperAdminHospitalViewModel>> GetHospitals()
        {
            Expression<Func<Hospital, bool>> filter = i => !i.IsDeleted;

            Expression<Func<Hospital, SuperAdminHospitalViewModel>> selectorExpression = i => new SuperAdminHospitalViewModel
            {
                Id = i.Id,
                Name = i.Name
            };

            var items = await _superAdminHospitalRepository.QueryAndSelectAsync(selector: selectorExpression,
                filter: filter,
                orderBy: m => PredicateBuilder.ApplyOrder(m, "Name", "ASC")
                );

            return (List<SuperAdminHospitalViewModel>)items;
        }

        public async Task<SuperAdminHospitalViewModel> GetHospitalById(int id)
        {
            var hospital = await _superAdminHospitalRepository.Get(id);

            if (hospital is null)
            {
                throw new Exception($"Hospital not found with Id : {id}");
            }

            var result = new SuperAdminHospitalViewModel()
            {
                Id = hospital.Id,
                Name = hospital.Name,
                ConnectionString = hospital.ConnectionString
            };

            return result;
        }
    }
}
