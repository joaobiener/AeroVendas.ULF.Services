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
	[Column("Status")]
	public string? Status { get; set; }
	
	[Column("CRIADO")]
	public DateTime CriadoEm { get; set; }

	[ForeignKey(nameof(AeroEnvioEmail))]
	public int AeroEnvioEmailId { get; set; }
	public AeroEnvioEmail? AeroEnvioEmail { get; set; }


	[ForeignKey(nameof(AeroSolicitacaoEmail))]
	public int AeroSolicitacaoEmailId { get; set; }
	public AeroSolicitacaoEmail? AeroSolicitacaoEmail { get; set; }


}
