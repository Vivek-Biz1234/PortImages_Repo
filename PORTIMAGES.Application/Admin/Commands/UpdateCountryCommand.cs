using MediatR;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Commands
{
    public  class UpdateCountryCommand:IRequest<ApiResponse<object>>
    {
        public long ID { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }
}
