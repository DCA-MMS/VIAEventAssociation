using System;
using System.Collections.Generic;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

public partial class Event
{
    public string Id { get; set; } = null!;

    public int Status { get; set; }

    public int Visibility { get; set; }

    public string? DurationStart { get; set; }

    public string? DurationEnd { get; set; }

    public int Capacity { get; set; }

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<User> Participants { get; set; } = new List<User>();
}
