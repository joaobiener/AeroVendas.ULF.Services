using System.ComponentModel.DataAnnotations;
namespace Shared.DataTransferObjects;

public abstract record AeroEnvioEmailForManipulationDto
{
	[Required(ErrorMessage = "Cidade é obrigatório.")]
	[MaxLength(100, ErrorMessage = "Cidade tem um tamanho máximo de 100 caracteres.")]
	public string? Cidade { get; init; }
	public string? CodigoContrato { get; init; }

	public string? CodigoBeneficiario { get; init; }

	public string? NomeBeneficiario { get; init; }

	public string? EmailBeneficiario { get; init; }

	public double PremioAtual { get; init; }
	public int? NumeroDependentes { get; init; }
	public string? RespostaEnvio { get; init; } = "Sem Resposta";

	public int? NumProtocolo { get; init; }
	public string? MensagemEmailHtml { get; init; }
	public string? UltimoStatus { get; init; } = "Por Enviar";
	public DateTime CriadoEm { get; init; } = DateTime.Now;
	public string? CriadoPor { get; init; }
	public DateTime? ModificadoEm { get; init; }
	public string? ModificadoPor { get; init; }


}
