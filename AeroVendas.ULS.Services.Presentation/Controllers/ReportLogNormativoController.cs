using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.RequestFeatures;
using System.ComponentModel.Design;
using System.Text.Json;

namespace AeroVendas.ULF.Services.Presentation.Controllers;


[Route("ViewContratoSemAeroVendas")]
[ApiController]
public class ReportLogAeroVendasController : ControllerBase
{
    private readonly IServiceManager _service;

    public ReportLogAeroVendasController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetViewLogAeroVendas([FromQuery] ViewAeroVendasParameters viewAeroVendasParameters)
    {
        var pagedResult = await _service.ViewAeroVendasService.GetAllViewsAeroVendasAsync(viewAeroVendasParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination",       
                        JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.viewAeroVendas);
    }
            
    [HttpGet("GetByRequest")]
    public async Task<IActionResult> GetLogAeroVendasByUser(string? Contrato,
		                                                    string? CodigoBeneficiario,
		                                                    string? NomeBeneficiario,
		                                                    string? Cidade,
															[FromQuery] ViewAeroVendasParameters viewAeroVendasParameters)
    {
        var pagedResult = await _service.ViewAeroVendasService.GetViewAeroVendasByAsync(
			Contrato,
			CodigoBeneficiario,
			NomeBeneficiario,
			Cidade, 
            viewAeroVendasParameters, trackChanges: false);

        Response.Headers.Add("X-Pagination",
                         JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.viewAeroVendas);
    }

    [HttpGet("GetCidadeSemAeroVendas")]
    public async Task<IActionResult> GetViewNomeAeroVendas([FromQuery] ViewAeroVendasParameters viewAeroVendasParameters)
    {
        var pagedResult = await _service.ViewAeroVendasService.GetViewCidadeAeroVendasAsync(viewAeroVendasParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination",
                        JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.viewAeroVendas);
    }
    

}



