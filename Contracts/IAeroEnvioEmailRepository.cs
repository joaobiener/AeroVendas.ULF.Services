using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAeroEnvioEmailRepository
{
	Task<PagedList<AeroEnvioEmail>> GetAllAeroEnvioEmailAsync(Guid aeroSolicitacaoId, 
				ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges);

	Task<AeroEnvioEmail> GetAeroEnvioEmailAsync(Guid aeroSolicitacaoId, Guid id, bool trackChanges);
	void CreateEnvioEmailForSolicitacao(Guid aeroSolicitacoId, AeroEnvioEmail aeroEnvioEmail);
	void DeleteAeroSolicitacao(AeroEnvioEmail aeroEnvioEmail);

	void bulkInsertEnvioEmailForSolicitacao(Guid aeroSolicitacaoId, IEnumerable<AeroEnvioEmail> contratosSemAero);

}
