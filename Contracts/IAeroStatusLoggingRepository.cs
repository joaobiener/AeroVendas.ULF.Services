using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAeroStatusLoggingRepository
{
	Task<PagedList<AeroSolicitacaoEmail>> GetAllStatusAsync(
		ViewAeroVendasParameters viewAeroVendasParameters,
		bool trackChanges);

	Task<AeroStatusLoggingDto> GetStatusSolicitacaoByIdAsync(Guid aeroSolicitacaoId, bool trackChanges);
	Task<AeroStatusLoggingDto> GetStatusEnvioEmailByIdAsync(Guid aeroEnvioEmailId, bool trackChanges);
	Task<AeroStatusLoggingDto> CreateStatusAsync(AeroStatusLoggingForCreationDto aeroStatus);


}

