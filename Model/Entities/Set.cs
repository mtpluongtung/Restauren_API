using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Set
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public decimal? Gia { get; set; }

    public string? Url { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
