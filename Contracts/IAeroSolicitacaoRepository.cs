using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAeroSolicitacaoRepository
{
	Task<PagedList<AeroSolicitacaoEmail>> GetAllAeroSolicitacaAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges);
	Task<AeroSolicitacaoEmail> GetAeroSolicitacaAsync(Guid aeroSolicitacaoId, bool trackChanges);
	void CreateAeroSolicitacao(AeroSolicitacaoEmail AeroSolicitacaoEmail);
	void DeleteAeroSolicitacao(AeroSolicitacaoEmail AeroSolicitacaoEmail);

}
