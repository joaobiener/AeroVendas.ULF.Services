using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record AeroStatusLoggingDto
{
	public Guid Id { get; set; }
	
	public string? Status { get; set; }
	//[Column("STATUS_")]
	///* Campo utilizado para verificar o status do Envio
	// * 1. Por Enviar
	// * 2. Enviando
	// * 3. Enviado
	// * 4. Erro de Envio
	// */

	public DateTime CriadoEm { get; set; }

	public Guid AeroEnvioEmailId { get; set; }
	
	public Guid AeroSolicitacaoEmailId { get; set; }

}

