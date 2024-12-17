using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public partial class ChamCong
    {
        public long Id { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime? CheckIn { get; set; } = null;
        public DateTime? CheckOut { get; set; } = null;
    }
}
