using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

	[Index(nameof(CodigoContrato), nameof(CodigoBeneficiario),"IX_CONTRATO_BENEFICIARIO")]
	[Table("CONTRATO_ENVIADO", Schema = "UNIMEDLF")]
	public class ContratoEnviado
	{

		[Column("CONTRATO_ENVIADO_ID")]
		[Key]
		public Guid Id { get; set; }
		
		[Column("CODIGO_CONTRATO")]

		public string? CodigoContrato { get; set; }

		[Column("CODIGO_BENEFICIARIO")]
		public string? CodigoBeneficiario { get; set; }

		[Column("MENSAGEM_HTML")]
		public string? MensagemHtml { get; set; }

		[Column("STATUS_ENVIO")]
		/* Campo utilizado para verificar o status do Envio
		 * 1. Por Enviar
		 * 2. Enviando
		 * 3. Enviado
		 * 4. Erro de Envio
		 */
		public string? StatusEnvio { get; set; } = "Por Enviar";

		[Column("RESPOSTA_ENVIO")]
		/* Campo utilizado para verificar o status do Envio
		 * 1. Sem Resposta
		 * 2. Aceite
		 * 3. Não Aceite
		 */
		public string? RespostaEnvio { get; set; } = "Sem Resposta";

		[Column("CRIADO_POR")]
		public string? CriadoPor { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("CRIADO")]
        public DateTime? CriadoEm { get; set; }

        [Column("MODIFICADO")]
		public DateTime? ModificadoEm { get; set; }

		[Column("MODIFICADO_POR")]
		public string? ModificadoPor { get; set; }

		[ForeignKey(nameof(EnvioMailMarketing))]
		public Guid EnvioMailMarketingId { get; set; }
		public EnvioMailMarketing? EnvioMailMarketing { get; set; }
	}
}
