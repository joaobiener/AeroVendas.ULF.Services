using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IViewAeroVendasService
{

    Task<(IEnumerable<ViewAeroVendasDto> viewAeroVendas, MetaData metaData)> GetAllViewsAeroVendasAsync(
        ViewAeroVendasParameters viewAeroVendasParameters, 
        bool trackChanges);

	Task<(IEnumerable<ViewAeroVendasDto> viewAeroVendas, MetaData metaData)> GetViewAeroVendasByAsync(
        string? Contrato, 
        string? CodigoBeneficiario, 
        string? NomeBeneficiario, 
        string? Cidade, 
        ViewAeroVendasParameters viewAeroVendasParameters, 
        bool trackChanges);
    Task<(IEnumerable<string> viewAeroVendas, MetaData metaData)> GetViewCidadeAeroVendasAsync(
    ViewAeroVendasParameters viewAeroVendasParameters,
    bool trackChanges);

}
