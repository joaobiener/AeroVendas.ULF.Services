using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Linq.Dynamic.Core;

namespace Repository;

internal sealed class AeroSolicitacaoRepository : RepositoryBase<AeroSolicitacaoEmail>, IAeroSolicitacaoRepository
{

	public AeroSolicitacaoRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public async Task<PagedList<AeroSolicitacaoEmail>> GetAllAeroSolicitacaAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
	{
		throw new NotImplementedException();
	}

	public async Task<AeroSolicitacaoEmail> GetAeroSolicitacaAsync(Guid aeroSolicitacaoId, bool trackChanges)
	{
		throw new NotImplementedException();
	}

	public void CreateAeroSolicitacao(AeroSolicitacaoEmailForCreationDto aeroSolicitacao)
	{
		throw new NotImplementedException();
	}

	public void DeleteAeroSolicitacao(Guid aeroSolicitacaoId)
	{
		throw new NotImplementedException();
	}

}
