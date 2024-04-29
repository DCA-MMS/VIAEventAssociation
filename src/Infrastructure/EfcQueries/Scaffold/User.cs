using System;
using System.Collections.Generic;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

public partial class User
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Avatar { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
