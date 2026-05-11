namespace PatientFlow.Domain.DTOs.Fhir;

public class FhirPatientRequest
{
    public string ResourceType { get; set; } = "Patient";

    public List<FhirIdentifier> Identifier { get; set; } = new();

    public List<FhirHumanName> Name { get; set; } = new();

    public string? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }
}

public class FhirIdentifier
{
    public string? System { get; set; }

    public string? Value { get; set; }
}

public class FhirHumanName
{
    public string? Family { get; set; }

    public List<string> Given { get; set; } = new();
}