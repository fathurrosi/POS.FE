using POS.Domain.Entities;
using POS.Domain.Models.Result;
using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface  IUserService
    {
        Task<List<User>> GetDataAsync();
        Task<User> GetByUsername(string username, string password, bool rememberMe);
        Task<User> GetByUsername(UserModel item);
        Task<User> GetById(string id);
        Task<PagingResult<Usp_GetUserPagingResult>> GetPagingAsync(int pageIndex, int pageSize);
        Task<int> Save(User item);
        Task<int> Delete(string id);
    }
}
