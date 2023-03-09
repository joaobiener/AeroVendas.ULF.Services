using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Diagnostics.Contracts;

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

	

	public async Task<AeroEnvioEmailDto> GetAeroEnvioEmailAsync(Guid aeroSolicitacaoId, Guid id, bool trackChanges)
	{
		await CheckIfSolicitacaoExists(aeroSolicitacaoId, trackChanges);

		var aeroEnvioEmailDb = await GetAeroEnvioEmailForSolicitacaoAndCheckIfItExists(aeroSolicitacaoId, id, trackChanges);

		var aeroEnvio = _mapper.Map<AeroEnvioEmailDto>(aeroEnvioEmailDb);
		return aeroEnvio;
	}

	
	public async Task DeleteAeroEnvioEmailForSolicitacaoAsync(Guid solicitacaoId, Guid id, bool trackChanges)
	{
		await CheckIfSolicitacaoExists(solicitacaoId, trackChanges);

		var aeroEnvioEmailDb = await GetAeroEnvioEmailForSolicitacaoAndCheckIfItExists(solicitacaoId, id, trackChanges);

		_repository.aeroEnvioEmail.DeleteAeroSolicitacao(aeroEnvioEmailDb);
		await _repository.SaveAsync();
	}

	public async Task<(AeroEnvioEmailForUpdateDto aeroEnvioEmailToPatch, AeroEnvioEmail aeroEnvioEntity)> GetAeroEnvioForPatchAsync(
				Guid solicitacaoId, Guid id, bool solicTrackChanges, bool envioTrackChanges)
	{
		await CheckIfSolicitacaoExists(solicitacaoId, solicTrackChanges);

		var aeroEnvioEmailDb = await GetAeroEnvioEmailForSolicitacaoAndCheckIfItExists(solicitacaoId, id, envioTrackChanges);

		var aeroEnvioEmailToPatch = _mapper.Map<AeroEnvioEmailForUpdateDto>(aeroEnvioEmailDb);

		return (aeroEnvioEmailToPatch: aeroEnvioEmailToPatch, aeroEnvioEntity: aeroEnvioEmailDb);
	}

	public async Task SaveChangesForPatchAsync(
		AeroEnvioEmailForUpdateDto aeroEnvioEmailToPatch, 
		AeroEnvioEmail aeroEnvioEntity)
	{
		_mapper.Map(aeroEnvioEmailToPatch, aeroEnvioEntity);
		 await _repository.SaveAsync();
	}



	public async Task UpdateAeroEnvioEmailForSolicitacaoAsync(
		Guid solicitacaoId, Guid id, AeroEnvioEmailForUpdateDto aeroEnvioEmailForUpdate, bool solicTrackChanges, bool envioTrackChanges)
{
		await CheckIfSolicitacaoExists(solicitacaoId, solicTrackChanges);

		var aeroEnvioEmailDb = await GetAeroEnvioEmailForSolicitacaoAndCheckIfItExists(solicitacaoId, id, envioTrackChanges);


		_mapper.Map(aeroEnvioEmailForUpdate, aeroEnvioEmailDb);
		await _repository.SaveAsync();
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

	//Fará a busca em todos os contratos com o nome da cidade em AeroVendas sem contrato para
	//fazer o BulkInsert dos emails a enviar
	public async Task BulkInsertAeroEnvioEmailForSolicitacaoAsync(AeroSolicitacaoEmailDto aeroSolicitacao)
	{
		ViewAeroVendasParameters parameters= new ViewAeroVendasParameters();
		parameters.PageSize = 30000;

		var viewAeroVendasWithMetaData = await _repository.viewAeroVendas.GetViewAeroVendasByAsync(
								null, null, null, aeroSolicitacao.Cidade, parameters,false);

		if (viewAeroVendasWithMetaData is null)
			throw new ViewAeroVendasNotFoundException();

		var listViewAeroVendasDto = _mapper.Map<IEnumerable<ViewAeroVendasDto>>(viewAeroVendasWithMetaData);

		List<AeroEnvioEmail> lstEnvioEmail = new List<AeroEnvioEmail>();

		var mensagem = await _repository.mensagemHtml.GetMessageAsync(aeroSolicitacao.MensagemHtmlId, trackChanges: false);

		foreach (ViewAeroVendasDto itemViewAeroVendas in listViewAeroVendasDto)

		{
			lstEnvioEmail.Add(new AeroEnvioEmail()
			{
				CodigoContrato = itemViewAeroVendas.Contrato,
				CodigoBeneficiario = itemViewAeroVendas.CodigoBeneficiario,
				NomeBeneficiario = itemViewAeroVendas.NomeBeneficiario,
				PremioAtual = itemViewAeroVendas.PremioAtual,
				EmailBeneficiario = itemViewAeroVendas.EmailBeneficiario,
				Cidade = itemViewAeroVendas.Cidade,
				NumeroDependentes = itemViewAeroVendas.NumeroDependentes,
				UltimoStatus = nameof(Status.PorEnviar),
				AeroSolicitacaoEmailId = aeroSolicitacao.Id,
				MensagemEmailHtml = mensagem.TemplateEmailHtml

			});;

		}
		_repository.aeroEnvioEmail.bulkInsertEnvioEmailForSolicitacao(lstEnvioEmail);
	
	}
}
