using System.ComponentModel.DataAnnotations;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Domain.DTOs.Patients;

public class CreatePatientRequest
{
    [Required]
    [StringLength(25)]
    public string MedicalRecordNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Phone]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }
}