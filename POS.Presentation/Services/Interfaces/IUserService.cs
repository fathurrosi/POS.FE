using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface  IUserService
    {
        Task<List<UserModel>> GetDataAsync();
        Task<UserModel> GetByUsername(string username, string password, bool rememberMe);
        Task<UserModel> GetByUsername(UserModel item);
        Task<UserModel> GetById(string id);
    }
}
