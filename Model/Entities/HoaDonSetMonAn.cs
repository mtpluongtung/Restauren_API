using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public partial class HoaDonSetMonAn
    {
        public long Id {  get; set; }
        public Guid MaOrder { get; set; }
        public long SetId {  get; set; }
        public int SoLuong {  get; set; }
        public decimal ThanhTien { get; set; }
    }
}
