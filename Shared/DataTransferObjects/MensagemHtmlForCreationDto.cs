using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record MensagemHtmlForCreationDto 
{
    [Required(ErrorMessage = "A mensagem é obrigatória.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? TemplateEmailHtml { get; init; }

}
