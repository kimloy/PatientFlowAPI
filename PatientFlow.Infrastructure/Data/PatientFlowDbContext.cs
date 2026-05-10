using Microsoft.EntityFrameworkCore;
using PatientFlow.Domain.Entities;

namespace PatientFlow.Infrastructure.Data;

public class PatientFlowDbContext : DbContext
{
    public PatientFlowDbContext(DbContextOptions<PatientFlowDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; } = null!;

    public DbSet<Encounter> Encounters { get; set; } = null!;

    public DbSet<ServiceRequest> ServiceRequests { get; set; } = null!;

    public DbSet<Observation> Observations { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
{
    var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is PatientFlow.Domain.Common.BaseEntity &&
                    (e.State == EntityState.Added ||
                     e.State == EntityState.Modified));

    foreach (var entry in entries)
    {
        var entity = (PatientFlow.Domain.Common.BaseEntity)entry.Entity;

        if (entry.State == EntityState.Added)
        {
            entity.CreatedAt = DateTime.UtcNow;
        }

        entity.UpdatedAt = DateTime.UtcNow;
    }

    return await base.SaveChangesAsync(cancellationToken);
}
}