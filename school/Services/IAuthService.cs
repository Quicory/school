using School_Data.DTOs;
using School_Data.Helpers;
using School_Data.Models;

namespace School_API.Services
{
    public interface IAuthService
    {
        Task<APIResponse> Registeration(RegistrationModel model, string role);
        Task<APIResponse> Login(LoginModel model);
        Task<APIResponse> ChangePassword(UserChangePasswordDTO model);
    }
}
