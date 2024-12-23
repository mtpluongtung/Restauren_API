using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.Bep
{
	public class BepCreateRequest
	{
		public Guid MaOrder { get; set; }
		public List<KhachGoiMonRequest> MonAn { get; set; }
		public long SetId { get; set; }
	}
	public class KhachGoiMonRequest
	{
		public long IdMonAn { get; set; }
		public int SoLuong { get; set; }
	}
}
