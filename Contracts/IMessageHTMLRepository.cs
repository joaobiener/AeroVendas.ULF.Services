using Entities.Models;

namespace Contracts;

public interface IMessageHTMLRepository
{
	Task<IEnumerable<MensagemHtml>> GetAllMessagesAsync(bool trackChanges);
	Task<MensagemHtml> GetMessageAsync(Guid mensagemId, bool trackChanges);
	void CreateMessage(MensagemHtml mensagem);
	Task<IEnumerable<MensagemHtml>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	void DeleteMessage(MensagemHtml mensagem);
}
