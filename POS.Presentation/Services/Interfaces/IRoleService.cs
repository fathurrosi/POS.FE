using POS.Domain.Entities;
using POS.Domain.Models.Result;
using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetByUsername(string username);
        Task<List<Role>> GetDataAsync();
        Task<PagingResult<Usp_GetRolePagingResult>> GetPagingAsync(int pageIndex, int pageSize);

        Task<Role> GetDataByIdAsync(int id);
        Task<int> Save(Role item);

        Task<int> Delete(int id);
    }
}
