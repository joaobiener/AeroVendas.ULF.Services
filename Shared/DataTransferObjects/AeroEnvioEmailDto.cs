using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroEnvioEmailDto
{
	public Guid Id { get; init; }
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

