using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
	//Tabela responsável por armazener Mensagens pré-definidas para utilizr
	//no Email Marketing
	[Table("MENSAGEM_HTML", Schema = "UNIMEDLF")]
	public  class MensagemHtml
	{
		[Column("MENSAGEM_HTML_ID")]
		[Key]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "O Título é obrigatório!")]
		[Column("TITULO")]
		public string? Titulo { get; set; }

		
        [Column("TEMPLATE_EMAIL_HTML")]
        public string? TemplateEmailHtml { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("CRIADO")]
        public DateTime CriadoEm { get; set; }

        [Column("CRIADO_POR")]
        public string? CriadoPor { get; set; }

        [Column("MODIFICADO")]
        public DateTime? ModificadoEm { get; set; }

        [Column("MODIFICADO_POR")]
        public string? ModificadoPor { get; set; }

    }
}
