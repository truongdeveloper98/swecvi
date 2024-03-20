using System.Text;
using SWECVI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SWECVI.Web.DependencyInjection;
using SWECVI.Web.Middlewares;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddCors(option =>
{
    option.AddPolicy("_myAllowSpecificOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


builder.Services.AddDbContext<ManagerHospitalDbContext>(optionsAction =>
{
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Stage")
    {
        var envConnectionString = Environment.GetEnvironmentVariable("ADMIN_CONNECTION_STRING");
        if (String.IsNullOrEmpty(envConnectionString))
        {
            optionsAction.UseSqlServer(configuration.GetConnectionString("SuperAdminConnection"), b => b.MigrationsAssembly("SWECVI.Database"));
        }
        else
        {
            optionsAction.UseSqlServer(envConnectionString, b => b.MigrationsAssembly("SWECVI.Database"));
        }
    }
    else
    {
            optionsAction.UseSqlServer(configuration.GetConnectionString("SuperAdminConnection"), b => b.MigrationsAssembly("SWECVI.Database"));
     }

});




// role, user
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ManagerHospitalDbContext>()
    .AddDefaultTokenProviders();


// Adding Authentication
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

// Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidAudience = configuration["JwtIssuer"],
            ValidIssuer = configuration["JwtIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]))
        };
    });

builder.Services.AddControllers();

builder.Services.ConfigureAppServices();

// builder.Services.AddDicomJobService();

builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("_myAllowSpecificOrigins");
}

app.UseHttpsRedirection();

// Handling exceptions
app.ConfigureExceptionHandler(app.Environment, app.Logger);

// Authentication & Authorization
app.UseAuthentication();
app.UseMiddleware<ConnectionMiddlewareExtensions>();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();



//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<ManagerHospitalDbContext>();
//    dataContext.Database.Migrate();

//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
//    var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
//    var regionRepo = scope.ServiceProvider.GetRequiredService<IRegionRepository>();
//    var hospitalRepo = scope.ServiceProvider.GetRequiredService<IHospitalRepository>();

//    var systemLogRepo = scope.ServiceProvider.GetRequiredService<ISystemLogRepository>();
//    var pythonCodeRepo = scope.ServiceProvider.GetRequiredService<IPythonCodeRepository>();
//    var pythonDefaultRepo = scope.ServiceProvider.GetRequiredService<IPythonDefaultRepository>();

//    DbInitializer.SeedUsers(userManager, roleManager, userRepo, regionRepo, hospitalRepo).Wait();
//    DbInitializer.SeedPythonCode(pythonCodeRepo, pythonDefaultRepo, systemLogRepo).Wait();
//}


app.MapFallbackToFile("index.html");

app.Run();

