using POS.Domain.Entities;
using POS.Domain.Models.Request;
using POS.Domain.Models.Response;

namespace POS.Presentation.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task<LoginResponse< User>> LoginAsync(LoginRequest item);
    }
}
