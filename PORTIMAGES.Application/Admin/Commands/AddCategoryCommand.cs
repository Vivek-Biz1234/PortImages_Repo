using MediatR;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Commands
{
    public class AddCategoryCommand :IRequest<ApiResponse<object>>
    {
   
        public string? CategoryName { get; set; }
        public string? Titletag { get; set; }
        public string? KeywordTag { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        
    }
}
