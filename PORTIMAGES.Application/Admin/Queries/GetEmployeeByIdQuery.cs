using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetEmployeeByIdQuery:IRequest<ApiResponse<EmployeeMasterRequestDTO?>?>
    {
        public int Id{ get; set; }
        public GetEmployeeByIdQuery(int id)
        {
                this.Id = id;
        }
    }
}
