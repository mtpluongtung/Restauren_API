using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Set
{
    public Set()
    {
        
    }

    public Set(string? name, decimal? gia, string? url)
    {
        Name = name;
        Gia = gia;
        Url = url;
    }

    public long Id { get; set; }

    public string? Name { get; set; }

    public decimal? Gia { get; set; }

    public string? Url { get; set; }

    public bool? IsActive { get; set; } = true;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; } = DateTime.Now;

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    public virtual IReadOnlyCollection<MonAn> MonAns { get; set; }
    public virtual IReadOnlyCollection<SetMonAn> SetMonAn { get; set; }
}
