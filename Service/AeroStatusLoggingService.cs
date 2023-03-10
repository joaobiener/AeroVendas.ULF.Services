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

	

	public async Task<(IEnumerable<AeroStatusLoggingDto> AeroStatus, MetaData metaData)> GetAllStatusAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
	{
		var aeroStatusWithMetaData = await _repository.aeroStatusLogging.GetAllStatusAsync(viewAeroVendasParameters, trackChanges);

		if (aeroStatusWithMetaData is null)
			throw new AeroStatusLoggingNotFoundExceptionAll();

		var aertoStatusDto = _mapper.Map<IEnumerable<AeroStatusLoggingDto>>(aeroStatusWithMetaData);

		return (AeroStatus: aertoStatusDto, metaData: aeroStatusWithMetaData.MetaData);
	}

	public async Task<(IEnumerable<AeroStatusLoggingDto> AeroStatus, MetaData metaData)> GetStatusByIdAsync(
				Guid? aeroSolicitacaoId, 
				Guid? aeroEnvioEmailId, 
				ViewAeroVendasParameters viewAeroVendasParameters, 
				bool trackChanges)
	{
		PagedList<AeroStatusLogging> aeroStatusWithMetaData=null;
		
		if (aeroEnvioEmailId != null) { 
			 aeroStatusWithMetaData = await _repository.aeroStatusLogging.GetStatusByEnvioEmailIdAsync(
				aeroEnvioEmailId, 
				viewAeroVendasParameters, 
				trackChanges);
		}
		else
		{
			aeroStatusWithMetaData = await _repository.aeroStatusLogging.GetStatusBySolicitacaoIdAsync(
			   aeroSolicitacaoId,
			   viewAeroVendasParameters,
			   trackChanges);
		}

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

	public async Task<AeroStatusLoggingDto> GetAeroStatusByIdAsync(Guid aeroStatusId, bool trackChanges)
	{
		var aeroStatus = await _repository.aeroStatusLogging.GetAeroStatusByIdAsync(aeroStatusId, trackChanges);
		if (aeroStatus is null)
			throw new AeroStatusByIdNotFoundException(aeroStatus.Id);

		var aeroStatusDto = _mapper.Map<AeroStatusLoggingDto>(aeroStatus);
		return aeroStatusDto;
	}


}
