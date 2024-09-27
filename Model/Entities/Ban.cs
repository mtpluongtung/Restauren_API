using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities;

public partial class Ban
{

    [Key]
    public long Id { get; set; }
    public string? TenBan { get; set; }
    public int TrangThai { get; set; }
    public string? Tang { get; set; }
    public int SoNguoi { get; set; }
    public int SoGhe { get; set; }
    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; } = DateTime.Now;
}
