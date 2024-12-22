using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
	public class DoanhThuChiTiet
	{
		public string MaHoaDon { get; set; }
		public decimal TongTien { get; set; }
		public List<ChiTietHoaDon> ChiTietHoaDon { get; set; }

	}
	public class ChiTietHoaDon
	{
		public string TenMonAn { get; set; }
		public int? Soluong { get; set; }
		public decimal? DonGia { get; set; }
		public decimal? ThanhTien { get; set; }
	}
}
