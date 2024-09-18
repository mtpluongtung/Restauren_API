using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class NhanVien
{
    public long? Id { get; set; }
    public string? TenNhanvien { get; set; }
    public string? MaNhanvien { get; set; }
    public string? Token { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; } 
}
