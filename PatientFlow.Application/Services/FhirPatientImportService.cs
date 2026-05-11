using PatientFlow.Application.Interfaces;
using PatientFlow.Domain.DTOs.Fhir;
using PatientFlow.Domain.DTOs.Patients;
using PatientFlow.Domain.Enums;

namespace PatientFlow.Application.Services;

public class FhirPatientImportService : IFhirPatientImportService
{
    private readonly IPatientService _patientService;

    public FhirPatientImportService(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public async Task<PatientResponse> ImportPatientAsync(FhirPatientRequest request)
    {
        if (request.ResourceType != "Patient")
        {
            throw new ArgumentException("FHIR resourceType must be Patient.");
        }

        var mrn = request.Identifier
            .FirstOrDefault(i => i.System != null && i.System.Contains("mrn", StringComparison.OrdinalIgnoreCase))
            ?.Value
            ?? request.Identifier.FirstOrDefault()?.Value
            ?? throw new ArgumentException("FHIR Patient must include at least one identifier.");

        var name = request.Name.FirstOrDefault()
            ?? throw new ArgumentException("FHIR Patient must include at least one name.");

        var firstName = name.Given.FirstOrDefault()
            ?? throw new ArgumentException("FHIR Patient must include at least one given name.");

        var lastName = name.Family
            ?? throw new ArgumentException("FHIR Patient must include a family name.");

        var birthDate = request.BirthDate
            ?? throw new ArgumentException("FHIR Patient must include birthDate.");

        var createPatientRequest = new CreatePatientRequest
        {
            MedicalRecordNumber = mrn,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = birthDate.ToDateTime(TimeOnly.MinValue),
            Gender = MapGender(request.Gender)
        };

        return await _patientService.CreatePatientAsync(createPatientRequest);
    }

    private static Gender MapGender(string? fhirGender)
    {
        return fhirGender?.ToLowerInvariant() switch
        {
            "male" => Gender.Male,
            "female" => Gender.Female,
            "other" => Gender.Other,
            _ => Gender.Unknown
        };
    }
}