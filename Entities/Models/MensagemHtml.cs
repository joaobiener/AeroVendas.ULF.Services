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
	}
}
