using AeroVendas.ULF.Services.Presentation.ModelBinders;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace AeroVendas.ULF.Services.Presentation.Controllers;

[Route("Upload")]
[ApiController]
public class ArquivoController : ControllerBase
{
	private readonly IServiceManager _service;

	public ArquivoController(IServiceManager service) => _service = service;


	[HttpGet]
	public async Task<IActionResult> GetArquivos([FromQuery] ViewAeroVendasParameters viewAeroVendasParameters)
	{
       

        var pagedResult = await _service.ArquivoService.GetAllArquivosAsync(viewAeroVendasParameters, trackChanges: false);
		Response.Headers.Add("X-Pagination",
						JsonSerializer.Serialize(pagedResult.metaData));

		return Ok(pagedResult.arquivos);
	}

	/// <summary>
	/// Single File Upload
	/// </summary>
	/// <param name="file"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<ActionResult> PostSingleFile()
	{
        IFormFile fileDetails = Request.Form.Files[0];
       

        if (fileDetails == null)
		{
			return BadRequest();
		}
		try
		{
			var createdArquivos = await _service.ArquivoService.PostFileAsync(fileDetails);
			

			return CreatedAtRoute("DownloadFile", new { id = createdArquivos.Id }, createdArquivos);
			//return Ok();
		}
		catch (Exception ex)
		{
			throw;
		}

	}

	/// <summary>
	/// Multiple File Upload
	/// </summary>
	/// <param name="file"></param>
	/// <returns></returns>
	[HttpPost("PostMultipleFile")]
	public async Task<ActionResult> PostMultipleFile([FromForm] List<FileUploadModel> fileDetails)
	{
		if (fileDetails == null)
		{
			return BadRequest();
		}

		try
		{
			await _service.ArquivoService.PostMultiFileAsync(fileDetails);
			return Ok();
		}
		catch (Exception)
		{
			throw;
		}
	}

	/// <summary>
	/// Download File
	/// </summary>
	/// <param name="file"></param>
	/// <returns></returns>
	//[HttpGet("DownloadFile")]
	[HttpGet("DownloadFile/{id:guid}", Name = "DownloadFile")]
	public async Task<ActionResult> DownloadFile(Guid id)
	{
		if (id==null)
		{
			return BadRequest();
		}

		try
		{
			await _service.ArquivoService.DownloadFileById(id, trackChanges: false);
			return Ok();
		}
		catch (Exception)
		{
			throw;
		}
	}

}
