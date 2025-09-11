using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.Domain.Entities;
using POS.Domain.Models.Result;

namespace POS.Presentation.Models
{
    public class RoleListModel : PagingResult<Usp_GetRolePagingResult>
    {
        public RoleListModel() { }
        public RoleListModel(PagingResult<Usp_GetRolePagingResult> pagingResult)
        {
            // Copy properties from pagingResult
            this.Items = pagingResult.Items;
            this.PageIndex = pagingResult.PageIndex;
            this.PageSize = pagingResult.PageSize;
            this.TotalCount = pagingResult.TotalCount;
        }
        public string? SearchText { get; set; }
    }
}
