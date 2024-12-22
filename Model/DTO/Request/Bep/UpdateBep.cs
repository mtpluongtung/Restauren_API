using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.Bep
{
	public class UpdateBep
	{
		public long Id { get; set; }
		public int TrangThai { get; set; }
		public int SoLuong { get; set; }	
	}
}
