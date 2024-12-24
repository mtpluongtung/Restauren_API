using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class MonAn
{
	public long Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public decimal? Gia { get; set; }

	public bool? IsActive { get; set; }

	public string? Url { get; set; }
	public int Loai { get; set; }

	public string? CreatedBy { get; set; } = string.Empty;

	public DateTime? CreatedDate { get; set; } = DateTime.Now;
	public ICollection<SetMonAn> SetMonAn { get; set; }
	
}
