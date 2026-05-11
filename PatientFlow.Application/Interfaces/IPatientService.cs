using PatientFlow.Domain.DTOs.Patients;

namespace PatientFlow.Application.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientResponse>> GetPatientsAsync();

    Task<PatientResponse?> GetPatientByIdAsync(Guid id);

    Task<PatientResponse> CreatePatientAsync(CreatePatientRequest request);
}