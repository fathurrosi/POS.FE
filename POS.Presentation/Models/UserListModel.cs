using POS.Domain.Entities;
using POS.Domain.Models.Result;

namespace POS.Presentation.Models
{
    public class UserListModel : PagingResult<Usp_GetUserPagingResult>
    {
        public UserListModel() { }
        public UserListModel(PagingResult<Usp_GetUserPagingResult> pagingResult)
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
