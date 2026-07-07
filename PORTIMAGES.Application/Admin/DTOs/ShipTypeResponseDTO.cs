using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.DTOs
{
    public class ShipTypeResponseDTO
    {
        public int ID { get; set; }
        public string? TypeName { get; set; }
        public bool IsActive { get; set; }

        public string? CreatedOn { get; set; }
        public string? UpdatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
