using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetUserByIdQuery:IRequest<ApiResponse<UserRequestDTO?>?>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
