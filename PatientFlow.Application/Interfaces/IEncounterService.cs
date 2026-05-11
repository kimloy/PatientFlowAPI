using PatientFlow.Domain.DTOs.Encounters;

namespace PatientFlow.Application.Interfaces;

public interface IEncounterService
{
    Task<EncounterResponse?> GetEncounterByIdAsync(Guid id);

    Task<IEnumerable<EncounterResponse>> GetEncountersByPatientIdAsync(Guid patientId);

    Task<EncounterResponse?> CreateEncounterAsync(CreateEncounterRequest request);
}