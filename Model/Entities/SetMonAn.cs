﻿using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class SetMonAn
{
    public long Id { get; set; }

    public long? MonAnId { get; set; }

    public long? SetId { get; set; }

    public string? TenMonAn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
