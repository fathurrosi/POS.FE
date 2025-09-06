using POS.Domain.Entities;
using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetByUsername(string username);
        Task<List<Role>> GetDataAsync();
    }
}
