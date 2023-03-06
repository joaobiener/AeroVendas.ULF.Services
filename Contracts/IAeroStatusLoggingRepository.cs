using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAeroStatusLoggingRepository
{
	Task<PagedList<AeroStatusLogging>> GetAllStatusAsync(
		ViewAeroVendasParameters viewAeroVendasParameters,
		bool trackChanges);


	Task<PagedList<AeroStatusLogging>> GetStatusByIdAsync(
		Guid aeroSolicitacaoId, 
		Guid aeroEnvioEmailId, 
		ViewAeroVendasParameters viewAeroVendasParameters, 
		bool trackChanges);

	void CreateStatusAsync(AeroStatusLogging aeroStatus);


}

