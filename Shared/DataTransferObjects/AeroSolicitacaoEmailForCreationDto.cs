using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailForCreationDto : AeroSolicitacaoForManipulationDto
{
	public AeroStatusLoggingForCreationDto? AeroStatusLogging { get; init; }
	public IEnumerable<AeroEnvioEmailForCreationDto>? AeroEnvioEmails { get; init; }

}



