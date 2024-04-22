using System;
using System.Collections.Generic;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public partial class Invitation
{
    public string Id { get; set; } = null!;

    public string GuestId { get; set; } = null!;

    public int Status { get; set; }

    public string? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User Guest { get; set; } = null!;
}
