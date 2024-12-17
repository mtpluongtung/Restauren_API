using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.Order
{
    public class CreateOrder
    {
        public long BanId { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public int SoLuongKH { get; set; }
    }
}
