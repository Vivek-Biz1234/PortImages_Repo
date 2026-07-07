using MediatR;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Commands
{
    public class UpdateCategoryCommand:IRequest<ApiResponse<object>>
    
    {
        public int ID { get; set; }
        public string? CategoryName { get; set; }
        public string? Titletag { get; set; }
        public string? KeywordTag { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }

        public int ? UpdatedBy { get;set; }

    }
}
