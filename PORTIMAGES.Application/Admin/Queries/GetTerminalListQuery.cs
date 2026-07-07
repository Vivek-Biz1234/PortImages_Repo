using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetTerminalListQuery:IRequest<ApiResponse<List<TerminalResponseDTO>>>
    {

    }
}
