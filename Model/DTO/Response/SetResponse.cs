using Entities.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
    public class SetResponse
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public decimal? Gia { get; set; }

        public string? Url { get; set; }

        public List<MonAnResponse> MonAn { get; set; } = new List<MonAnResponse>();
    }
    
}
