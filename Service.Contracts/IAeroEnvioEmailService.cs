using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAeroEnvioEmailService
{

	Task<(IEnumerable<AeroEnvioEmailDto> aeroEnvioEmail, MetaData metaData)> GetAllAeroEnvioEmailAsync(Guid aeroSolicitacaoId,
			ViewAeroVendasParameters viewAeroVendasParameters,
			bool trackChanges);
	Task<AeroEnvioEmailDto> GetAeroEnvioEmailAsync(Guid aeroSolicitacaoId, Guid id, bool trackChanges);
	Task<AeroEnvioEmailDto> CreateAeroEnvioEmailForSolicitacaoAsync(Guid aeroSolicitacaoId,
		AeroEnvioEmailForCreationDto aeroEnvioEmailForCreation, bool trackChanges);
	Task DeleteAeroEnvioEmailForSolicitacaoAsync(Guid solicitacaoId, Guid id, bool trackChanges);
	Task UpdateAeroEnvioEmailForSolicitacaoAsync(Guid solicitacaoId, Guid id,
		AeroEnvioEmailForUpdateDto aeroEnvioEmailForUpdate, bool solicTrackChanges, bool envioTrackChanges);
	Task<(AeroEnvioEmailForUpdateDto aeroEnvioEmailToPatch, AeroEnvioEmail aeroEnvioEntity)> GetAeroEnvioForPatchAsync(
		Guid solicitacaoId, Guid id, bool solicTrackChanges, bool envioTrackChanges);
	Task SaveChangesForPatchAsync(AeroEnvioEmailForUpdateDto aeroEnvioEmailToPatch, AeroEnvioEmail aeroEnvioEntity);

	Task BulkInsertAeroEnvioEmailForSolicitacaoAsync(AeroSolicitacaoEmailDto aeroSolicitacao);

	Task<int> ProcessaEnvioEmailForSolicitacaoAsync(AeroSolicitacaoEmail aeroSolicitacao);
}
