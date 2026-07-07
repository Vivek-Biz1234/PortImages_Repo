using MediatR;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Commands
{
    public class AddVehicleStatusCommand: IRequest<ApiResponse<object>>
    {
        public string StatusName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
