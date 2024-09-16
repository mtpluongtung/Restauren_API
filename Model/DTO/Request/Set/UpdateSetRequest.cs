using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.Set
{
    public class UpdateSetRequest
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public decimal? Gia { get; set; }
        public string? Url { get; set; }
        public List<long> MonAn { get; set; } = new List<long>();
    }
}
