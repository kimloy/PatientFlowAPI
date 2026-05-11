using PatientFlow.Domain.DTOs.Fhir;
using PatientFlow.Domain.DTOs.Patients;

namespace PatientFlow.Application.Interfaces;

public interface IFhirPatientImportService
{
    Task<PatientResponse> ImportPatientAsync(FhirPatientRequest request);
}