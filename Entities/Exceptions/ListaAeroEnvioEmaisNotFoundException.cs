namespace Entities.Exceptions;

public class ListaAeroEnvioEmaisNotFoundException : NotFoundException
{
	public ListaAeroEnvioEmaisNotFoundException()
		: base($"A lista de Envio de email solicitada não existe na Base de Dados.")
	{
	}
}
