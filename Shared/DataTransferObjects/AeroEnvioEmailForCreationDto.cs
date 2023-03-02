namespace Shared.DataTransferObjects;

public record AeroEnvioEmailForCreationDto : AeroEnvioEmailForManipulationDto
{
	public AeroStatusLoggingForCreationDto? AeroStatusLogging { get; init; }
}
