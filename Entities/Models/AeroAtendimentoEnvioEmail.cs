using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Metrics;

namespace Entities.Models;



[Table("AERO_ATENDIMENTO_ENVIO_EMAIL", Schema = "UNIMEDLF")]
public class AeroAtendimentoEnvioEmail
{
	[Column("AERO_ATENDIMENTO_ENVIO_EMAIL_ID")]
	[Key]
	public Guid Id { get; set; }

	[Column("NUM_PROTOCOLO")]
	public int NumProtocolo { get; set; }
	[Column("CRIADO")]
	public DateTime CriadoEm { get; set; }
	[Column("CRIADO_POR")]
	public string? CriadoPor { get; set; }

	[ForeignKey("FK_ATENDIMENTO_AERO_ENVIO__EMAIL_REFID")]
	[Column("AERO_ENVIO_EMAIL_REFID")]
	public Guid? AeroEnvioEmailId { get; set; }
	public AeroEnvioEmail? AeroSolicitacaoEmail { get; set; }



}
