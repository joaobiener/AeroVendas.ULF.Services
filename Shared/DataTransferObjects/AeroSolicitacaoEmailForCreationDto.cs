using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailForCreationDto : AeroSolicitacaoForManipulationDto
{
	public IEnumerable<AeroStatusLoggingForCreationDto>? AeroStatusLoggings { get; init; }
	public IEnumerable<AeroEnvioEmailForCreationDto>? AeroEnvioEmails { get; init; }

}



