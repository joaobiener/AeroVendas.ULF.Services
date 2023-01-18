namespace Entities.Exceptions;

public sealed class AeroVendasNotFoundException : NotFoundException
{
	public AeroVendasNotFoundException(int logId)
		: base($"LogAeroVendas com o Id: {logId} não existe na base de dados.")
	{
	}
}
