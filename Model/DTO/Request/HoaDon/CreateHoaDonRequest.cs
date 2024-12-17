using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.HoaDon
{
    public class CreateHoaDonRequest
    {
        public long BanId { get; set; }
        public Guid MaOrder { get; set; }
        public DateTime NgayTao { get; set; }
        public double TongTien { get; set; }
        public bool ThanhToan { get; set; } = false;
        public List<HoaDonSetMonAnRequest> Set { get; set; } = new List<HoaDonSetMonAnRequest>();
        public List<HoaDonMonAnRequest> MonAn { get; set; } = new List<HoaDonMonAnRequest>();
    }


    public class HoaDonMonAnRequest
    {
        public long MonAnId { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
    }
    public class HoaDonSetMonAnRequest
    {
        public long SetId { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
    }
}
