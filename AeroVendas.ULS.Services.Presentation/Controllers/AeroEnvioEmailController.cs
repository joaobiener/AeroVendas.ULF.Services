using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace AeroVendas.ULF.Services.Controllers;

[Route("api/aerosolicitacao/{aerosolicitacaoId}/aeroenvioemails")]
[ApiController]
public class AeroEnvioEmailController : ControllerBase
{
	private readonly IServiceManager _service;

	public AeroEnvioEmailController(IServiceManager service) => _service = service;

	[HttpGet]
	public async Task<IActionResult> GetEnvioEmailForSolicitacao(Guid aeroSolicitacaoId,
		[FromQuery] ViewAeroVendasParameters viewAeroVendasParameters)
	{
		var pagedResult = await _service.AeroEnvioEmailService.GetAllAeroEnvioEmailAsync(aeroSolicitacaoId,
			viewAeroVendasParameters, trackChanges: false);

		Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

		return Ok(pagedResult.aeroEnvioEmail);
	}

	[HttpGet("{id:guid}", Name = "GetAeroEnvioEmailForSolicitacao")]
	public async Task<IActionResult> GetAeroEnvioEmailForSolicitacao(Guid aeroSolicitacaoId, Guid id)
	{
		var aeroEnvioEmail = await _service.AeroEnvioEmailService.GetAeroEnvioEmailAsync(aeroSolicitacaoId, id, trackChanges: false);
		return Ok(aeroEnvioEmail);
	}

	[HttpPost]

	public async Task<IActionResult> CreateAeroEnvioEmailForSolicitacao
		(Guid aeroSolicitacaoId, 
		[FromBody] AeroEnvioEmailForCreationDto aeroEnvioEmail)
	{
		var aeroEnvioEmailToReturn = await _service.AeroEnvioEmailService.CreateAeroEnvioEmailForSolicitacaoAsync(
					aeroSolicitacaoId, aeroEnvioEmail,
			trackChanges: false);

		return CreatedAtRoute("GetEmployeeForCompany", new { aeroSolicitacaoId, id = aeroEnvioEmailToReturn.Id },
			aeroEnvioEmailToReturn);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
	{
		await _service.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, trackChanges: false);

		return NoContent();
	}

	[HttpPut("{id:guid}")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id,
		[FromBody] EmployeeForUpdateDto employee)
	{
		await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee,
			compTrackChanges: false, empTrackChanges: true);

		return NoContent();
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
		[FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
	{
		if (patchDoc is null)
			return BadRequest("patchDoc object sent from client is null.");

		var result = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id,
			compTrackChanges: false, empTrackChanges: true);

		patchDoc.ApplyTo(result.employeeToPatch, ModelState);

		TryValidateModel(result.employeeToPatch);

		if (!ModelState.IsValid)
			return UnprocessableEntity(ModelState);

		await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntity);

		return NoContent();
	}
}
