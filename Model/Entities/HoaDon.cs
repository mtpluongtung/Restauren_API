using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class HoaDon
{
    public long? Id { get; set; }

    public long? BanId { get; set; }

    public long? SetId { get; set; }

    public long? MonAnId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
