using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Repository;

internal sealed class ViewAeroVendasRepository : RepositoryBase<ViewContratoSemAeroVendas>, IViewAeroVendasRepository
{
	public ViewAeroVendasRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}
   

    public async Task<PagedList<ViewContratoSemAeroVendas>> GetAllViewAeroVendasAsync(ViewAeroVendasParameters viewLogAeroVendasParameters, bool trackChanges)
    {
        var viewLogsAeroVendass = await FindAll(trackChanges)
                // .OrderBy(e => e.Cidade).ThenBy(x => x.Contrato)
                 .ToListAsync();

        return PagedList<ViewContratoSemAeroVendas>
               .ToPagedList(viewLogsAeroVendass,
                            viewLogAeroVendasParameters.PageNumber,
                            viewLogAeroVendasParameters.PageSize);
    }

    //Busca as cidades associadas ao contrato
    public async Task<PagedList<string>> GetViewCidadeAeroVendas(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
    {
        var viewAeroVendas = await FindByCondition(x =>
                                       x.Cidade!= null
                                      , trackChanges)
                                    .Select(o => o.Cidade).Distinct()
                                    .OrderBy(x=>x)
									.ToListAsync();

        return PagedList<string>
               .ToPagedList(viewAeroVendas,
                            viewAeroVendasParameters.PageNumber,
                            viewAeroVendasParameters.PageSize);

    }

	public async Task<PagedList<ViewContratoSemAeroVendas>> GetViewAeroVendasByAsync(string? Contrato,
		                                                                             string? CodigoBeneficiario,
		                                                                             string? NomeBeneficiario,
		                                                                             string? Cidade,
																			         ViewAeroVendasParameters? viewAeroVendasParameters, 
                                                                                     bool trackChanges)
    {
        var viewAeroVendas =  await FindByCondition(x =>
                                    (Contrato == null || x.Contrato == Contrato) &&
                                    (CodigoBeneficiario == null || x.CodigoBeneficiario == CodigoBeneficiario) &&
                                    (NomeBeneficiario == null || x.NomeBeneficiario == NomeBeneficiario) &&
									(Cidade == null || x.Cidade == Cidade) 
									,trackChanges)
                                    .Search(viewAeroVendasParameters.SearchTerm)
                                    .Sort(viewAeroVendasParameters.OrderBy)
                                    .ToListAsync();
        return PagedList<ViewContratoSemAeroVendas>
               .ToPagedList(viewAeroVendas, 
                            viewAeroVendasParameters.PageNumber,
                            viewAeroVendasParameters.PageSize);

    }
    	
}