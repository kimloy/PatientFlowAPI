using Microsoft.EntityFrameworkCore;
using PatientFlow.Application.Interfaces;
using PatientFlow.Domain.DTOs.Observations;
using PatientFlow.Domain.DTOs.Patients;
using PatientFlow.Domain.Entities;
using PatientFlow.Infrastructure.Data;

namespace PatientFlow.Application.Services;

public class ObservationService : IObservationService
{
    private readonly PatientFlowDbContext _context;

    public ObservationService(PatientFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ObservationResponse?> GetObservationByIdAsync(Guid id)
    {
        return await _context.Observations
            .Where(o => o.Id == id)
            .Select(o => new ObservationResponse
            {
                Id = o.Id,
                PatientId = o.PatientId,
                Patient = o.Patient == null ? null : new PatientResponse
                {
                    Id = o.Patient.Id,
                    MedicalRecordNumber = o.Patient.MedicalRecordNumber,
                    FirstName = o.Patient.FirstName,
                    LastName = o.Patient.LastName,
                    DateOfBirth = o.Patient.DateOfBirth,
                    Gender = o.Patient.Gender,
                    PhoneNumber = o.Patient.PhoneNumber,
                    Email = o.Patient.Email,
                    CreatedAt = o.Patient.CreatedAt,
                    UpdatedAt = o.Patient.UpdatedAt
                },
                EncounterId = o.EncounterId,
                ObservationType = o.ObservationType,
                Value = o.Value,
                Unit = o.Unit,
                Status = o.Status,
                RecordedAt = o.RecordedAt,
                CreatedAt = o.CreatedAt,
                UpdatedAt = o.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ObservationResponse>> GetObservationsByPatientIdAsync(Guid patientId)
    {
        return await _context.Observations
            .Where(o => o.PatientId == patientId)
            .Select(o => new ObservationResponse
            {
                Id = o.Id,
                PatientId = o.PatientId,
                Patient = o.Patient == null ? null : new PatientResponse
                {
                    Id = o.Patient.Id,
                    MedicalRecordNumber = o.Patient.MedicalRecordNumber,
                    FirstName = o.Patient.FirstName,
                    LastName = o.Patient.LastName,
                    DateOfBirth = o.Patient.DateOfBirth,
                    Gender = o.Patient.Gender,
                    PhoneNumber = o.Patient.PhoneNumber,
                    Email = o.Patient.Email,
                    CreatedAt = o.Patient.CreatedAt,
                    UpdatedAt = o.Patient.UpdatedAt
                },
                EncounterId = o.EncounterId,
                ObservationType = o.ObservationType,
                Value = o.Value,
                Unit = o.Unit,
                Status = o.Status,
                RecordedAt = o.RecordedAt,
                CreatedAt = o.CreatedAt,
                UpdatedAt = o.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ObservationResponse>> GetObservationsByEncounterIdAsync(Guid encounterId)
    {
        return await _context.Observations
            .Where(o => o.EncounterId == encounterId)
            .Select(o => new ObservationResponse
            {
                Id = o.Id,
                PatientId = o.PatientId,
                EncounterId = o.EncounterId,
                ObservationType = o.ObservationType,
                Value = o.Value,
                Unit = o.Unit,
                Status = o.Status,
                RecordedAt = o.RecordedAt,
                CreatedAt = o.CreatedAt,
                UpdatedAt = o.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<ObservationResponse?> CreateObservationAsync(CreateObservationRequest request)
    {
        var patientExists = await _context.Patients.AnyAsync(p => p.Id == request.PatientId);

        if (!patientExists)
        {
            return null;
        }

        if (request.EncounterId.HasValue)
        {
            var encounterExists = await _context.Encounters
                .AnyAsync(e => e.Id == request.EncounterId.Value && e.PatientId == request.PatientId);

            if (!encounterExists)
            {
                return null;
            }
        }

        var observation = new Observation
        {
            PatientId = request.PatientId,
            EncounterId = request.EncounterId,
            ObservationType = request.ObservationType,
            Value = request.Value,
            Unit = request.Unit,
            Status = request.Status,
            RecordedAt = request.RecordedAt
        };

        _context.Observations.Add(observation);
        await _context.SaveChangesAsync();

        return await GetObservationByIdAsync(observation.Id);
    }
}