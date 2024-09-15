using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities;

public partial class Ban
{

    [Key]
    public long Id { get; set; }

    public string? TenBan { get; set; }

    public bool TrangThai { get; set; } = false;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; } = DateTime.Now;
}
