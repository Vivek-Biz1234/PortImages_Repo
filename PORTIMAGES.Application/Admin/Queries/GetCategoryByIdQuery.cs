using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetCategoryByIdQuery:IRequest<ApiResponse<CategoryRequestDTO>>
    {
        public int Id { get; set; }
        public GetCategoryByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
