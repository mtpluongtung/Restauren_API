using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
    public class MenuResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Type { get; set; }
        public string Url { get; set; } = string.Empty;
        public decimal Gia { get; set; }
    }
}
