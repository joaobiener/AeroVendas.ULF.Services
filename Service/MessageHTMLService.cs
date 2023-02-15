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

internal sealed class MessageHTMLService : IMessageHTMLService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
	private readonly IMapper _mapper;

	public MessageHTMLService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
	{
		_repository = repository;
		_logger = logger;
		_mapper = mapper;
	}

	//public async Task<IEnumerable<MensagemHtmlDto>> GetAllMessagesAsync(bool trackChanges)
	//{
	//    var mensagens = await _repository.mensagemHtml.GetAllMessagesAsync(trackChanges);

	//    var mensagensDto = _mapper.Map<IEnumerable<MensagemHtmlDto>>(mensagens);

	//    return mensagensDto;
	//}

	public async Task<(IEnumerable<MensagemHtmlDto> mensagensHTML, MetaData metaData)> GetAllMessagesAsync(
		ViewAeroVendasParameters viewAeroVendasParameters,bool trackChanges)

	{
		var mensagensWithMetaData = await _repository.mensagemHtml.GetAllMessagesAsync(viewAeroVendasParameters, trackChanges);

		if (mensagensWithMetaData is null)
			throw new MensagemHtmlNotFoundExceptionAll();

		var mensagensDto = _mapper.Map<IEnumerable<MensagemHtmlDto>>(mensagensWithMetaData);

		return (mensagensHTML: mensagensDto, metaData: mensagensWithMetaData.MetaData);

	}


	public async Task<MensagemHtmlDto> GetMensagemByIdAsync(Guid mensagemId, bool trackChanges)
    {
        var mensagem = await _repository.mensagemHtml.GetMessageAsync(mensagemId, trackChanges);
        if (mensagem is null)
            throw new MensagemHtmlNotFoundException(mensagemId);

        var mensagemDto = _mapper.Map<MensagemHtmlDto>(mensagem);
        return mensagemDto;
    }

    public async Task<MensagemHtmlDto> CreateCompanyAsync(MensagemHtmlForCreationDto mensagem)
    {
        var mensagemEntity = _mapper.Map<MensagemHtml>(mensagem);

        _repository.mensagemHtml.CreateMessage(mensagemEntity);
        await _repository.SaveAsync();

        var mensagemToReturn = _mapper.Map<MensagemHtmlDto>(mensagemEntity);

        return mensagemToReturn;
    }
    public async Task<IEnumerable<MensagemHtmlDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var mensagemEntities = await _repository.mensagemHtml.GetByIdsAsync(ids, trackChanges);
        if (ids.Count() != mensagemEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var companiesToReturn = _mapper.Map<IEnumerable<MensagemHtmlDto>>(mensagemEntities);

        return companiesToReturn;
    }


    public async Task<(IEnumerable<MensagemHtmlDto> mensagens, string ids)> CreateMensagemCollectionAsync(IEnumerable<MensagemHtmlForCreationDto> mensagemCollection)
    {
        if (mensagemCollection is null)
            throw new MensagemHtmlCollectionBadRequest();

        var mensagemEntities = _mapper.Map<IEnumerable<MensagemHtml>>(mensagemCollection);
        foreach (var mensagem in mensagemEntities)
        {
            _repository.mensagemHtml.CreateMessage(mensagem);
        }

        await _repository.SaveAsync();

        var mensagemCollectionToReturn = _mapper.Map<IEnumerable<MensagemHtmlDto>>(mensagemEntities);

        var ids = string.Join(",", mensagemCollectionToReturn.Select(c => c.Id));

        return (companies: mensagemCollectionToReturn, ids: ids);
    }

    public async Task DeleteMensagemAsync(Guid mensagemId, bool trackChanges)
    {
        var mensagem = await _repository.mensagemHtml.GetMessageAsync(mensagemId, trackChanges);
        if (mensagem is null)
            throw new MensagemHtmlNotFoundException(mensagemId);

        _repository.mensagemHtml.DeleteMessage(mensagem);
        await _repository.SaveAsync();
    }

    public async Task UpdateMensagemAsync(Guid mensagemid, MensagemForUpdateDto mensagemForUpdate, bool trackChanges)
    {
        var mensagemEntity = await _repository.mensagemHtml.GetMessageAsync(mensagemid, trackChanges);
        if (mensagemEntity is null)
            throw new MensagemHtmlNotFoundException(mensagemid);

        _mapper.Map(mensagemForUpdate, mensagemEntity);
        await _repository.SaveAsync();
    }

  
    public async Task<MensagemHtmlDto> CreateMensagemAsync(MensagemHtmlForCreationDto mensagem)
    {
        var mensagemEntity = _mapper.Map<MensagemHtml>(mensagem);

        _repository.mensagemHtml.CreateMessage(mensagemEntity);

        await _repository.SaveAsync();

        var mensagemToReturn = _mapper.Map<MensagemHtmlDto>(mensagemEntity);

        return mensagemToReturn;
    }
}
