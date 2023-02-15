using AeroVendas.ULF.Services.Presentation.ModelBinders;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace AeroVendas.ULF.Services.Presentation.Controllers;

[Route("Arquivo")]
[ApiController]
public class ArquivoController : ControllerBase
{
	private readonly IServiceManager _service;

	public ArquivoController(IServiceManager service) => _service = service;


	/// <summary>
	/// Single File Upload
	/// </summary>
	/// <param name="file"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<ActionResult> PostSingleFile([FromForm] FileUploadModel fileDetails)
	{
		if (fileDetails == null)
		{
			return BadRequest();
		}
		try
		{
			await _service.ArquivoService.PostFileAsync(fileDetails);
			return Ok();
		}
		catch (Exception ex)
		{
			throw;
		}

	}

}
