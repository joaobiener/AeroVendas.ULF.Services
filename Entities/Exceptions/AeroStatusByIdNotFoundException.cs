namespace Entities.Exceptions;

public sealed class AeroStatusByIdNotFoundException : NotFoundException
{
	public AeroStatusByIdNotFoundException(Guid statusId)
		: base($"O Status com o id: {statusId} não existe na Base de Dados.")
	{
	}
}
