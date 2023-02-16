using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record ArquivoDto(  Guid Id,
								string? Nome,
								string? Tipo,
								byte[] DataFiles,
								string? CriadoPor,
								DateTime? CriadoEm);


