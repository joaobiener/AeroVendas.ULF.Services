using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAeroStatusLoggingService
{
	Task<(IEnumerable<AeroStatusLoggingDto> AeroStatus, MetaData metaData)>  GetAllStatusAsync(
		ViewAeroVendasParameters viewAeroVendasParameters,
		bool trackChanges);

	Task<(IEnumerable<AeroStatusLoggingDto> AeroStatus, MetaData metaData)> GetStatusByIdAsync(
		Guid? aeroSolicitacaoId, 
		Guid? aeroEnvioEmailId,
		ViewAeroVendasParameters viewAeroVendasParameters,
		bool trackChanges);

	Task<AeroStatusLoggingDto> GetAeroStatusByIdAsync(Guid aeroStatusId, bool trackChanges);
	Task<AeroStatusLoggingDto> CreateStatusAsync(AeroStatusLoggingForCreationDto aeroStatus);


}
