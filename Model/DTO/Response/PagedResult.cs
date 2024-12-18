using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
	public class PagedResult<T>
	{
		public List<T> Items { get; set; } // Danh sách kết quả trên trang hiện tại
		public int TotalRecords { get; set; } // Tổng số lượng bản ghi
		public int PageNumber { get; set; } // Số trang hiện tại
		public int PageSize { get; set; } // Số lượng bản ghi trên mỗi trang
	}
}
