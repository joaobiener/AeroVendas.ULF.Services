using AeroVendas.ULF.Services.Presentation.ActionFilters;
using AeroVendas.ULF.Services.Presentation.ModelBinders;
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

		return CreatedAtRoute("AeroSolicitacaoById", new { id = createdAeroSolicitacao.Id }, createdAeroSolicitacao);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteAeroSolicitacao(Guid id)
	{
		await _service.AeroSolicitacaoService.DeleteAeroSolicitacaoAsync(id, trackChanges: false);

		return NoContent();
	}


}
