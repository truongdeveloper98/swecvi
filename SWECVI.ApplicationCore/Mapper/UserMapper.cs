using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.ViewModels;
namespace SWECVI.ApplicationCore.Mapper
{
    public static class UserMapper
    {
        public static User MapToUser(this UserInformationDto model)
        {
            User user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FullName = model.FirstName + " " + model.LastName,
                IsActive = model.IsActive,
            };
            return user;
        }

        public static AppUser MapToAppUser(this UserInformationDto userModel)
        {
            AppUser appUser = new AppUser
            {
                UserName = userModel.Email,
                Email = userModel.Email,
                EmailConfirmed = true,
                PhoneNumber = userModel.PhoneNumber
            };
            return appUser;
        }

        public static UserInformationDto MapToUserInformationDto(this User user)
        {
            UserInformationDto userInformation = new UserInformationDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Identity.Email,
                PhoneNumber = user.Identity.PhoneNumber,
                IsActive = user.IsActive,
                IndexDepartment = user.IndexDepartment,
            };
            return userInformation;
        }

    }
}