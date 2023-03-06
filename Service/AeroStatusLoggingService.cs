using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Service;

internal sealed class AeroStatusLoggingService : IAeroStatusLoggingService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
	private readonly IMapper _mapper;

	public AeroStatusLoggingService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
	{
		_repository = repository;
		_logger = logger;
		_mapper = mapper;
	}

	

	public async Task<AeroSolicitacaoEmailDto> GetAeroSolicitacaoByIdAsync(Guid aeroSolicitacaoId, bool trackChanges)
	{
		var aeroSolicitacao = await _repository.aeroSolicitacao.GetAeroSolicitacaAsync(aeroSolicitacaoId, trackChanges);
		if (aeroSolicitacao is null)
			throw new AeroSolicitacaoNotFoundException(aeroSolicitacaoId);

		var aeroSolicitacaoDto = _mapper.Map<AeroSolicitacaoEmailDto>(aeroSolicitacao);
		return aeroSolicitacaoDto;
	}

	public async Task<AeroSolicitacaoEmailDto> CreateAeroSolicitacaoAsync(AeroSolicitacaoEmailForCreationDto aeroSolicitacao)
	{
		var aeroSolicitacaoEntity = _mapper.Map<AeroSolicitacaoEmail>(aeroSolicitacao);

		_repository.aeroSolicitacao.CreateAeroSolicitacao(aeroSolicitacaoEntity);
		await _repository.SaveAsync();

		var aeroSolicitacaoToReturn = _mapper.Map<AeroSolicitacaoEmailDto>(aeroSolicitacaoEntity);

		return aeroSolicitacaoToReturn;
	}

	public async Task DeleteAeroSolicitacaoAsync(Guid aeroSolicitacaoId, bool trackChanges)
	{
		var aeroSolicitacaoDto = await GetAeroSolicitacaoByIdAsync(aeroSolicitacaoId, trackChanges);

		var aeroSolicitacao = _mapper.Map<AeroSolicitacaoEmail>(aeroSolicitacaoDto);
		_repository.aeroSolicitacao.DeleteAeroSolicitacao(aeroSolicitacao);
		await _repository.SaveAsync();
	}

	public async Task<(IEnumerable<AeroStatusLoggingDto> AeroStatus, MetaData metaData)> GetAllStatusAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
	{
		var aeroStatusWithMetaData = await _repository.aeroStatusLogging.GetAllStatusAsync(viewAeroVendasParameters, trackChanges);

		if (aeroStatusWithMetaData is null)
			throw new AeroStatusLoggingNotFoundExceptionAll();

		var aertoStatusDto = _mapper.Map<IEnumerable<AeroStatusLoggingDto>>(aeroStatusWithMetaData);

		return (AeroStatus: aertoStatusDto, metaData: aeroStatusWithMetaData.MetaData);
	}

	public async Task<(IEnumerable<AeroStatusLoggingDto> AeroStatus, MetaData metaData)> GetStatusByIdAsync(
				Guid aeroSolicitacaoId, 
				Guid aeroEnvioEmailId, 
				ViewAeroVendasParameters viewAeroVendasParameters, 
				bool trackChanges)
	{
		var aeroStatusWithMetaData = await _repository.aeroStatusLogging.GetStatusByIdAsync(
			aeroSolicitacaoId,
			aeroEnvioEmailId, 
			viewAeroVendasParameters, 
			trackChanges);

		if (aeroStatusWithMetaData is null)
			throw new ViewAeroVendasNotFoundException();

		var aeroStatusDto = _mapper.Map<IEnumerable<AeroStatusLoggingDto>>(aeroStatusWithMetaData);

		return (AeroStatus: aeroStatusDto, metaData: aeroStatusWithMetaData.MetaData);
	}


	public async Task<AeroStatusLoggingDto> CreateStatusAsync(AeroStatusLoggingForCreationDto aeroStatus)
	{
		var aeroStatusEntity = _mapper.Map<AeroStatusLogging>(aeroStatus);

		_repository.aeroStatusLogging.CreateStatusAsync(aeroStatusEntity);
		await _repository.SaveAsync();

		var aeroStatusToReturn = _mapper.Map<AeroStatusLoggingDto>(aeroStatusEntity);

		return aeroStatusToReturn;
	}

	
}
