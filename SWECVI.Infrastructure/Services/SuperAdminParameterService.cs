using Microsoft.EntityFrameworkCore;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Services
{
    public class SuperAdminParameterService : ISuperAdminParameterService
    {
        private readonly ManagerHospitalDbContext _superAdminDbContext;

        public SuperAdminParameterService(ManagerHospitalDbContext superAdminDbContext)
        {
            _superAdminDbContext = superAdminDbContext;
        }

        public async Task InsertDataToDB(List<DicomtagParameterViewModel> models)
        {

            var dicomTagNotExists = new List<DicomTags>();

            foreach (var model in models)
            {
                var tagExists = await _superAdminDbContext.DicomTags
                                          .Where(x => x.CSD == model.MeasurementConceptCSD &&
                                                   x.CV == model.MeasurementConceptCV &&
                                                   x.CM.ToLower() == model.MeasurementConceptCM.ToLower())
                                            .FirstOrDefaultAsync();

                if (tagExists == null)
                {
                    _superAdminDbContext.DicomTags.Add(new DicomTags()
                    {
                        CV = model.MeasurementConceptCV,
                        CM = model.MeasurementConceptCM,
                        CSD = model.MeasurementConceptCSD,
                        SNOMED = string.Empty,
                        IndexContextID = 1,
                        IsDeleted = false,
                        DeletedAt = DateTime.MinValue,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    });

                    await _superAdminDbContext.SaveChangesAsync();

                }

            }


        }
    }
}
