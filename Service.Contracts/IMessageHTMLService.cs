using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IMessageHTMLService
{
	Task<IEnumerable<MensagemHtmlDto>> GetAllMessagesAsync(bool trackChanges);
	Task<MensagemHtmlDto> GetMensagemByIdAsync(Guid mensagemId, bool trackChanges);
	Task<MensagemHtmlDto> CreateMensagemAsync(MensagemHtmlForCreationDto mensagem);
	Task<IEnumerable<MensagemHtmlDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	Task<(IEnumerable<MensagemHtmlDto> mensagens, string ids)> CreateMensagemCollectionAsync
		(IEnumerable<MensagemHtmlForCreationDto> mensagemCollection);
	Task DeleteMensagemAsync(Guid mensagemId, bool trackChanges);
	Task UpdateMensagemAsync(Guid mensagemid, MensagemForUpdateDto mensagemForUpdate, bool trackChanges);
}
