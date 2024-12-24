using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.MonAn
{
	public class SearchMonAnRequest
	{
		public string? Text { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
