using Models.DTO.Request.MonAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.NhanVien
{
	public class SearchChamCong : BaseSearchRequest
	{
		public DateTime? From { get; set; }
		public DateTime? To { get; set; }
	}
}
