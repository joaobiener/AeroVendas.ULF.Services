namespace Entities.Exceptions;

public class AeroEnvioEmailNotFoundException : NotFoundException
{
	public AeroEnvioEmailNotFoundException(Guid envioEmailId)
		: base($"o Envio com o id: {envioEmailId} não existe na Base de Dados.")
	{
	}
}
