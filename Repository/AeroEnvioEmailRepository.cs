using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository;

internal sealed class AeroEnvioEmailRepository : RepositoryBase<AeroEnvioEmail>, IAeroEnvioEmailRepository
{
	
	public AeroEnvioEmailRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public async Task<PagedList<AeroEnvioEmail>> GetAllAeroEnvioEmailAsync(
			Guid aeroSolicitacaoId,
			string status,
			ViewAeroVendasParameters viewAeroVendasParameters, 
			bool trackChanges)
	{


		var aeroEnvioEmail = await FindByCondition(e => 
							(e.AeroSolicitacaoEmailId.Equals(aeroSolicitacaoId)) && 
						    (status == null || e.UltimoStatus == status),
					trackChanges)
					.Search(viewAeroVendasParameters.SearchTerm)
					.Sort(viewAeroVendasParameters.OrderBy)
					.ToListAsync();

		return PagedList<AeroEnvioEmail>
			   .ToPagedList(aeroEnvioEmail,
							viewAeroVendasParameters.PageNumber,
							viewAeroVendasParameters.PageSize);

	}

	public async Task<AeroEnvioEmail> GetAeroEnvioEmailAsync(Guid aeroSolicitacaoId, Guid id, bool trackChanges) =>
		await FindByCondition(e => e.AeroSolicitacaoEmailId.Equals(aeroSolicitacaoId) && e.Id.Equals(id), trackChanges)
		.SingleOrDefaultAsync();

	public void CreateEnvioEmailForSolicitacao(Guid aeroSolicitacoId, AeroEnvioEmail aeroEnvioEmail)
	{
		aeroEnvioEmail.AeroSolicitacaoEmailId = aeroSolicitacoId;
		Create(aeroEnvioEmail);
	}

	public async void DeleteAeroSolicitacao(AeroEnvioEmail aeroEnvioEmail)=> Delete(aeroEnvioEmail);

	public void bulkInsertEnvioEmailForSolicitacao(IEnumerable<AeroEnvioEmail> contratosSemAero) =>BulkInsert(contratosSemAero);


}