using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetINSStatusByIdQuery: IRequest<ApiResponse<INSStatusRequestDTO?>?>
    {
        public int Id { get; set; }
        public GetINSStatusByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
