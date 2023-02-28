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
	Task DeleteAeroSolicitacaoAsync(Guid aeroSolicitacaoId, bool trackChanges);
	Task UpdateMensagemAsync(Guid aeroSolicitacaoId, MensagemForUpdateDto aeroSolicitacoForUpdate, bool trackChanges);

}
