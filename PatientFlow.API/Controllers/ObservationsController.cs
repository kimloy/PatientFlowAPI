using Microsoft.AspNetCore.Mvc;
using PatientFlow.Application.Interfaces;
using PatientFlow.Domain.DTOs.Observations;

namespace PatientFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObservationsController : ControllerBase
{
    private readonly IObservationService _observationService;

    public ObservationsController(IObservationService observationService)
    {
        _observationService = observationService;
    }

    [HttpPost]
    public async Task<ActionResult<ObservationResponse>> CreateObservation(
        CreateObservationRequest request)
    {
        var observation = await _observationService
            .CreateObservationAsync(request);

        if (observation == null)
        {
            return BadRequest(
                "Patient or encounter relationship is invalid.");
        }

        return CreatedAtAction(
            nameof(GetObservationById),
            new { id = observation.Id },
            observation);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObservationResponse>> GetObservationById(Guid id)
    {
        var observation = await _observationService
            .GetObservationByIdAsync(id);

        if (observation == null)
        {
            return NotFound();
        }

        return Ok(observation);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<ObservationResponse>>>
        GetObservationsByPatientId(Guid patientId)
    {
        var observations = await _observationService
            .GetObservationsByPatientIdAsync(patientId);

        return Ok(observations);
    }

    [HttpGet("encounter/{encounterId}")]
    public async Task<ActionResult<IEnumerable<ObservationResponse>>>
        GetObservationsByEncounterId(Guid encounterId)
    {
        var observations = await _observationService
            .GetObservationsByEncounterIdAsync(encounterId);

        return Ok(observations);
    }
}