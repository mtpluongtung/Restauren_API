using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class HoaDon
{
    public long Id { get; set; }    
    public Guid MaOrder {  get; set; }
    public DateTime NgayTao { get; set; } = DateTime.Now;
    public decimal TongTien {  get; set; }
    public bool ThanhToan {  get; set; } = false;
}
