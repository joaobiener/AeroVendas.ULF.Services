using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Contracts;

public interface IMessageHTMLRepository
{
	Task<PagedList<MensagemHtml>> GetAllMessagesAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges);
	Task<MensagemHtml> GetMessageAsync(Guid mensagemId, bool trackChanges);
	void CreateMessage(MensagemHtml mensagem);
	Task<IEnumerable<MensagemHtml>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	void DeleteMessage(MensagemHtml mensagem);

}
