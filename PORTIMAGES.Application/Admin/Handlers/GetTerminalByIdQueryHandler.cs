using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetTerminalByIdQueryHandler : IRequestHandler<GetTerminalByIdQuery, ApiResponse<TerminalRequestDTO?>>
    {
        private readonly ITerminalRepository _terminalRepository;
        public GetTerminalByIdQueryHandler(ITerminalRepository terminalRepository)
        {
            _terminalRepository = terminalRepository;
        } 
        public async Task<ApiResponse<TerminalRequestDTO?>> Handle(GetTerminalByIdQuery request, CancellationToken cancellationToken)
        {
            return await _terminalRepository.GetTerminalByIdAsync(request.Id);
        }
    }
}
