using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DataTransferObjects;
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


	public async Task<Arquivo> DownloadFileById(Guid Id, bool trackChanges)
	{ 
	
		var arquivo = await FindByCondition(c => c.Id.Equals(Id), trackChanges)
		.SingleOrDefaultAsync();

		return arquivo;

		//return await FindByCondition(c => c.Id.Equals(Id), trackChanges)
		//.SingleOrDefaultAsync();

	}

	public async Task<PagedList<Arquivo>> GetAllArquivosAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
	{
		var arquivos = await FindByCondition(x =>
						   x.Nome != null
						  , trackChanges)
						  .Search(viewAeroVendasParameters.SearchTerm)
						.Sort(viewAeroVendasParameters.OrderBy)
						.ToListAsync();


		return PagedList<Arquivo>
			   .ToPagedList(arquivos,
							viewAeroVendasParameters.PageNumber,
							viewAeroVendasParameters.PageSize);
	}
}
