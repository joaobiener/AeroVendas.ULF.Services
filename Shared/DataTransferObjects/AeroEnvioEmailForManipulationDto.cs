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

	public long? NumProtocolo { get; init; }
	public string? MensagemEmailHtml { get; init; }
	public string? UltimoStatus { get; init; }
	public DateTime CriadoEm { get; init; }
	public int AeroSolicitacaoEmailRefId { get; init; }

}
