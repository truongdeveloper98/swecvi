using System.Text;
using SWECVI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SWECVI.ApplicationCore.Entities;
using SWECVI.MirthConnectApi.DependencyInjection;
using SWECVI.MirthConnectApi.Middlewares;

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
app.UseMiddleware<ConnectionMiddlewareExtensions>();
app.UseStaticFiles();
app.MapControllers();



app.MapFallbackToFile("index.html");

app.Run();

