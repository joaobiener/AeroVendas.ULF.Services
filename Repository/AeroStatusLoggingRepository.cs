using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Diagnostics.Contracts;
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


	public async Task<PagedList<AeroStatusLogging>> GetStatusByIdAsync(
		Guid? aeroSolicitacaoId, 
		Guid? aeroEnvioEmailId, 
		ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)

	
	{
		
		
		var aeroStatus = await FindByCondition(x =>
								   (aeroSolicitacaoId == null || x.AeroSolicitacaoEmailId.Equals(aeroSolicitacaoId)) &&
								   (aeroEnvioEmailId == null || x.AeroEnvioEmailId.Equals(aeroEnvioEmailId)) 
								   , trackChanges)
								   .Search(viewAeroVendasParameters.SearchTerm)
								   .Sort(viewAeroVendasParameters.OrderBy)
								   .ToListAsync();
		return PagedList<AeroStatusLogging>
			   .ToPagedList(aeroStatus,
							viewAeroVendasParameters.PageNumber,
							viewAeroVendasParameters.PageSize);
	}

	public async Task<PagedList<AeroStatusLogging>> GetStatusBySolicitacaoIdAsync(
						Guid? aeroSolicitacaoId,
						ViewAeroVendasParameters viewAeroVendasParameters,
						bool trackChanges)
	{

		var aeroStatus = await FindByCondition(x =>
								   (x.AeroSolicitacaoEmailId.Equals(aeroSolicitacaoId)) &&
								   (x.AeroEnvioEmailId==Guid.Empty)
								   , trackChanges)
								   .Search(viewAeroVendasParameters.SearchTerm)
								   .Sort(viewAeroVendasParameters.OrderBy)
								   .ToListAsync();
		return PagedList<AeroStatusLogging>
			   .ToPagedList(aeroStatus,
							viewAeroVendasParameters.PageNumber,
							viewAeroVendasParameters.PageSize);
	}

	public async Task<PagedList<AeroStatusLogging>> GetStatusByEnvioEmailIdAsync(
			  Guid? aeroEnvioEmailId,
			  ViewAeroVendasParameters viewAeroVendasParameters,
			  bool trackChanges)
	{
		var aeroStatus = await FindByCondition(x =>
								   (x.AeroEnvioEmailId.Equals(aeroEnvioEmailId))
								   , trackChanges)
								   .Search(viewAeroVendasParameters.SearchTerm)
								   .Sort(viewAeroVendasParameters.OrderBy)
								   .ToListAsync();
		return PagedList<AeroStatusLogging>
			   .ToPagedList(aeroStatus,
							viewAeroVendasParameters.PageNumber,
							viewAeroVendasParameters.PageSize);
	}

	public void CreateStatusAsync(AeroStatusLogging aeroStatus) => Create(aeroStatus);


	public async Task<AeroStatusLogging> GetAeroStatusByIdAsync(Guid aeroStatusById, bool trackChanges) =>
			await FindByCondition(c => c.Id.Equals(aeroStatusById), trackChanges)
			.SingleOrDefaultAsync();

	public void bulkInsertEnvioEmailLogs(IEnumerable<AeroStatusLogging> contratosSemAero) => BulkInsert(contratosSemAero);

	
}