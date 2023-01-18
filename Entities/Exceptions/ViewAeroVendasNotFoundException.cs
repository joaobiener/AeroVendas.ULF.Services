using Entities.Models;

namespace Entities.Exceptions;

public sealed class ViewAeroVendasNotFoundException : NotFoundException
{
	public ViewAeroVendasNotFoundException()
		: base($"LogAeroVendas com a pesquisa fornecida não existe na base de dados.")
	{
	}
}
