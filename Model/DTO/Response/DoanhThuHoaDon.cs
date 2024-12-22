using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
	public class DoanhThuHoaDon
	{
		public DateTime Ngay { get; set; }
		public decimal DoanhThu { get; set; }
		public string Loai { get; set; }
	}
}
