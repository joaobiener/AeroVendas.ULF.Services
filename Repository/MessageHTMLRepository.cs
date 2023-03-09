using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System.Linq.Dynamic.Core;

namespace Repository;

internal sealed class MessageHTMLRepository : RepositoryBase<MensagemHtml>, IMessageHTMLRepository
{

	public MessageHTMLRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	//public async Task<IEnumerable<MensagemHtml>> GetAllMessagesAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges) =>
	//	await FindAll(trackChanges)
	//	.OrderByDescending(c => c.CriadoEm)
	//	.ToListAsync();
	public async Task<PagedList<MensagemHtml>> GetAllMessagesAsync(ViewAeroVendasParameters viewLogAeroVendasParameters, bool trackChanges)
	{

		var mensagens = await FindByCondition(x =>
								   x.Titulo != null
								  , trackChanges)
								  .Search(viewLogAeroVendasParameters.SearchTerm)
								.Sort(viewLogAeroVendasParameters.OrderBy)
								.ToListAsync();


        return PagedList<MensagemHtml>
			   .ToPagedList(mensagens,
							viewLogAeroVendasParameters.PageNumber,
							viewLogAeroVendasParameters.PageSize);
	}

	public async Task<MensagemHtml> GetMessageAsync(Guid? mensagemId, bool trackChanges) =>
		await FindByCondition(c => c.Id.Equals(mensagemId), trackChanges)
		.SingleOrDefaultAsync();

	public void CreateMessage(MensagemHtml mensagem) => Create(mensagem);

	public async Task<IEnumerable<MensagemHtml>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
		await FindByCondition(x => ids.Contains(x.Id), trackChanges)
		.ToListAsync();

	public void DeleteMessage(MensagemHtml mensagem) => Delete(mensagem);


}
