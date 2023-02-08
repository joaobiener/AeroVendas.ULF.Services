namespace Entities.Exceptions;

public sealed class MensagemHtmlCollectionBadRequest : BadRequestException
{
	public MensagemHtmlCollectionBadRequest()
		: base("Coleção de Mensagens enviadas pelo cliente é nula.")
	{
	}
}
