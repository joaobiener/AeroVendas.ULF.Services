using System.ComponentModel.DataAnnotations;
namespace Shared.DataTransferObjects;

public abstract record AeroStatusLoggingForManipulationDto
{
	[Required(ErrorMessage = "Status é obrigatório.")]
	[MaxLength(100, ErrorMessage = "Status tem um tamanho máximo de 50 caracteres.")]
	public string? Status { get; set; }
	//[Column("STATUS_")]
	///* Campo utilizado para verificar o status do Envio
	// * 1. Por Enviar
	// * 2. Enviando
	// * 3. Enviado
	// * 4. Erro de Envio
	// */

	public DateTime CriadoEm { get; set; }

	public Guid? AeroEnvioEmailId { get; set; }

	public Guid? AeroSolicitacaoEmailId { get; set; }


}
