namespace Entities.Exceptions;

public sealed class AeroVendasByUserNotFoundException : NotFoundException
{
	public AeroVendasByUserNotFoundException(string login, int AeroVendasid)
		: base($"LogAeroVendas com o login: {login} e o AeroVendasId: {AeroVendasid} não existe na base de dados.")
	{
	}
}
