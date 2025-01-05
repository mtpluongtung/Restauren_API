using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.Bep
{
	public class ThemMonTinhTien
	{
		public Guid MaOrder { get; set; }
		public long IdMonAn { get; set; }
		public int SoLuong { get; set; }
		public decimal ThanhTien { get; set; }
	}
}
