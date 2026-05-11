using Microsoft.EntityFrameworkCore;
using PatientFlow.Application.Interfaces;
using PatientFlow.Domain.DTOs.Patients;
using PatientFlow.Domain.Entities;
using PatientFlow.Infrastructure.Data;

namespace PatientFlow.Application.Services;

public class PatientService : IPatientService
{
    private readonly PatientFlowDbContext _context;

    public PatientService(PatientFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientResponse>> GetPatientsAsync()
    {
        return await _context.Patients
            .Select(patient => new PatientResponse
            {
                Id = patient.Id,
                MedicalRecordNumber = patient.MedicalRecordNumber,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                CreatedAt = patient.CreatedAt,
                UpdatedAt = patient.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<PatientResponse?> GetPatientByIdAsync(Guid id)
    {
        return await _context.Patients
            .Where(patient => patient.Id == id)
            .Select(patient => new PatientResponse
            {
                Id = patient.Id,
                MedicalRecordNumber = patient.MedicalRecordNumber,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                CreatedAt = patient.CreatedAt,
                UpdatedAt = patient.UpdatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PatientResponse> CreatePatientAsync(CreatePatientRequest request)
    {
        var mrnExists = await _context.Patients
            .AnyAsync(p => p.MedicalRecordNumber == request.MedicalRecordNumber);

        if (mrnExists)
        {
            throw new ArgumentException("A patient with this medical record number already exists.");
        }
        
        var patient = new Patient
        {
            MedicalRecordNumber = request.MedicalRecordNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return new PatientResponse
        {
            Id = patient.Id,
            MedicalRecordNumber = patient.MedicalRecordNumber,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            CreatedAt = patient.CreatedAt,
            UpdatedAt = patient.UpdatedAt
        };
    }
}