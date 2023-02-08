using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

internal sealed class MessageHTMLRepository : RepositoryBase<MensagemHtml>, IMessageHTMLRepository
{
	public MessageHTMLRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public async Task<IEnumerable<MensagemHtml>> GetAllMessagesAsync(bool trackChanges) =>
		await FindAll(trackChanges)
		.OrderBy(c => c.Id)
		.ToListAsync();

	public async Task<MensagemHtml> GetMessageAsync(Guid mensagemId, bool trackChanges) =>
		await FindByCondition(c => c.Id.Equals(mensagemId), trackChanges)
		.SingleOrDefaultAsync();

	public void CreateMessage(MensagemHtml mensagem) => Create(mensagem);

	public async Task<IEnumerable<MensagemHtml>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
		await FindByCondition(x => ids.Contains(x.Id), trackChanges)
		.ToListAsync();

	public void DeleteMessage(MensagemHtml mensagem) => Delete(mensagem);

   
   
}
