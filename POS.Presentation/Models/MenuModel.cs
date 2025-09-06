using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entities;
using POS.Domain.Models.Result;

namespace POS.Presentation.Models
{
    public class MenuModel : DetailResult<Menu>
    {
        public MenuModel() {
            this.Item = new Menu();
            this.IsDisabled = true;
        }
        public MenuModel(Menu item)
        {
            this.Item = item;
            this.IsDisabled = item == null;

        }

        

        public IEnumerable<SelectListItem>? ParentList { get; set; }

    }
}
