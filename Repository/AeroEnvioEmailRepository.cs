using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.ComponentModel.Design;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository;

internal sealed class AeroEnvioEmailRepository : RepositoryBase<AeroEnvioEmail>, IAeroEnvioEmailRepository
{

	public AeroEnvioEmailRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public async Task<PagedList<AeroEnvioEmail>> GetAllAeroEnvioEmailAsync(Guid aeroSolicitacaoId, ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
	{
		var aeroEnvioEmail = await FindByCondition(x =>
				   x.Id != null
				  , trackChanges)
				  .Search(viewAeroVendasParameters.SearchTerm)
				.Sort(viewAeroVendasParameters.OrderBy)
				.ToListAsync();

		return PagedList<AeroEnvioEmail>
			   .ToPagedList(aeroEnvioEmail,
							viewAeroVendasParameters.PageNumber,
							viewAeroVendasParameters.PageSize);

	}

	public async Task<AeroEnvioEmail> GetAeroEnvioEmailAsync(Guid aeroSolicitacaoId, Guid id, bool trackChanges) =>
		await FindByCondition(e => e.AeroSolicitacaoEmailRefId.Equals(aeroSolicitacaoId) && e.Id.Equals(id), trackChanges)
		.SingleOrDefaultAsync();

	public void CreateEnvioEmailForSolicitacao(Guid aeroSolicitacoId, AeroEnvioEmail aeroEnvioEmail)
	{
		aeroEnvioEmail.AeroSolicitacaoEmailRefId = aeroSolicitacoId;
		Create(aeroEnvioEmail);
	}

	public async void DeleteAeroSolicitacao(AeroEnvioEmail aeroEnvioEmail)=> Delete(aeroEnvioEmail);

}