using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace AeroVendas.ULF.Services.Controllers;

[Route("aerosolicitacao")]
[ApiController]
public class AeroSolicitacaoController : ControllerBase
{
	private readonly IServiceManager _service;

	public AeroSolicitacaoController(IServiceManager service) => _service = service;

	[HttpGet]
	public async Task<IActionResult> GetAeroSolicitacoes([FromQuery] ViewAeroVendasParameters viewAeroVendasParameters)
	{
		var pagedResult =   await _service.AeroSolicitacaoService.GetAllAeroSolicitacaoAsync(viewAeroVendasParameters, trackChanges: false);
		Response.Headers.Add("X-Pagination",
					JsonSerializer.Serialize(pagedResult.metaData));
		return Ok(pagedResult.AeroSolicitacao);
	}

	[HttpGet("{id:guid}", Name = "AeroSolicitacaoById")]
	public async Task<IActionResult> GetAeroSolicitacaoById(Guid id)
	{
		var aeroSolicitacao = await _service.AeroSolicitacaoService.GetAeroSolicitacaoByIdAsync(id, trackChanges: false);
		return Ok(aeroSolicitacao);
	}
	[HttpPost]
	public async Task<IActionResult> CreateAeroSolicitacao([FromBody] AeroSolicitacaoEmailForCreationDto aeroSolicitacao)
	{
		var createdAeroSolicitacao = await _service.AeroSolicitacaoService.CreateAeroSolicitacaoAsync(aeroSolicitacao);

		await _service.AeroEnvioEmailService.BulkInsertAeroEnvioEmailForSolicitacaoAsync(createdAeroSolicitacao);

		AeroStatusLoggingForCreationDto aeroStatus = new AeroStatusLoggingForCreationDto()
		{
			AeroSolicitacaoEmailId = createdAeroSolicitacao.Id,
			CriadoEm = DateTime.Now,
			Status = nameof(Status.PorEnviar)
		};


		var createdAeroStatus = await _service.AeroStatusLoggingService.CreateStatusAsync(aeroStatus);


		return CreatedAtRoute("AeroSolicitacaoById", new { id = createdAeroSolicitacao.Id }, createdAeroSolicitacao);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateAeroSolicitacao(Guid id, [FromBody] AeroSolicitacaoEmailForUpdateDto aeroSolictacaoDto)
	{
		if (aeroSolictacaoDto is null)
			return BadRequest("aeroSolictacaoDto é nulo");

		if (!ModelState.IsValid)
			return UnprocessableEntity(ModelState);

		await _service.AeroSolicitacaoService.UpdateAeroSolcitacaoAsync(id, aeroSolictacaoDto, trackChanges: true);

		AeroStatusLoggingForCreationDto aeroStatus = new AeroStatusLoggingForCreationDto()
		{
			AeroSolicitacaoEmailId = id,
			CriadoEm = DateTime.Now,
			Status = aeroSolictacaoDto.UltimoStatus
		};
		var createdAeroStatus = await _service.AeroStatusLoggingService.CreateStatusAsync(aeroStatus);

		return NoContent();
	}

	[HttpPatch("{aeroSolicitacaoId:guid}")]
	public async Task<IActionResult> PartiallyUpdateAeroSolicitacao(Guid aeroSolicitacaoId, 
		[FromBody] JsonPatchDocument<AeroSolicitacaoEmailForUpdateDto> patchDoc)
	{
		if (patchDoc is null)
			return BadRequest("patchDoc object enviado pelo cliente é nulo.");

		var result = await _service.AeroSolicitacaoService.GetAeroSolicitacaoForPatchAsync(aeroSolicitacaoId,
			solicTrackChanges: true);

		patchDoc.ApplyTo(result.aeroSolicitacaoToPatch, ModelState);

		TryValidateModel(result.aeroSolicitacaoToPatch);

		if (!ModelState.IsValid)
			return UnprocessableEntity(ModelState);

		string beforeStatus = result.aeroSolicitacaoEntity.UltimoStatus;

		await _service.AeroSolicitacaoService.SaveChangesForPatchAsync(result.aeroSolicitacaoToPatch, result.aeroSolicitacaoEntity);

		//Caso a alteração seja no UltimoStatus inclui registro na tabela de status
		AeroStatusLoggingForCreationDto aeroStatus = new AeroStatusLoggingForCreationDto()
		{
			AeroSolicitacaoEmailId = aeroSolicitacaoId,
			CriadoEm = DateTime.Now,
			Status = result.aeroSolicitacaoToPatch.UltimoStatus
		};


		if (result.aeroSolicitacaoToPatch.UltimoStatus != beforeStatus)
		{
			var createdAeroStatus = await _service.AeroStatusLoggingService.CreateStatusAsync(aeroStatus);

		}

		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteAeroSolicitacao(Guid id)
	{
		await _service.AeroSolicitacaoService.DeleteAeroSolicitacaoAsync(id, trackChanges: false);

		return NoContent();
	}


}
