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
}