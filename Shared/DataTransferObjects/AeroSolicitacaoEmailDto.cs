using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailDto {
	public Guid Id { get; init; }
	public string? Cidade { get; init; }
	public int? TotalEnviado { get; init; }
	public string? UltimoStatus { get; init; }
	public DateTime? CriadoEm { get; init; }
	public Guid? MensagemHtmlRefId { get; init; }
}

