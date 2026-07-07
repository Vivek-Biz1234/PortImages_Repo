using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class UpdateUserCommandHandler:IRequestHandler<UpdateUserCommand,ApiResponse<object>>
    {
        private readonly IUserMasterRepository _companyRepository;
        public UpdateUserCommandHandler(IUserMasterRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        public async Task<ApiResponse<object>> Handle(UpdateUserCommand request,CancellationToken cancellationToken)
        {
            var dto = new UserRequestDTO()
            {
                ID=request.ID,
                UserName = request.UserName,
                Email = request.Email,
                Contact = request.Contact,
                ContactPerson = request.ContactPerson,
                Address = request.Address,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy
            };
            return await _companyRepository.UpdateCompanyAsync(dto);
        }
    }
}
