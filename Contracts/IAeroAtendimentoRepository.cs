using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAeroAtendimentoRepository
{
	Task<PagedList<AeroAtendimentoEnvioEmail>> GetAllAtendimentoAsync(AeroAtendimentoEnvioEmail viewAeroVendasParameters, bool trackChanges);
	Task<AeroAtendimentoEnvioEmail> GetAtendimentoByIdAsync(Guid? atendimentoId, bool trackChanges);
	void CreateMessage(AeroAtendimentoEnvioEmail mensagem);
	void DeleteMessage(AeroAtendimentoEnvioEmail mensagem);

}
