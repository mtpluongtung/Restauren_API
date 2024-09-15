using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class NhanVien
{
    public long? Id { get; set; }

    public string? TenNhanvien { get; set; }

    public string? MaNhanvien { get; set; }

    public DateTime? CheckinTime { get; set; } = DateTime.Now;

    public DateTime? CheckOutTime { get; set; } = DateTime.Now;
}
