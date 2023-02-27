namespace Entities.Exceptions;

public sealed class AeroSolicitacaoNotFoundExceptionAll : NotFoundException
{
	public AeroSolicitacaoNotFoundExceptionAll()
		: base($"Não existem Solicitacaos cadastradas no Banco de Dados.")
	{
	}
}
