using PatientFlow.Domain.Common;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.Entities;

public class Patient : BaseEntity
{
    public string MedicalRecordNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;  

    public string LastName { get; set; } = string.Empty;    

    public DateTime DateOfBirth { get; set; }

    public Gender Gender { get; set;}

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();

    public ICollection<Observation> Observations { get; set; } = new List<Observation>();   

    public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();  

}