using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Queries
{
    public  class GetCountryByIdQuery:IRequest<ApiResponse<CountryRequestDTO>>
    {
        public int Id { get; set; }
        public GetCountryByIdQuery(int id) 
        {
            this.Id = id;
        }
    }
}
