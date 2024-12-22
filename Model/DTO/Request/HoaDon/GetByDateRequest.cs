using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.HoaDon
{
	public class GetByDateRequest
	{
		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
