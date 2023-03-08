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

internal sealed class AeroSolicitacaoService : IAeroSolicitacaoService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
	private readonly IMapper _mapper;

	public AeroSolicitacaoService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
	{
		_repository = repository;
		_logger = logger;
		_mapper = mapper;
	}

	//Cria a solicitiação para o envio dos emails (Registo Pai)
	public async Task<(IEnumerable<AeroSolicitacaoEmailDto> AeroSolicitacao, MetaData metaData)> GetAllAeroSolicitacaoAsync(
            ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
	{
		var aeroSolicitacaoWithMetaData = await _repository.aeroSolicitacao.GetAllAeroSolicitacaAsync(viewAeroVendasParameters, trackChanges);

		if (aeroSolicitacaoWithMetaData is null)
			throw new AeroSolicitacaoNotFoundExceptionAll();

		var aeroSolicitacaoDto = _mapper.Map<IEnumerable<AeroSolicitacaoEmailDto>>(aeroSolicitacaoWithMetaData);

		return (AeroSolicitacao: aeroSolicitacaoDto, metaData: aeroSolicitacaoWithMetaData.MetaData);
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

	public async Task UpdateAeroSolcitacaoAsync(Guid aeroSolicitacaoId, AeroSolicitacaoEmailForUpdateDto aeroSolicitacoForUpdate, bool trackChanges)
	{
		var aeroSolicitacaoDto = await GetAeroSolicitacaoByIdAsync(aeroSolicitacaoId, trackChanges);
		if (aeroSolicitacaoDto is null)
			throw new AeroSolicitacaoNotFoundException(aeroSolicitacaoId);

		var aeroSolicitacao = _mapper.Map<AeroSolicitacaoEmail>(aeroSolicitacaoDto);

		_mapper.Map(aeroSolicitacoForUpdate, aeroSolicitacao);
		await _repository.SaveAsync();
	}

	public async Task SaveChangesForPatchAsync(AeroSolicitacaoEmailForUpdateDto aeroSolicitacaoToPatch, AeroSolicitacaoEmail aeroSolicitacaooEntity)
	{
		_mapper.Map(aeroSolicitacaoToPatch, aeroSolicitacaooEntity);
		await _repository.SaveAsync();
	}

	public async Task<(AeroSolicitacaoEmailForUpdateDto aeroSolicitacaoToPatch, AeroSolicitacaoEmail aeroSolicitacaoEntity)> GetAeroSolicitacaoForPatchAsync(
				Guid solicitacaoId,  bool solicTrackChanges)
	{


		var aeroSolicitacaoDb = await GetAeroSolicitacaoAndCheckIfItExists(solicitacaoId, solicTrackChanges);

		var aeroSolicitacaoToPatch = _mapper.Map<AeroSolicitacaoEmailForUpdateDto>(aeroSolicitacaoDb);

		return (aeroSolicitacaoToPatch: aeroSolicitacaoToPatch, aeroEnvioEntity: aeroSolicitacaoDb);
	}

	private async Task<AeroSolicitacaoEmail> GetAeroSolicitacaoAndCheckIfItExists
		(Guid aeroSolicitacaoId, bool trackChanges)
	{
		var aeroSolicitacaoDb = await _repository.aeroSolicitacao.GetAeroSolicitacaAsync(aeroSolicitacaoId, trackChanges);
		if (aeroSolicitacaoDb is null)
			throw new AeroSolicitacaoNotFoundException(aeroSolicitacaoId);

		return aeroSolicitacaoDb;
	}
}
