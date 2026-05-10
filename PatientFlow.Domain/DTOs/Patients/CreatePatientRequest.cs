using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.DTOs.Patients;

public class CreatePatientRequest
{
    public string MedicalRecordNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }
}