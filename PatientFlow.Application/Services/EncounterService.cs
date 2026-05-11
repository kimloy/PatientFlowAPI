using Microsoft.EntityFrameworkCore;
using PatientFlow.Application.Interfaces;
using PatientFlow.Domain.DTOs.Encounters;
using PatientFlow.Domain.DTOs.Patients;
using PatientFlow.Domain.Entities;
using PatientFlow.Infrastructure.Data;

namespace PatientFlow.Application.Services;

public class EncounterService : IEncounterService
{
    private readonly PatientFlowDbContext _context;

    public EncounterService(PatientFlowDbContext context)
    {
        _context = context;
    }

    public async Task<EncounterResponse?> GetEncounterByIdAsync(Guid id)
    {
        return await _context.Encounters
            .Where(e => e.Id == id)
            .Select(e => new EncounterResponse
            {
                Id = e.Id,
                PatientId = e.PatientId,
                Patient = e.Patient == null ? null : new PatientResponse
                {
                    Id = e.Patient.Id,
                    MedicalRecordNumber = e.Patient.MedicalRecordNumber,
                    FirstName = e.Patient.FirstName,
                    LastName = e.Patient.LastName,
                    DateOfBirth = e.Patient.DateOfBirth,
                    Gender = e.Patient.Gender,
                    PhoneNumber = e.Patient.PhoneNumber,
                    Email = e.Patient.Email,
                    CreatedAt = e.Patient.CreatedAt,
                    UpdatedAt = e.Patient.UpdatedAt
                },
                EncounterType = e.EncounterType,
                Status = e.Status,
                Department = e.Department,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Reason = e.Reason,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<EncounterResponse>> GetEncountersByPatientIdAsync(Guid patientId)
    {
        return await _context.Encounters
            .Where(e => e.PatientId == patientId)
            .Select(e => new EncounterResponse
            {
                Id = e.Id,
                PatientId = e.PatientId,
                Patient = e.Patient == null ? null : new PatientResponse
                {
                    Id = e.Patient.Id,
                    MedicalRecordNumber = e.Patient.MedicalRecordNumber,
                    FirstName = e.Patient.FirstName,
                    LastName = e.Patient.LastName,
                    DateOfBirth = e.Patient.DateOfBirth,
                    Gender = e.Patient.Gender,
                    PhoneNumber = e.Patient.PhoneNumber,
                    Email = e.Patient.Email,
                    CreatedAt = e.Patient.CreatedAt,
                    UpdatedAt = e.Patient.UpdatedAt
                },
                EncounterType = e.EncounterType,
                Status = e.Status,
                Department = e.Department,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Reason = e.Reason,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<EncounterResponse?> CreateEncounterAsync(CreateEncounterRequest request)
    {
        var patientExists = await _context.Patients
            .AnyAsync(p => p.Id == request.PatientId);

        if (!patientExists)
        {
            return null;
        }

        var encounter = new Encounter
        {
            PatientId = request.PatientId,
            EncounterType = request.EncounterType,
            Status = request.Status,
            Department = request.Department,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Reason = request.Reason
        };

        _context.Encounters.Add(encounter);
        await _context.SaveChangesAsync();

        return new EncounterResponse
        {
            Id = encounter.Id,
            PatientId = encounter.PatientId,
            EncounterType = encounter.EncounterType,
            Status = encounter.Status,
            Department = encounter.Department,
            StartTime = encounter.StartTime,
            EndTime = encounter.EndTime,
            Reason = encounter.Reason,
            CreatedAt = encounter.CreatedAt,
            UpdatedAt = encounter.UpdatedAt
        };
    }
}