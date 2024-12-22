using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
	public class BepResponse
	{
		public long Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string TenBan { get; set; }  = string.Empty;
		public int SoLuong { get; set; }
		public int TrangThai { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime? SuccesTime { get; set; }
	}
}
