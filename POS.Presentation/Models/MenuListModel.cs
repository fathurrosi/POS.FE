using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entities;
using POS.Domain.Models.Result;
using POS.Shared;

namespace POS.Presentation.Models
{
    public class MenuListModel : PagingResult<Usp_GetMenuPagingResult>
    {
        public MenuListModel() { }
        public MenuListModel(PagingResult<Usp_GetMenuPagingResult> pagingResult)
        {
            // Copy properties from pagingResult
            this.Items = pagingResult.Items;
            this.PageIndex = pagingResult.PageIndex;
            this.PageSize = pagingResult.PageSize;
            this.TotalCount = pagingResult.TotalCount;
        }
        public string? SearchText { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
