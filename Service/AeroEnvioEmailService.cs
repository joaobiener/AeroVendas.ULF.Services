using AutoMapper;
using AutoMapper.Configuration;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Service;

internal sealed class AeroEnvioEmailService : IAeroEnvioEmailService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
	private readonly IMapper _mapper;
	private readonly IEmailManager _emailManager;




	public AeroEnvioEmailService(IRepositoryManager repository,
								 ILoggerManager logger,
								 IMapper mapper,
								 IEmailManager emailManager)
	{
		_repository = repository;
		_logger = logger;
		_mapper = mapper;
		_emailManager = emailManager;

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
			.GetAllAeroEnvioEmailAsync(aeroSolicitacaoId, null, viewAeroVendasParameters, trackChanges);

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
		//Caso a alteração seja no UltimoStatus inclui registro na tabela de status
		AeroStatusLoggingForCreationDto aeroStatusDto = new AeroStatusLoggingForCreationDto()
		{
			AeroSolicitacaoEmailId = aeroEnvioEntity.AeroSolicitacaoEmailId,
			AeroEnvioEmailId = aeroEnvioEntity.Id,
			CriadoEm = DateTime.Now,
			Status = aeroEnvioEmailToPatch.UltimoStatus
		};

		var aeroStatusEntity = _mapper.Map<AeroStatusLogging>(aeroStatusDto);

		
		if (aeroEnvioEmailToPatch.UltimoStatus != aeroEnvioEntity.UltimoStatus)
		{
			_repository.aeroStatusLogging.CreateStatusAsync(aeroStatusEntity);

		}

		AeroEnvioEmailForUpdateDto aeroEnvioEmailToPatchModificado = new AeroEnvioEmailForUpdateDto();
		aeroEnvioEmailToPatchModificado = aeroEnvioEmailToPatch;
		aeroEnvioEmailToPatchModificado.ModificadoEm = DateTime.Now;


		_mapper.Map(aeroEnvioEmailToPatchModificado, aeroEnvioEntity);
		
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
	public async Task<int> BulkInsertAeroEnvioEmailForSolicitacaoAsync(AeroSolicitacaoEmailDto aeroSolicitacao)
	{
		ViewAeroVendasParameters parameters = new ViewAeroVendasParameters();
		parameters.PageSize = 30000;
		int qtdRegistos = 0;
		var viewAeroVendasWithMetaData = await _repository.viewAeroVendas.GetViewAeroVendasByAsync(
								null, null, null, aeroSolicitacao.Cidade, parameters, false);

		if (viewAeroVendasWithMetaData is null)
			throw new ViewAeroVendasNotFoundException();

		var listViewAeroVendasDto = _mapper.Map<IEnumerable<ViewAeroVendasDto>>(viewAeroVendasWithMetaData);

		//Lista de Envio de emails para incluir na tabela
		List<AeroEnvioEmail> lstEnvioEmail = new List<AeroEnvioEmail>();
		//Lista de Status de Envio para incluir na tabela de Status Logging
		List<AeroStatusLogging> lstStatus = new List<AeroStatusLogging>();

		var mensagem = await _repository.mensagemHtml.GetMessageAsync(aeroSolicitacao.MensagemHtmlId, trackChanges: false);
		qtdRegistos = lstEnvioEmail.Count;
		foreach (ViewAeroVendasDto itemViewAeroVendas in listViewAeroVendasDto)

		{
			Guid novoId = Guid.NewGuid();
			DateTime dtCriacao = DateTime.Now;
			lstEnvioEmail.Add(new AeroEnvioEmail()
			{

				Id = novoId,
				CodigoContrato = itemViewAeroVendas.Contrato,
				CodigoBeneficiario = itemViewAeroVendas.CodigoBeneficiario,
				NomeBeneficiario = itemViewAeroVendas.NomeBeneficiario,
				PremioAtual = itemViewAeroVendas.PremioAtual,
				EmailBeneficiario = itemViewAeroVendas.EmailBeneficiario,
				Cidade = itemViewAeroVendas.Cidade,
				NumeroDependentes = itemViewAeroVendas.NumeroDependentes,
				UltimoStatus = nameof(Status.PorEnviar),
				AeroSolicitacaoEmailId = aeroSolicitacao.Id,
				CriadoPor = aeroSolicitacao.CriadoPor,
				CriadoEm = dtCriacao,
				MensagemEmailHtml = mensagem.TemplateEmailHtml

			});
			lstStatus.Add(new AeroStatusLogging()
			{
				AeroEnvioEmailId = novoId,
				AeroSolicitacaoEmailId = aeroSolicitacao.Id,
				Status = nameof(Status.PorEnviar),
				CriadoEm = dtCriacao
			});
		}

		//Bulk insere na tabela de envio de emails
		_repository.aeroEnvioEmail.bulkInsertEnvioEmailForSolicitacao(lstEnvioEmail);
		// Bulk insere na tabela de Logs de Status
		_repository.aeroStatusLogging.bulkInsertEnvioEmailLogs(lstStatus);
		return qtdRegistos;

	}

	//Processa o envio dos emails Que estão por enviar
	public async Task<int> ProcessaEnvioEmailForSolicitacaoAsync(AeroSolicitacaoEmail aeroSolicitacao)
	{
		ViewAeroVendasParameters parameters = new ViewAeroVendasParameters();
		//numero maximo de proessamento será de 30,000 registros verificar configuracoes em Appsettings->EmailSettings->QtdEnvio

		parameters.PageSize = _emailManager.EmailService.getQtdEnvio();

		var envioEmailsWithMetaData = await _repository.aeroEnvioEmail.GetAllAeroEnvioEmailAsync(
								aeroSolicitacao.Id,
								nameof(Status.PorEnviar),
								parameters,
								false);

		if (envioEmailsWithMetaData is null)
			throw new ListaAeroEnvioEmaisNotFoundException();

		var listEnvioEmail = _mapper.Map<IEnumerable<AeroEnvioEmail>>(envioEmailsWithMetaData);

		var mensagem = await getMessageHtml(aeroSolicitacao.MensagemHtmlId, false);

		foreach (AeroEnvioEmail itemAeroEnvioEmail in listEnvioEmail)

		{
			if (itemAeroEnvioEmail.EmailBeneficiario != null)
			{
				string aceiteLinkBenef = $"<a href=\"{_emailManager.EmailService.getLinkPaginaAceite()}{itemAeroEnvioEmail.Id}\"  target=\"_blank\" style=\"color:inherit;text-decoration:inherit;\">{itemAeroEnvioEmail.MensagemEmailHtml}</a>";
				//_emailManager.EmailService.Send(itemAeroEnvioEmailDto.EmailBeneficiario, itemAeroEnvioEmailDto.)
				_emailManager.EmailService.Send("joao.pedro@niteroi.unimed.com.br", mensagem.Titulo, aceiteLinkBenef);

				AtualizaStatus(aeroSolicitacao.Id, itemAeroEnvioEmail.Id, nameof(Status.Enviado), aeroSolicitacao.ModificadoPor, aceiteLinkBenef);

			}


		}

		return listEnvioEmail.Count();
	}

	public async Task AtualizaStatus(Guid solicitacaoId, Guid id,  string status, string user,string mensagemHTML)
		
	{

		/*{
    "op": "replace",
    "path": "/ultimostatus",
    "value": "Enviando"
    },
    {
    "op": "replace",
    "path": "/modificadopor",
    "value": "Julio"
    }*/
		JsonPatchDocument<AeroEnvioEmailForUpdateDto> patchDoc = new JsonPatchDocument<AeroEnvioEmailForUpdateDto>();


		patchDoc.Replace(e => e.UltimoStatus, status);
		patchDoc.Replace(e => e.ModificadoPor, user);
		patchDoc.Replace(e => e.ModificadoEm, DateTime.Now);
		patchDoc.Replace(e => e.MensagemEmailHtml, mensagemHTML);

		var result = await GetAeroEnvioForPatchAsync(solicitacaoId, id,
		solicTrackChanges: false, envioTrackChanges: true);

		patchDoc.ApplyTo(result.aeroEnvioEmailToPatch);


		await SaveChangesForPatchAsync(result.aeroEnvioEmailToPatch, result.aeroEnvioEntity);


	}

	private async Task<MensagemHtml> getMessageHtml(Guid? MensagemHtmlId, bool trackChanges)
	{
		var mensagem = await _repository.mensagemHtml.GetMessageAsync(MensagemHtmlId, trackChanges: false);
		return mensagem;
	}
}
