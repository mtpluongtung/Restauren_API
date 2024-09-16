using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public partial class HoaDonMonAn
    {
        public Guid HoaDonId { get; set; }
        public long MonAnId { get; set; }
        public int SoLuong {  get; set; }
        public double ThanhTien { get; set; }
    }
}
