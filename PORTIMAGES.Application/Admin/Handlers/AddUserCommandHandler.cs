
using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class AddUserCommandHandler:IRequestHandler<AddUserCommand,ApiResponse<object>>
    {
        private readonly IUserMasterRepository _companyRepository;
        public AddUserCommandHandler(IUserMasterRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        public async Task<ApiResponse<object>> Handle(AddUserCommand request,CancellationToken cancellationToken)
        {
            var dto = new UserRequestDTO()
            {
                UserName=request.UserName,
                Email=request.Email,
                Contact=request.Contact,
                ContactPerson=request.ContactPerson,
                Address=request.Address,
                Password = request.Password,
                IsActive=request.IsActive,
                CreatedBy=request.CreatedBy
            };
            return await _companyRepository.AddCompanyAsync(dto);
        }
    }
}
