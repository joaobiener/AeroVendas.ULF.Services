using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace AeroVendas.ULF.Services.Controllers;

[Route("aerostatus")]
[ApiController]
public class AeroStatusLoggingController : ControllerBase
{
	private readonly IServiceManager _service;

	public AeroStatusLoggingController(IServiceManager service) => _service = service;


	[HttpGet("{id:guid}", Name = "AeroStatusById")]
	public async Task<IActionResult> GetAeroStatusById(Guid id)
	{
		var aeroStatus = await _service.AeroStatusLoggingService.GetAeroStatusByIdAsync(id, trackChanges: false);

		return Ok(aeroStatus);
	}

	[HttpGet("solicitacao/{aerosolicitacaoid:guid}", Name = "AeroStatusBySolicitacao")]
	public async Task<IActionResult> GetAeroStatusBySolicitacao(
			Guid? AeroSolicitacaoId,
			[FromQuery] ViewAeroVendasParameters viewAeroVendasParameters)
	{
	
		var pagedResult = await _service.AeroStatusLoggingService.GetStatusByIdAsync(AeroSolicitacaoId,
			null,
			viewAeroVendasParameters, 
			trackChanges: false);

		Response.Headers.Add("X-Pagination",
						 JsonSerializer.Serialize(pagedResult.metaData));

		return Ok(pagedResult.AeroStatus);
	}


	[HttpGet("envioEmail/{aeroenvioemailid:guid}", Name = "AeroStatusByEnvio")]
	public async Task<IActionResult> GetAeroStatusByEnvio(
			Guid? AeroEnvioEmailId,
			[FromQuery] ViewAeroVendasParameters viewAeroVendasParameters)
	{
		var pagedResult = await _service.AeroStatusLoggingService.GetStatusByIdAsync(null,
			AeroEnvioEmailId,
			viewAeroVendasParameters,
			trackChanges: false);

		Response.Headers.Add("X-Pagination",
						 JsonSerializer.Serialize(pagedResult.metaData));

		return Ok(pagedResult.AeroStatus);
	}

	
	[HttpPost]
	public async Task<IActionResult> CreateAeroStatus([FromBody] AeroStatusLoggingForCreationDto aeroStatus)
	{
		var createdAeroStatus = await _service.AeroStatusLoggingService.CreateStatusAsync(aeroStatus);

		return CreatedAtRoute("AeroStatusByEnvio", new { id = createdAeroStatus.Id }, createdAeroStatus);
	}


}
