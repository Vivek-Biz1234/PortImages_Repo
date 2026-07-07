using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class DeleteTerminalCommandHandler:IRequestHandler<DeleteTerminalCommand,ApiResponse<object>>
    {
        private readonly ITerminalRepository _terminalRepository;
        public DeleteTerminalCommandHandler(ITerminalRepository terminalRepository)
        {
            this._terminalRepository = terminalRepository;
        }

        public async Task<ApiResponse<object>> Handle(DeleteTerminalCommand request,CancellationToken cancellationToken)
        {
            return await _terminalRepository.DeleteTerminalAsync(request.ID, request.DeletedBy);
        }
    }
}
