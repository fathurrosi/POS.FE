using POS.Domain.Entities;
using POS.Domain.Models.Result;
using POS.Presentation.Models;
using POS.Shared;

namespace POS.Presentation.Services.Interfaces
{
    public interface IMenuService
    {
        Task<Menu> GetDataByIdAsync(int id);
        Task<List<Menu>> GetDataAsync();
        Task<List<Menu>> GetDataByUsernameAsync(string username);
        Task<PagingResult<Usp_GetMenuPagingResult>> GetPagingAsync(int pageIndex, int pageSize);
        Task<int> Save(Menu item);

        Task<int> Delete(int id);
    }
}
