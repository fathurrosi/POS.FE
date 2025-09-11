using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entities;
using POS.Domain.Models.Result;

namespace POS.Presentation.Models
{
    public class UserModel : DetailResult<User>
    {
        public UserModel()
        {
            this.Item = new User();
            this.IsDisabled = true;
        }
        public UserModel(User item)
        {
            this.Item = item;
            this.IsDisabled = item == null;

        }



        public IEnumerable<SelectListItem>? StatusList { get; set; }
    }
}
