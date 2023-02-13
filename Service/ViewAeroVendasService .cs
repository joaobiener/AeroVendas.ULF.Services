using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.ComponentModel.Design;

namespace Service;

internal sealed class ViewAeroVendasService : IViewAeroVendasService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ViewAeroVendasService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
	{
		_repository = repository;
		_logger = logger;
        _mapper = mapper;
	}



	public async Task<(IEnumerable<ViewAeroVendasDto> viewAeroVendas, MetaData metaData)> GetAllViewsAeroVendasAsync(
        ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)

	{
		var viewAeroVendasWithMetaData = await _repository.viewAeroVendas.GetAllViewAeroVendasAsync(viewAeroVendasParameters, trackChanges);

		if (viewAeroVendasWithMetaData is null)
			throw new ViewAeroVendasNotFoundException();

		var viewAeroVendasDto = _mapper.Map<IEnumerable<ViewAeroVendasDto>>(viewAeroVendasWithMetaData);

		return (viewAeroVendas: viewAeroVendasDto, metaData: viewAeroVendasWithMetaData.MetaData);

	}

	public async Task<(IEnumerable<ViewAeroVendasDto> viewAeroVendas, MetaData metaData)> GetViewAeroVendasByAsync(
						string? Contrato, 
						string? CodigoBeneficiario, 
						string? NomeBeneficiario, 
						string? Cidade, 
						ViewAeroVendasParameters viewAeroVendasParameters, 
						bool trackChanges)
	{
		var viewAeroVendasWithMetaData = await _repository.viewAeroVendas.GetViewAeroVendasByAsync(
								Contrato, CodigoBeneficiario, NomeBeneficiario, Cidade, viewAeroVendasParameters, trackChanges);

		if (viewAeroVendasWithMetaData is null)
			throw new ViewAeroVendasNotFoundException();

		var viewAeroVendasDto = _mapper.Map<IEnumerable<ViewAeroVendasDto>>(viewAeroVendasWithMetaData);

		return (viewAeroVendas: viewAeroVendasDto, metaData: viewAeroVendasWithMetaData.MetaData);
	}

	public async Task<(IEnumerable<string> viewAeroVendas, MetaData metaData)> GetViewCidadeAeroVendasAsync(
				ViewAeroVendasParameters viewAeroVendasParameters, 
				bool trackChanges)
	{
		var viewAeroVendasWithMetaData = await _repository.viewAeroVendas.GetViewCidadeAeroVendas(viewAeroVendasParameters, trackChanges);


		return (viewAeroVendas: viewAeroVendasWithMetaData, metaData: viewAeroVendasWithMetaData.MetaData);
	}
}
