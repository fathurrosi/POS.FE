using POS.Domain.Entities;
using POS.Domain.Models.Result;

namespace POS.Presentation.Models
{
    public class RoleModel : DetailResult<Role>
    {
        public RoleModel()
        {
            this.Item = new Role();
            this.IsDisabled = true;
        }
        public RoleModel(Role item)
        {
            this.Item = item;
            this.IsDisabled = item == null;

        }
    }
}
