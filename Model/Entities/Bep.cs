using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
	public partial class Bep
	{
		public long Id { get; set; }
		public long IdMonAn { get; set; }
		public long BanId { get; set; } 
		public int SoLuong { get; set; }	
		public int TrangThai { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime? SuccesTime { get; set; }
	}
}
