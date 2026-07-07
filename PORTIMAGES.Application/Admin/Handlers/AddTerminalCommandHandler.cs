using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{ 
    public class AddTerminalCommandHandler : IRequestHandler<AddTerminalCommand, ApiResponse<object>>
    {
        private readonly ITerminalRepository _terminalRepository;
        public AddTerminalCommandHandler(ITerminalRepository terminalRepository)
        {
            this._terminalRepository = terminalRepository;
        }
        public async Task<ApiResponse<object>> Handle(AddTerminalCommand request, CancellationToken cancellationToken)
        {
            var dto = new TerminalRequestDTO
            {
                PortId = request.PortId,
                TerminalName = request.TerminalName,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };
            return await _terminalRepository.AddTerminalAsync(dto);           
        }
    }
}
