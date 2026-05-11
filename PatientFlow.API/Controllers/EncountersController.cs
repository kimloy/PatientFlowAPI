using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientFlow.Domain.Entities;
using PatientFlow.Infrastructure.Data;
using PatientFlow.Domain.DTOs.Encounters;
using PatientFlow.Domain.DTOs.Patients;
using PatientFlow.Application.Interfaces;

namespace PatientFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EncountersController : ControllerBase
{
    private readonly IEncounterService _encounterService;

    public EncountersController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EncounterResponse>> GetEncounterById(Guid id)
    {
        var encounter = await _encounterService.GetEncounterByIdAsync(id);

        if (encounter == null)
        {
            return NotFound();
        }

        return Ok(encounter);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<EncounterResponse>>> GetEncountersByPatientId(Guid patientId)
    {
        var encounters = await _encounterService.GetEncountersByPatientIdAsync(patientId);

        return Ok(encounters);
    }

    [HttpPost]
    public async Task<ActionResult<EncounterResponse>> CreateEncounter(CreateEncounterRequest request)
    {
        var encounter = await _encounterService.CreateEncounterAsync(request);

        if (encounter == null)
        {
            return BadRequest("Patient does not exist.");
        }

        return CreatedAtAction(nameof(GetEncounterById), new { id = encounter.Id }, encounter);
    }
}