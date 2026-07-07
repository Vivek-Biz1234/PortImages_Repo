using MediatR;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Commands
{
    public class UpdateINSStatusCommand: IRequest<ApiResponse<object>>
    {
        public int ID { get; set; }
        public string StatusName { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
    }
}
