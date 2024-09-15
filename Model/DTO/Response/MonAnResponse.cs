using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Response
{
    public class MonAnResponse
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public decimal? Gia { get; set; }

        public bool? IsActive { get; set; }

        public string? Url { get; set; }
    }
}
