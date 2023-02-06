using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	[Table("ENVIO_MAIL_MARKETING", Schema = "UNIMEDLF")]
	public class EnvioMailMarketing
	{
		[Column("ENVIO_MAIL_MARKETING_ID")]
		[Key]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "A cidade é obrigatória!")]
		[MaxLength(60, ErrorMessage = "Tamanho máximo de 60 caracteres.")]
		[Column("CIDADE")]
		public string? Cidade { get; set; }

		[Required(ErrorMessage = "A Mensagem é obrigatória!")]
		[Column("MENSAGEM_EMAIL_HTML")]
		public string? MensagemEmailHtml { get; set; }

		[Required(ErrorMessage = "Company address is a required field.")]
		[MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters.")]
		[Column("TOTAL_ENVIO")]
		public int? TotalEnviado{ get; set; }

		[Column("DATA_ENVIO")]
		[Timestamp]
		public Byte[]? DataEnvio { get; set; }

		[Required(ErrorMessage = "O Status de Envio é obrigatória!")]
		[MaxLength(60, ErrorMessage = "Tamanho máximo de 60 caracteres.")]
		[Column("STATUS_ENVIO")]
		/* Campo utilizado para verificar o status do Envio
		 * 1. Por Enviar
		 * 2. Enviando
		 * 3. Enviado
		 * 4. Erro de Envio
		 */
		public string? StatusEnvio { get; set; } = "Por Enviar";

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		[Column("CRIADO")]
		public DateTime? CriadoEm { get; set; }

		[Column("CRIADO_POR")]
		public String? CriadoPor { get; set; }
	
		[Column("MODIFICADO")]
		public DateTime? ModificadoEm { get; set; }

		[Column("MODIFICADO_POR")]
		public String? ModificadoPor { get; set; }

		[ForeignKey("ContratoEnviado")]
		public ICollection<ContratoEnviado>? ContratosEnviados { get; set; }
	}
}

