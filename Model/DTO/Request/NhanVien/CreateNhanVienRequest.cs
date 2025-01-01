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
		public string Phone { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string ViTri { get; set; }
		public int CapBac { get; set; }
	}
}
