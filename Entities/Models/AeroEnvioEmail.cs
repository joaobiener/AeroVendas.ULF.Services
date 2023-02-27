﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Metrics;

namespace Entities.Models;


[Index(nameof(CodigoContrato), nameof(CodigoBeneficiario), "IX_CONTRATO_BENEFICIARIO")]
[Table("AERO_ENVIO_EMAIL", Schema = "UNIMEDLF")]
public class AeroEnvioEmail
{
	[Column("AERO_ENVIO_EMAIL_ID")]
	[Key]
	public Guid Id { get; set; }
	[Column("CIDADE")]
	public string? Cidade { get; set; }
	[Column("CODIGO_CONTRATO")]
	public string? CodigoContrato { get; set; }

	[Column("CODIGO_BENEFICIARIO")]
	public string? CodigoBeneficiario { get; set; }

	[Column("NOME_BENEFICIARIO")]
	public string? NomeBeneficiario { get; set; }

	[Column("EMAIL_BENEFICIARIO")]
	public string? EmailBeneficiario { get; set; }

	[Column("PREMIO_ATUAL")]
	public double PremioAtual { get; set; }

	

	[Column("NUMERO_DEPENDENTES")]
	public int? NumeroDependentes { get; set; }

	//[Column("STATUS_ENVIO")]
	///* Campo utilizado para verificar o status do Envio
	// * 1. Por Enviar
	// * 2. Enviando
	// * 3. Enviado
	// * 4. Erro de Envio
	// */
	//public string? StatusEnvio { get; set; } = "Por Enviar";

	[Column("RESPOSTA_ENVIO")]
	/* Campo utilizado para verificar o status do Envio
	 * 1. Sem Resposta
	 * 2. Aceite
	 * 3. Não Aceite
	 */
	public string? RespostaEnvio { get; set; } = "Sem Resposta";


	[Column("NUMERO_PROTOCOLO")]
	public long? NumProtocolo { get; set; }

	[Required(ErrorMessage = "A Mensagem é obrigatória!")]
	[Column("MENSAGEM_EMAIL_HTML")]
	public string? MensagemEmailHtml { get; set; }
	[Column("ULTIMO_STATUS")]
	public string? UltimoStatus { get; set; }

	[Column("CRIADO")]
	public DateTime CriadoEm { get; set; }

	[ForeignKey(nameof(AeroSolicitacaoEmail))]
	[Column("AERO_SOLICITACAO_EMAIL_REFID")]
	public int AeroSolicitacaoEmailRefId { get; set; }
	public AeroSolicitacaoEmail AeroSolicitacaoEmail { get; set; }

	public ICollection<AeroStatusLogging> AeroStatusLoggings { get; set; }
	//Foreign key para AeroStatusLoggin


}
