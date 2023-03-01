using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailForCreationDto : AeroSolicitacaoForManipulationDto
{
	public IEnumerable<AeroEnvioEmailForCreationDto>? AeroEnvioEmails { get; init; }
}



