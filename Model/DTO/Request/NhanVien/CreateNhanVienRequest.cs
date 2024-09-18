using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.NhanVien
{
    public class CreateNhanVienRequest
    {
        public string TenNhanvien { get; set; }
        public string MaNhanvien { get; set; }
    }
}
