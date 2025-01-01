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
		public string Name { get; set; }
		public List<InfoChamCong> infoChamCongs { get; set; } = new List<InfoChamCong>();
	}
	public class InfoChamCong
	{
		public DateTime? CheckIn { get; set; }
		public DateTime? CheckOut { get; set; }
	}
}
