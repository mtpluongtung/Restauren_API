using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
    public class HoaDonResponse
    {
        public long Id { get; set; }
        public Guid MaOrder { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal TongTien { get; set; }
        public bool ThanhToan { get; set; } = false;
        public string TenKhachHang  { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;   
        public OrderInHaDonResponse OrderDetails { get; set; } = new OrderInHaDonResponse(); 
        public List<MonAnInHoaDonResponse> MonAn { get; set; } = new List<MonAnInHoaDonResponse>();
        public List<SetInHoaDonResponse> SetMonAn { get; set; } = new List<SetInHoaDonResponse>();
    }

    public class MonAnInHoaDonResponse
    {
        public long MonAnId { get; set; }
        public string? Name { get; set; } =string.Empty;
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public decimal? Gia { get; set; }
    }
    public class SetInHoaDonResponse
    {
        public long SetId { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
		public string? Name { get; set; } 
		public decimal? Gia { get; set; }
	}
}
