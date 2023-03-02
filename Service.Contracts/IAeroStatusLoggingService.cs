using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAeroStatusLoggingService
{
	Task<(IEnumerable<AeroStatusLoggingDto> AeroStatus, MetaData metaData)>  GetAllStatusAsync(
		ViewAeroVendasParameters viewAeroVendasParameters,
		bool trackChanges);
	
	Task<AeroStatusLoggingDto> GetStatusSolicitacaoByIdAsync(Guid aeroSolicitacaoId, bool trackChanges);
	Task<AeroStatusLoggingDto> GetStatusEnvioEmailByIdAsync(Guid aeroSolicitacaoId, bool trackChanges);
	Task<AeroStatusLoggingDto> CreateStatusAsync(AeroStatusLoggingForCreationDto aeroStatus);

}
