namespace Shared.DataTransferObjects;

public record AeroEnvioEmailForCreationDto : AeroEnvioEmailForManipulationDto
{
	public IEnumerable<AeroStatusLoggingForCreationDto>? AeroStatusLoggings { get; init; }
}
