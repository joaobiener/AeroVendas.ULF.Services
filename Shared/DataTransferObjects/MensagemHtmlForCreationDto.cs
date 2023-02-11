using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public  record MensagemHtmlForCreationDto 
{

	[Required(ErrorMessage = "O Título é obrigatória.")]
	[MaxLength(150, ErrorMessage = "Maximum length for the Name is 150 characters.")]
	public string? Titulo { get; set; }

	[Required(ErrorMessage = "A mensagem é obrigatória.")]
    public string? TemplateEmailHtml { get; set; }
    public string? CriadoPor { get; set; }

}
