using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SWECVI.ApplicationCore.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using SWECVI.ApplicationCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SWECVI.ApplicationCore.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Services;

namespace SWECVI.ApplicationCore.DomainServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _provider;


        public AuthenticationService(IUserRepository userRepo, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IServiceProvider provider,
        ILogger<AuthenticationService> logger, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _provider = provider;
        }

        public async Task<LoginDto.LoginResult> Login(LoginDto.Login model)
        {
            var appUser = _userManager.Users.FirstOrDefault(r => r.UserName == model.Email || r.Email == model.Email); 
            if (appUser == null)
            {
                throw new Exception("Credentials invalid");
            }

            var result = await _signInManager.PasswordSignInAsync(appUser.UserName, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                string token = await GenerateJwtToken(model.Email,model.HospitalId.ToString(), appUser);
                var user = await _userRepo.Get(u => u.IdentityId == appUser.Id);
                if (user == null)
                {
                    throw new Exception("Credentials invalid");
                }
                if (user.IsActive == false)
                {
                    throw new Exception("User is inactive");
                }

                return new LoginDto.LoginResult(){
                    Token = token
                };
            }
            throw new Exception("Credentials invalid");
        }

        private async Task<string> GenerateJwtToken(string email,string hospitalName, AppUser user, IList<string> userRoles = null)
        {
            if (userRoles == null)
            {
                userRoles = await _userManager.GetRolesAsync(user);
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, hospitalName),
                new Claim(ClaimTypes.Role, userRoles[0].ToString())
            };

            claims.AddRange(userRoles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
