using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetTerminalByIdQuery:IRequest<ApiResponse<TerminalRequestDTO?>?>
    {
        public int Id { get; set; }
        public GetTerminalByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
