using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
	public class CaLamViecResponse
	{
		public long Id { get; set; }
		public int GioBatDauCa { get; set; }
		public int GioKetThucCa { get; set; }
		public string LoaiCa { get; set; }
	}
}
