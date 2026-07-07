using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class DeleteCountryCommandHandler:IRequestHandler<DeleteCountryCommand,ApiResponse<object>>
    {
        private readonly ICountryRepository _countryRepository;
        public  DeleteCountryCommandHandler(ICountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        public async Task<ApiResponse<object>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            return await _countryRepository.DeleteCountryAsync(request.ID, request.DeletedBy);
        }
    }
}
