using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailForCreationDto : AeroSolicitacaoForManipulationDto
{
	public IEnumerable<AeroEnvioEmailForCreationDto>? Employees { get; init; }
}



