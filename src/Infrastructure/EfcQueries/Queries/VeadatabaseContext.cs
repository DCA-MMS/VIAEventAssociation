using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public partial class VeadatabaseContext : DbContext
{
    public VeadatabaseContext()
    {
    }

    public VeadatabaseContext(DbContextOptions<VeadatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source = C:\\Users\\Simon\\source\\repos\\VIAEventAssociation\\src\\Infrastructure\\EfcDmPersistence\\VEADatabase.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasMany(d => d.Participants).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "EventUser",
                    r => r.HasOne<User>().WithMany().HasForeignKey("ParticipantsId"),
                    l => l.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                    j =>
                    {
                        j.HasKey("EventId", "ParticipantsId");
                        j.ToTable("EventUser");
                        j.HasIndex(new[] { "ParticipantsId" }, "IX_EventUser_ParticipantsId");
                    });
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.ToTable("Invitation");

            entity.HasIndex(e => e.EventId, "IX_Invitation_EventId");

            entity.HasIndex(e => e.GuestId, "IX_Invitation_GuestId");

            entity.HasOne(d => d.Event).WithMany(p => p.Invitations).HasForeignKey(d => d.EventId);

            entity.HasOne(d => d.Guest).WithMany(p => p.Invitations).HasForeignKey(d => d.GuestId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
