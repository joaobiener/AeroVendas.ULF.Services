using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System.Linq.Dynamic.Core;

namespace Repository;

internal sealed class ArquivoRepository : RepositoryBase<Arquivo>, IArquivoRepository
{

	public ArquivoRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	//public async Task<IEnumerable<MensagemHtml>> GetAllMessagesAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges) =>
	//	await FindAll(trackChanges)
	//	.OrderByDescending(c => c.CriadoEm)
	//	.ToListAsync();

	public void CreateArquivo(Arquivo arquivo) => Create(arquivo);


	public async Task<Arquivo> DownloadFileById(Guid Id, bool trackChanges) =>
	await FindByCondition(c => c.Id.Equals(Id), trackChanges)
	.SingleOrDefaultAsync();


}
