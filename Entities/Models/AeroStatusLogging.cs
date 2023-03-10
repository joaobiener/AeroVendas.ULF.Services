using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Metrics;

namespace Entities.Models;


[Table("AERO_STATUS_LOGGING", Schema = "UNIMEDLF")]
public class AeroStatusLogging
{
	[Column("AERO_STATUS_LOGGING_ID")]
	[Key]
	public Guid Id { get; set; }
	[Column("STATUS")]
	public string? Status { get; set; }
	//[Column("STATUS_")]
	///* Campo utilizado para verificar o status do Envio
	// * 1. Por Enviar
	// * 2. Enviando
	// * 3. Enviado
	// * 4. Erro de Envio
	// */

	[Column("CRIADO")]
	public DateTime CriadoEm { get; set; }

	[ForeignKey("FK_AERO_SOLICITACAO_EMAIL_LOGGING")]
	[Column("AERO_SOLICITACAO_EMAIL_REFID")]
	public Guid AeroSolicitacaoEmailId { get; set; }

	[ForeignKey("FK_AERO_ENVIO_EMAIL_LOGGING_REFID")]
	[Column("AERO_ENVIO_EMAIL_REFID")]
	public Guid AeroEnvioEmailId { get; set; }
	public AeroEnvioEmail? AeroEnvioEmail { get; set; }

	public AeroSolicitacaoEmail? AeroSolicitacaoEmail { get; set; }


}
