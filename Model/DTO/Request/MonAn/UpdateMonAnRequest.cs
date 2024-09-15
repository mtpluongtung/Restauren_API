using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Request.MonAn
{
    public class UpdateMonAnRequest
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public decimal? Gia { get; set; }

        public bool? IsActive { get; set; }

        public string? Url { get; set; }

    }
}
