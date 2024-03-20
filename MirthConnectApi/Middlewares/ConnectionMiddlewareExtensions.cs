using Microsoft.EntityFrameworkCore;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.Infrastructure.Data;
using System.Security.Claims;

namespace SWECVI.MirthConnectApi.Middlewares
{
    public class ConnectionMiddlewareExtensions
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ConnectionMiddlewareExtensions> _logger;


        public ConnectionMiddlewareExtensions(
            RequestDelegate next,
            ILogger<ConnectionMiddlewareExtensions> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, ManagerHospitalDbContext dbContext, ICacheService cacheService)
        {
                cacheService.GetManufacturerDicomParameters();
                cacheService.GetDicomTags();
                var headerResult = httpContext.Request.Headers["HospitalId"];

                int hospitalId = dbContext.Hospitals.FirstOrDefault(x => x.Name == "SuperAdmin").Id;

                if(!string.IsNullOrEmpty(headerResult))
                {
                   hospitalId = Convert.ToInt32(headerResult);
                }
                else
                {
                    var id = httpContext.User.FindFirst(ClaimTypes.Name);

                    if (id != null)
                    {
                        hospitalId = Convert.ToInt32(id.Value);
                    }
                }                
                
                
                if(hospitalId == default)
                {
                    await _next.Invoke(httpContext);
                }
               else{
                var hospitalByName = await dbContext.Hospitals.FirstOrDefaultAsync(x=>x.Id == hospitalId);

                string connectionString = null;

                if (hospitalByName != default)
                {
                    connectionString = hospitalByName.ConnectionString;
                }


                if (connectionString != null)
                {
                    dbContext.Database.GetDbConnection().ConnectionString = connectionString;

                    await _next.Invoke(httpContext);
                }
                else
                {
                    string error = $"Hospital doesn't exist.";

                    _logger.LogError(error);

                    httpContext.Response.StatusCode = 401; //Unauthorized    
                    await httpContext.Response.WriteAsync(error);
                }
            }
        }

    }
}