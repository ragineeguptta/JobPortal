using JobPortal.Core.Entities;
using JobPortal.UI.ViewModels.Auth;

namespace JobPortal.UI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginViewModel model);
        Task<string?> RegisterAsync(RegisterViewModel model);
    }
}
