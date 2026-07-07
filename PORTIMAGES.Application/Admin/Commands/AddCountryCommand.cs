using MediatR;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Commands
{
    public class AddCountryCommand :IRequest<ApiResponse<object>>
    {
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
