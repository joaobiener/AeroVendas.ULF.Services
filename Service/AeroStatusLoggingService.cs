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

	public async Task<AeroStatusLoggingDto> GetStatusSolicitacaoByIdAsync(Guid aeroSolicitacaoId, bool trackChanges)
	{
		throw new NotImplementedException();
	}

	public async Task<AeroStatusLoggingDto> GetStatusEnvioEmailByIdAsync(Guid aeroSolicitacaoId, bool trackChanges)
	{
		throw new NotImplementedException();
	}

	public async Task<AeroStatusLoggingDto> CreateStatusAsync(AeroStatusLoggingForCreationDto aeroStatus)
	{
		throw new NotImplementedException();
	}
}
