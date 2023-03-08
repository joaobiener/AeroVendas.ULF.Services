using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAeroSolicitacaoService
{
	Task<(IEnumerable<AeroSolicitacaoEmailDto> AeroSolicitacao, MetaData metaData)>  GetAllAeroSolicitacaoAsync(
		ViewAeroVendasParameters viewAeroVendasParameters,
		bool trackChanges);
	
	Task<AeroSolicitacaoEmailDto> GetAeroSolicitacaoByIdAsync(Guid aeroSolicitacaoId, bool trackChanges);
	Task<AeroSolicitacaoEmailDto> CreateAeroSolicitacaoAsync(AeroSolicitacaoEmailForCreationDto aeroSolicitacao);
	Task<(AeroSolicitacaoEmailForUpdateDto aeroSolicitacaoToPatch, AeroSolicitacaoEmail aeroSolicitacaoEntity)> GetAeroSolicitacaoForPatchAsync(
				Guid solicitacaoId, bool solicTrackChanges);
	
	Task DeleteAeroSolicitacaoAsync(Guid aeroSolicitacaoId, bool trackChanges);
	Task UpdateAeroSolcitacaoAsync(Guid aeroSolicitacaoId, AeroSolicitacaoEmailForUpdateDto aeroSolicitacoForUpdate, bool trackChanges);
	Task SaveChangesForPatchAsync(AeroSolicitacaoEmailForUpdateDto aeroSolicitacaoToPatch, AeroSolicitacaoEmail aeroSolicitacaooEntity);
}
