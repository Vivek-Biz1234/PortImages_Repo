using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;  
namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetINSDestinationByIdQuery: IRequest<ApiResponse<INSDestinationRequestDTO?>?>
    {
        public int Id { get; set; }
        public GetINSDestinationByIdQuery(int id)
        {
            this.Id = id;
        }

    }
}
