using PatientFlow.Domain.DTOs.Observations;

namespace PatientFlow.Application.Interfaces;

public interface IObservationService
{
    Task<ObservationResponse?> GetObservationByIdAsync(Guid id);

    Task<IEnumerable<ObservationResponse>> GetObservationsByPatientIdAsync(Guid patientId);

    Task<IEnumerable<ObservationResponse>> GetObservationsByEncounterIdAsync(Guid encounterId);

    Task<ObservationResponse?> CreateObservationAsync(CreateObservationRequest request);
}