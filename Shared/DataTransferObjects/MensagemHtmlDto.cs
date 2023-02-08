using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record MensagemHtmlDto
{
	public Guid Id { get; init; }
	public string? TemplateEmailHtml { get; init; }

    public string? CriadoPor { get; init; }

    public DateTime? CriadoEm { get; init; }


    public DateTime? ModificadoEm { get; init; }

   
    public string? ModificadoPor { get; init; }
}
