using WorkTimePanelFull.Application.DTOs;

namespace WorkTimePanelFull.Application.Services
{
    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(UserLoginDto dto);
    }
}
