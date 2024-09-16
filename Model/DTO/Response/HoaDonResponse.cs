using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
    public class HoaDonResponse
    {
        public Guid MaHoaDon { get; set; }
        public long BanId { get; set; }
        public DateTime NgayTao { get; set; }
        public double TongTien { get; set; }
        public bool ThanhToan { get; set; } = false;

        public List<MonAnInHoaDonResponse> MonAn { get; set; } = new List<MonAnInHoaDonResponse>();
        public List<SetInHoaDonResponse> SetMonAn { get; set; } = new List<SetInHoaDonResponse>();
    }

    public class MonAnInHoaDonResponse
    {
        public long MonAnId { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
    }
    public class SetInHoaDonResponse
    {
        public long SetId { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
    }
}
