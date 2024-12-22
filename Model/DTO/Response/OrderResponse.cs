using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
    public class OrderResponse
    {
        public Guid MaOrder { get; set; }
        public long BanId { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public int SoLuongKH { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
    }
    public class OrderInHaDonResponse
	{
		public long BanId { get; set; }
        public string BanName { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string TenKhachHang { get; set; } = string.Empty;
		public int SoLuongKH { get; set; } = 0;
	}
}
