using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailDto {
	public Guid Id { get; set; }
	public string? Cidade { get; set; }
	public int? TotalEnviado { get; set; }
	public string? UltimoStatus { get; set; }
	public DateTime? CriadoEm { get; set; }
	public string? CriadoPor { get; set; }
	public DateTime? ModificadoEm { get; set; }
	public string? ModificadoPor{ get; set; }
	public Guid? MensagemHtmlId { get; set; }
	
}
