namespace Entities.Exceptions;

public sealed class MensagemHtmlNotFoundException : NotFoundException
{
	public MensagemHtmlNotFoundException(Guid mensagemId)
		: base($"A Mensagem com o  id: {mensagemId} não existe no Banco de Dados.")
	{
	}
}
