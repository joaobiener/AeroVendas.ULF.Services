using System.ComponentModel.DataAnnotations;
namespace Shared.DataTransferObjects;

public abstract record AeroSolicitacaoForManipulationDto
{
	[Required(ErrorMessage = "Cidade é obrigatório.")]
	[MaxLength(100, ErrorMessage = "Cidade tem um tamanho máximo de 100 caracteres.")]
	public string? Cidade { get; init; }
	public int? TotalEnviado { get; init; }
	public string? UltimoStatus { get; init; }
	public DateTime? CriadoEm { get; init; } = DateTime.Now;
	public string? CriadoPor { get; init; }
	public DateTime? ModificadoEm { get; init; }
	public string? ModificadoPor { get; init; }
	public Guid MensagemHtmlId { get; init; }

}
