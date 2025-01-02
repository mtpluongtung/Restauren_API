using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
	public class ChamCongNhanVien
	{
		public long Id { get; set; }
		public string TenNhanVien { get; set; }
		public string MaNhanVien { get; set; }
		public DateTime? CheckIn { get; set; }
		public DateTime? CheckOut { get; set; }
		public double TotalTime { get; set; }
		public bool TrangThai { get; set; }
	}
	
}
