using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailForCreationDto( string? Cidade,
												int? TotalEnviado,
												string? UltimoStatus,
												Guid? MensagemHtmlRefId);

