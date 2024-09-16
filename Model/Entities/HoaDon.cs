using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class HoaDon
{
    public Guid MaHoaDon {  get; set; }
    public long BanId { get; set; }
    public DateTime NgayTao { get; set; }
    public double TongTien {  get; set; }
    public bool ThanhToan {  get; set; } = false;
}
