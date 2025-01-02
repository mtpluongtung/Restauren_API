using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public partial class CaLamViecNhanVien
    {
        public long Id { get; set; }    
        public long CaId { get; set; }
        public string CaName { get; set; }
        public string MaNhanVien { get; set; }
    }
}
