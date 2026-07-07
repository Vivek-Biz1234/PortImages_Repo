using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;


namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetINSStatusByIdQueryHandler : IRequestHandler<GetINSStatusByIdQuery, ApiResponse<INSStatusRequestDTO?>>
    {
        private readonly IINSStatusRepository INSStatusRepository;

        public GetINSStatusByIdQueryHandler(IINSStatusRepository statusRepository)
        {
            this.INSStatusRepository = statusRepository;
        }

        public async Task<ApiResponse<INSStatusRequestDTO?>> Handle(GetINSStatusByIdQuery request, CancellationToken cancellationToken)
        {
            return await INSStatusRepository.GetINSStatusByIdAsync(request.Id);
        }
    }
}
