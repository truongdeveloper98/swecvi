using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Utilities;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.ViewModels.Hospital;
using System.Linq.Expressions;

namespace SWECVI.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IHospitalRepository _hospitalRepository;


        public DepartmentService(IDepartmentRepository departmentRepository,
            IHospitalRepository hospitalRepository)
        {
            _departmentRepository = departmentRepository;
            _hospitalRepository = hospitalRepository;
        }

        public async Task<DepartmentViewModel> GetById(int id)
        {
            var department = await _departmentRepository.Get(id);

            if (department is null)
            {
                throw new Exception($"Department not found with Id : {id}");
            }

            var result = new DepartmentViewModel()
            {
                Name = department.Name,
                Location = department.Location,
                Modality = department.Modality,
                SendingUnit = department.SendingUnit
            };

            return result;
        }

        public async Task<PagedResponseDto<DepartmentViewModel>> GetDepartments(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch)
        {
            Expression<Func<Department, bool>> filter = i => !i.IsDeleted;

            if (!string.IsNullOrEmpty(textSearch))
            {
                Expression<Func<Department, bool>> searchFilter = i => i.Name.Contains(textSearch);

                filter = PredicateBuilder.AndAlso(filter, searchFilter);
            }

            Expression<Func<Department, DepartmentViewModel>> selectorExpression = i => new DepartmentViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Location = i.Location,
                Modality = i.Modality,
                SendingUnit = i.SendingUnit,
            };


            var totalItems = await _departmentRepository.Count(filter);

            var items = await _departmentRepository
                .QueryAndSelectAsync(
                    selector: selectorExpression,
                    filter,
                    orderBy: m => PredicateBuilder.ApplyOrder(m, sortColumnName, sortColumnDirection),
                    "",
                    pageSize,
                    page: currentPage
                );

            return new PagedResponseDto<DepartmentViewModel>()
            {
                Page = currentPage,
                Limit = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = (List<DepartmentViewModel>)items
            };
        }

        public async Task<bool> CreateDepartment(DepartmentViewModel model)
        {
            var department = new Department()
            {
                Name = model.Name,
                Location  = model.Location,
                Modality = model.Modality,
                SendingUnit = model.SendingUnit,
                IsDeleted = false,
                IndexHospital = model.IndexHospital
            };

            await _departmentRepository.Add(department);

            return true;
        }

        public async Task<bool> UpdateDepartment(int id, DepartmentViewModel model)
        {
            var department = await _departmentRepository.Get(id);

            if (department is null)
            {
                throw new Exception($"Department not found with Id : {id}");
            }

            department.Name = model.Name;
            department.Location = model.Location;
            department.Modality = model.Modality;
            department.SendingUnit = model.SendingUnit;
            department.IndexHospital = model.IndexHospital;
            
            await _departmentRepository.Update(department);

            return true;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await _departmentRepository.Get(id);

            if(department is null)
            {
                throw new Exception($"Department dont exists with id {id}");
            }

            await _departmentRepository.Delete(department); 

            return true;
        }
    }
}
