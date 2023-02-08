using AeroVendas.ULF.Services.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
namespace CompanyEmployees.Presentation.Controllers;

[Route("mensagemHtml")]
[ApiController]
public class MensagemHtmlController : ControllerBase
{
	private readonly IServiceManager _service;

	public MensagemHtmlController(IServiceManager service) => _service = service;

	[HttpGet]
	public async Task<IActionResult> GetMensagensHtml()
	{
		var mensagens = await _service.MensagemHtmlService.GetAllMessagesAsync(trackChanges: false);

		return Ok(mensagens);
	}

	[HttpGet("{id:guid}", Name = "MensagemById")]
	public async Task<IActionResult> GetMensagem(Guid id)
	{
		var mensagem = await _service.MensagemHtmlService.GetMensagemByIdAsync(id, trackChanges: false);
		return Ok(mensagem);
	}

	[HttpGet("mensagem/({ids})", Name = "mensagemCollection")]
	public async Task<IActionResult> GetMensagemCollection
		([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
	{
		var mensagens = await _service.MensagemHtmlService.GetByIdsAsync(ids, trackChanges: false);

		return Ok(mensagens);
	}

    [HttpPost("CreateMessageHTML")]
    public async Task<IActionResult> CreateMensagem([FromBody] MensagemHtmlForCreationDto mensagem)
	{
		if (mensagem is null)
			return BadRequest("MensagemHtmlForCreationDto é nula");

		if (!ModelState.IsValid)
			return UnprocessableEntity(ModelState);

		var createdMensagem = await _service.MensagemHtmlService.CreateMensagemAsync(mensagem);

		return CreatedAtRoute("MensagemById", new { id = createdMensagem.Id }, createdMensagem);
	}

	[HttpPost("collection")]
	public async Task<IActionResult> CreateMensagemCollection
		([FromBody] IEnumerable<MensagemHtmlForCreationDto> MensagemCollection)
	{
		var result = await _service.MensagemHtmlService.CreateMensagemCollectionAsync(MensagemCollection);

		return CreatedAtRoute("mensagemCollection", new { result.ids }, result.mensagens);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteMensagem(Guid id)
	{
		await _service.MensagemHtmlService.DeleteMensagemAsync(id, trackChanges: false);

		return NoContent();
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateMensagem(Guid id, [FromBody] MensagemForUpdateDto mensagem)
	{
		if (mensagem is null)
            return BadRequest("MensagemForUpdateDto é nulo");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.MensagemHtmlService.UpdateMensagemAsync(id, mensagem, trackChanges: true);

        return NoContent();
    }
}
