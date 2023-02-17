using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record ArquivoDto { 
	
	public Guid Id { get; set; }
	public string? Nome  { get; set; }
	public string? Tipo { get; set; }
	public byte[] DataFiles { get; set; }
	public string? CriadoPor { get; set; }

	public DateTime? CriadoEm{ get; set; }


}