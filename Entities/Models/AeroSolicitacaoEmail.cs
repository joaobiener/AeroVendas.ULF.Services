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
	//[Column("STATUS_ENVIO")]
	///* Campo utilizado para verificar o status do Envio
	// * 1. Por Enviar
	// * 2. Enviando
	// * 3. Enviado
	// * 4. Erro de Envio
	// */

	[Column("CRIADO")]
	public DateTime CriadoEm { get; set; }
	[Column("CRIADO_POR")]
	public string? CriadoPor { get; set; }
	[Column("MODIFICADO")]
	public DateTime ModificadoEm { get; set; }
	[Column("MODIFICADO_POR")]
	public string? ModificadoPor { get; set; }

	//Foreign key para Texto da Mensagem
	[ForeignKey("FK_MENSAGEMHTML")]
	[Column("MENSAGEMHTML_REFID")]
	public Guid MensagemHtmlId { get; set; }
	public MensagemHtml? MensagemHtml { get; set; }

	public ICollection<AeroEnvioEmail> AeroEnvioEmails { get; set; }

	public ICollection<AeroStatusLogging> AeroStatusLoggings { get; set; }

}
