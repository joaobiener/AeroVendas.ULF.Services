namespace Entities.Exceptions;

public sealed class AeroStatusLoggingNotFoundExceptionAll : NotFoundException
{
	public AeroStatusLoggingNotFoundExceptionAll()
		: base($"Não existem Status cadastradas no Banco de Dados.")
	{
	}
}
