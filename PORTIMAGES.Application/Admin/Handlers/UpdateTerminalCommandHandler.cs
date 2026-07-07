using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class UpdateTerminalCommandHandler : IRequestHandler<UpdateTerminalCommand, ApiResponse<object>>
    {
        private readonly ITerminalRepository _terminalRepository;
        public UpdateTerminalCommandHandler(ITerminalRepository terminalRepository)
        {
            this._terminalRepository = terminalRepository;
        }

        public async Task<ApiResponse<object>> Handle(UpdateTerminalCommand request, CancellationToken cancellationToken)
        {
            var dto = new TerminalRequestDTO
            {
                ID = request.ID,
                PortId = request.PortId,
                TerminalName = request.TerminalName,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy,
            };
            return await _terminalRepository.UpdateTerminalAsync(dto);          
        }
    }
}
