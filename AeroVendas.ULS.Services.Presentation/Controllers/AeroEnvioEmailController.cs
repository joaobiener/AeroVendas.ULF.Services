using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace AeroVendas.ULF.Services.Controllers;

[Route("aerosolicitacao/{aerosolicitacaoId}/aeroenvioemails")]
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

		AeroStatusLoggingForCreationDto aeroStatus = new AeroStatusLoggingForCreationDto()
		{
			AeroSolicitacaoEmailId = aeroSolicitacaoId,
			AeroEnvioEmailId = aeroEnvioEmailToReturn.Id,
			CriadoEm = aeroEnvioEmailToReturn.CriadoEm,
			Status = aeroEnvioEmail.UltimoStatus

		};
		var createdAeroStatus = await _service.AeroStatusLoggingService.CreateStatusAsync(aeroStatus);


		return CreatedAtRoute("GetAeroEnvioEmailForSolicitacao", new { aeroSolicitacaoId, id = aeroEnvioEmailToReturn.Id },
			aeroEnvioEmailToReturn);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteAeroEnvioEmailForSolicitacao(Guid aeroSolicitacaoId, Guid id)
	{
		await _service.AeroEnvioEmailService.DeleteAeroEnvioEmailForSolicitacaoAsync(aeroSolicitacaoId, id, trackChanges: false);

		return NoContent();
	}

	[HttpPut("{id:guid}")]

	public async Task<IActionResult> UpdateAeroEnvioEmailForSolicitacao(Guid aeroSolicitacaoId, Guid id,
		[FromBody] AeroEnvioEmailForUpdateDto aeroEnvioEmail)
	{
		await _service.AeroEnvioEmailService.UpdateAeroEnvioEmailForSolicitacaoAsync(aeroSolicitacaoId, id, aeroEnvioEmail,
			solicTrackChanges: false, envioTrackChanges: true);

		AeroStatusLoggingForCreationDto aeroStatus = new AeroStatusLoggingForCreationDto()
		{
			AeroSolicitacaoEmailId = aeroSolicitacaoId,
			AeroEnvioEmailId = id,
			CriadoEm = DateTime.Now,
			Status = aeroEnvioEmail.UltimoStatus
		};
		var createdAeroStatus = await _service.AeroStatusLoggingService.CreateStatusAsync(aeroStatus);
		return NoContent();
	}

	[HttpPatch("{id:guid}")]
	public async Task<IActionResult> PartiallyUpdateAeroEnvioEmailForSolicitacao(Guid aeroSolicitacaoId, Guid id,
		[FromBody] JsonPatchDocument<AeroEnvioEmailForUpdateDto> patchDoc)
	{
		if (patchDoc is null)
			return BadRequest("patchDoc object enviado pelo cliente é nulo.");

		var result = await _service.AeroEnvioEmailService.GetAeroEnvioForPatchAsync(aeroSolicitacaoId, id,
			solicTrackChanges: false, envioTrackChanges: true);

		patchDoc.ApplyTo(result.aeroEnvioEmailToPatch, ModelState);

		TryValidateModel(result.aeroEnvioEmailToPatch);

		if (!ModelState.IsValid)
			return UnprocessableEntity(ModelState);

		string beforeStatus = result.aeroEnvioEntity.UltimoStatus;

		await _service.AeroEnvioEmailService.SaveChangesForPatchAsync(result.aeroEnvioEmailToPatch, result.aeroEnvioEntity);

		//Caso a alteração seja no UltimoStatus inclui registro na tabela de status
		AeroStatusLoggingForCreationDto aeroStatus = new AeroStatusLoggingForCreationDto()
		{
			AeroSolicitacaoEmailId = aeroSolicitacaoId,
			AeroEnvioEmailId = id,
			CriadoEm = DateTime.Now,
			Status = result.aeroEnvioEmailToPatch.UltimoStatus
		};
		

		if (result.aeroEnvioEmailToPatch.UltimoStatus != beforeStatus)
		{
			var createdAeroStatus = await _service.AeroStatusLoggingService.CreateStatusAsync(aeroStatus);

		}

		return NoContent();
	}

}
