namespace Entities.Exceptions;

public sealed class MensagemHtmlNotFoundExceptionAll : NotFoundException
{
	public MensagemHtmlNotFoundExceptionAll()
		: base($"Não existem mensagens cadastradas no Banco de Dados.")
	{
	}
}
