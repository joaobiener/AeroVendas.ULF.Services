namespace Entities.Exceptions;

public sealed class AeroSolicitacaoNotFoundException : NotFoundException
{
	public AeroSolicitacaoNotFoundException(Guid SolicitacaoId)
		: base($"A solicitação com o id: {SolicitacaoId} não existe na Base de Dados.")
	{
	}
}
