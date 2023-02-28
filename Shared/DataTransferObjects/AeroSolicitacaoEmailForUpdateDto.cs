using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailForUpdateDto : AeroSolicitacaoForManipulationDto
{
	public IEnumerable<AeroEnvioEmailForCreationDto>? Employees { get; init; }
}