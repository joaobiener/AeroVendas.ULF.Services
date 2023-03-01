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

internal sealed class AeroEnvioEmailService : IAeroEnvioEmailService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
	private readonly IMapper _mapper;

	public AeroEnvioEmailService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
	{
		_repository = repository;
		_logger = logger;
		_mapper = mapper;
	}

	//Busca todos Envios de Email 
	public async Task<(IEnumerable<AeroEnvioEmailDto> aeroEnvioEmail, MetaData metaData)> GetAllAeroEnvioEmailAsync(
			Guid aeroSolicitacaoId, 
			ViewAeroVendasParameters viewAeroVendasParameters, 
			bool trackChanges
	)
	{
		await CheckIfSolicitacaoExists(aeroSolicitacaoId, trackChanges);

		var aeroEnvioWithMetaData = await _repository.aeroEnvioEmail
			.GetAllAeroEnvioEmailAsync(aeroSolicitacaoId, viewAeroVendasParameters, trackChanges);

		var aeroEnvioEmailDto = _mapper.Map<IEnumerable<AeroEnvioEmailDto>>(aeroEnvioWithMetaData);

		return (aeroEnvioEmail: aeroEnvioEmailDto, metaData: aeroEnvioWithMetaData.MetaData);
	}


	public async Task<AeroEnvioEmailDto> CreateAeroEnvioEmailForSolicitacaoAsync(Guid aeroSolicitacaoId, 
			AeroEnvioEmailForCreationDto aeroEnvioEmailForCreation, 
			bool trackChanges)
	{
		await CheckIfSolicitacaoExists(aeroSolicitacaoId, trackChanges);

		var aeroEnvioEmailEntity = _mapper.Map<AeroEnvioEmail>(aeroEnvioEmailForCreation);

		_repository.aeroEnvioEmail.CreateEnvioEmailForSolicitacao(aeroSolicitacaoId, aeroEnvioEmailEntity);
		await _repository.SaveAsync();

		var employeeToReturn = _mapper.Map<AeroEnvioEmailDto>(aeroEnvioEmailEntity);

		return employeeToReturn;
	}

	public async Task DeleteAeroEnvioEmailForSolicitacaoAsync(Guid solicitacaoId, Guid id, bool trackChanges)
	{
		throw new NotImplementedException();
	}

	

	public async Task<AeroEnvioEmailDto> GetAeroEnvioEmailAsync(Guid aeroSolicitacaoId, Guid id, bool trackChanges)
	{
		await CheckIfSolicitacaoExists(aeroSolicitacaoId, trackChanges);

		var aeroEnvioEmailDb = await GetAeroEnvioEmailForSolicitacaoAndCheckIfItExists(aeroSolicitacaoId, id, trackChanges);

		var aeroEnvio = _mapper.Map<AeroEnvioEmailDto>(aeroEnvioEmailDb);
		return aeroEnvio;
	}

	public async Task<(AeroEnvioEmailForUpdateDto aeroEnvioEmailToPatch, AeroEnvioEmail aeroEnvioEntity)> GetAeroEnvioForPatchAsync(Guid solicitacaoId, Guid id, bool solicTrackChanges, bool EnvioTrackChanges)
	{
		throw new NotImplementedException();
	}

	

	public async Task SaveChangesForPatchAsync(AeroEnvioEmailForUpdateDto aeroEnvioEmailToPatch, AeroEnvioEmail aeroEnvioEntity)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateAeroEnvioEmailForSolicitacaoAsync(Guid solicitacaoId, Guid id, AeroEnvioEmailForUpdateDto aeroEnvioEmailForUpdate, bool solicTrackChanges, bool EnvioTrackChanges)
	{
		throw new NotImplementedException();
	}


	private async Task CheckIfSolicitacaoExists(Guid aeroSolicitacaoId, bool trackChanges)
	{
		var aeroSolicitacao = await _repository.aeroSolicitacao.GetAeroSolicitacaAsync(aeroSolicitacaoId, trackChanges);
		if (aeroSolicitacao is null)
			throw new AeroSolicitacaoNotFoundException(aeroSolicitacaoId);
	}

	private async Task<AeroEnvioEmail> GetAeroEnvioEmailForSolicitacaoAndCheckIfItExists
		(Guid aeroSolicitacaoId, Guid id, bool trackChanges)
	{
		var aeroEnvioEmailDb = await _repository.aeroEnvioEmail.GetAeroEnvioEmailAsync(aeroSolicitacaoId, id, trackChanges);
		if (aeroEnvioEmailDb is null)
			throw new AeroEnvioEmailNotFoundException(id);

		return aeroEnvioEmailDb;
	}
}
