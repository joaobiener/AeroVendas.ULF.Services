using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Metrics;

namespace Entities.Models;

[Table("AERO_SOLICITACAO_EMAIL", Schema = "UNIMEDLF")]
public class AeroSolicitacaoEmail
{
	[Column("AERO_SOLICITACAO_EMAIL_ID")]
	[Key]
	public Guid Id { get; set; }

	[Column("CIDADE")]
	public string? Cidade { get; set; }

	[Column("TOTAL_ENVIO")]
	public int? TotalEnviado { get; set; }

	[Column("ULTIMO_STATUS")]
	public string? UltimoStatus { get; set; }

	[Column("CRIADO")]
	public DateTime CriadoEm { get; set; }

	//Foreign key para Texto da Mensagem
	[ForeignKey("FK_MENSAGEMHTML")]
	[Column("MENSAGEMHTML_REFID")]
	public Guid MensagemHtmlRefId { get; set; }
	public MensagemHtml? MensagemHtml { get; set; }

	public ICollection<AeroEnvioEmail> AeroEnvioEmails { get; set; }

	public ICollection<AeroStatusLogging> AeroStatusLoggings { get; set; }

}
