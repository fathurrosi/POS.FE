using POS.Domain.Entities;
using POS.Domain.Models.Response;
using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse< User>> GetTokenFromApiAsync(UserModel item);
    }
}
