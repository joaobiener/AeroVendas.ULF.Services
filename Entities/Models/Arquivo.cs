using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("MENSAGEM_FILES", Schema = "UNIMEDLF")]
public class Arquivo
{
	[Column("ARQUIVO_ID")]
	[Key]
	public Guid Id { get; set; }
	[Column("NOME")]
	public string? Nome { get; set; }
	[Column("TIPO_ARQUIVO")]
	public FileType? Tipo { get; set; }

	[MaxLength]
	[Column("ARQUIVO")] 
	public byte[] DataFiles { get; set; }
	[Column("CRIADO_POR")]
	public string? CriadoPor { get; set; }
	[Column("CRIADO")]
	public DateTime CriadoEm { get; set; }
}
