using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.HoaDon
{
    public class UpdateHoaDonRequest
    {
        public Guid MaHoaDon { get; set; }
        public long BanId { get; set; }
        public double TongTien { get; set; }
        public bool ThanhToan { get; set; } = false;
        public List<long> Set { get; set; } = new List<long>();
        public List<long> MonAn { get; set; } = new List<long>();
    }
}
