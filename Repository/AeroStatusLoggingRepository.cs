using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository;

internal sealed class AeroStatusLoggingRepository : RepositoryBase<AeroStatusLogging>, IAeroStatusLoggingRepository
{

	public AeroStatusLoggingRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public async Task<PagedList<AeroStatusLogging>> GetAllStatusAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
	{
		var aeroStatus = await FindByCondition(x =>
						   x.Id != null
						  , trackChanges)
						  .Search(viewAeroVendasParameters.SearchTerm)
						.Sort(viewAeroVendasParameters.OrderBy)
						.ToListAsync();

		return PagedList<AeroStatusLogging>
			   .ToPagedList(aeroStatus,
							viewAeroVendasParameters.PageNumber,
							viewAeroVendasParameters.PageSize);
	}

	public async Task<AeroStatusLogging> GetStatusSolicitacaoByIdAsync(Guid aeroSolicitacaoId, bool trackChanges) =>
			await FindByCondition(c => c.Id.Equals(aeroSolicitacaoId), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<AeroStatusLogging> GetStatusEnvioEmailByIdAsync(Guid aeroEnvioEmailId, bool trackChanges) =>
			await FindByCondition(c => c.Id.Equals(aeroEnvioEmailId), trackChanges)
			.SingleOrDefaultAsync();


	public void CreateStatusAsync(AeroStatusLogging aeroStatus) => Create(aeroStatus);
}