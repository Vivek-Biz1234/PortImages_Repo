using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetTerminalListQueryHandler:IRequestHandler<GetTerminalListQuery,ApiResponse<List<TerminalResponseDTO>>>
    {
        private readonly ITerminalRepository _terminalRepository;
        public GetTerminalListQueryHandler(ITerminalRepository terminalRepository)
        {
            this._terminalRepository = terminalRepository;
        }

        public async Task<ApiResponse<List<TerminalResponseDTO>>> Handle(GetTerminalListQuery request,CancellationToken cancellationToken)
        {
            return await _terminalRepository.GetTerminalListAsync();
        }
    }
}
