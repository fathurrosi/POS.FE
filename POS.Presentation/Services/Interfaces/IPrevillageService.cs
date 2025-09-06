using POS.Domain.Entities;
using POS.Presentation.Models;

namespace POS.Presentation.Services.Interfaces
{
    public interface IPrevillageService
    {
        public Task<List<VUserPrevillage>> GetByUsername(string username);
        public Task<List<Previllage>> GetByRole(string role);
    }
}
