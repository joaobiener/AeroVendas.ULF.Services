using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroSolicitacaoEmailDto(  Guid Id,
								string? Cidade,
								int? TotalEnviado,
								string? UltimoStatus,
								DateTime? CriadoEm,
								Guid? MensagemHtmlRefId);

