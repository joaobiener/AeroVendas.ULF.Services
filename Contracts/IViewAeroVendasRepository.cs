using Contracts;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;
public interface IViewAeroVendasRepository
{
  
    Task<PagedList<ViewContratoSemAeroVendas>> GetAllViewAeroVendasAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges);
	
	
	Task<PagedList<ViewContratoSemAeroVendas>> GetViewAeroVendasByAsync(
		string? Contrato, 
		string? CodigoBeneficiario, 
		string? NomeBeneficiario, 
		string? Cidade, ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges);
    Task<PagedList<string>> GetViewCidadeAeroVendas(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges);

}
