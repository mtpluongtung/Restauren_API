using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public partial class Order
    {
        [Key]
        public Guid MaOrder { get; set; } = Guid.NewGuid();
        public long BanId { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public int SoLuongKH { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
