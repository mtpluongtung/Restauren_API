using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.Order
{
    public class OrderThemRequest
    {
        public int Type { get; set; }
        public string Note { get; set; } = string.Empty;
        
    }
}
