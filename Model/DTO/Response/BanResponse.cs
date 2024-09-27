using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Response
{
    public class BanResponse
    {
        public long Id { get; set; }
        public string? TenBan { get; set; }
        public int TrangThai { get; set; }
        public string? Tang { get; set; }
        public int SoNguoi { get; set; }
        public int SoGhe { get; set; }
    }
}
