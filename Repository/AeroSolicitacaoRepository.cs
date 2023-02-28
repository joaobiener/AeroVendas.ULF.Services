using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository;

internal sealed class AeroSolicitacaoRepository : RepositoryBase<AeroSolicitacaoEmail>, IAeroSolicitacaoRepository
{

	public AeroSolicitacaoRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public async Task<PagedList<AeroSolicitacaoEmail>> GetAllAeroSolicitacaAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
	{
		var aeroSolicitacao = await FindByCondition(x =>
						   x.Id != null
						  , trackChanges)
						  .Search(viewAeroVendasParameters.SearchTerm)
						.Sort(viewAeroVendasParameters.OrderBy)
						.ToListAsync();

		return PagedList<AeroSolicitacaoEmail>
			   .ToPagedList(aeroSolicitacao,
							viewAeroVendasParameters.PageNumber,
							viewAeroVendasParameters.PageSize);
	}

	public async Task<AeroSolicitacaoEmail> GetAeroSolicitacaAsync(Guid aeroSolicitacaoId, bool trackChanges) =>
			await FindByCondition(c => c.Id.Equals(aeroSolicitacaoId), trackChanges)
			.SingleOrDefaultAsync();

	public void CreateAeroSolicitacao(AeroSolicitacaoEmail AeroSolicitacaoEmail) => Create(AeroSolicitacaoEmail);

	public void DeleteAeroSolicitacao(AeroSolicitacaoEmail AeroSolicitacaoEmail) => Delete(AeroSolicitacaoEmail);


}