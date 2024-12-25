using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Request.Ban
{
    public class CreateBanRequest
    {
        public string TenBan { get; set; } = string.Empty;
        public string Tang { get; set; } = string.Empty;

    }
}
