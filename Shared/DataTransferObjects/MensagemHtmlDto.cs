using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record MensagemHtmlDto(  Guid Id,
								string? Titulo,
								string? TemplateEmailHtml,
								string? CriadoPor,
								DateTime? CriadoEm,
								DateTime? ModificadoEm,
								string? ModificadoPor);

