using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class NhanVien
{
    public long Id { get; set; }
	public string? TenNhanvien { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Address { get; set; }
	public string? MaNhanvien { get; set; }
	public string Token { get; set; }
	public string ViTri { get; set; }
	public int? CapBac { get; set; }

}
