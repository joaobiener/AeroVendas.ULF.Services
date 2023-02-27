using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailForUpdateDto( string? Cidade,
												int? TotalEnviado,
												Guid? MensagemHtmlRefId);

