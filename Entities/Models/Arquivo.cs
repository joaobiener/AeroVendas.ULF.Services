using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Metrics;

namespace Entities.Models;
//-- Create table
//create table UNIMEDLF.MENSAGEM_FILES
//(
//  arquivo_id RAW(32) default SYS_GUID() not null,
//  nome NVARCHAR2(150) not null,
//  Tipo_arquivo NVARCHAR2(100),
//  arquivo BLOB not null,
//  criado DATE default SYSDATE not null,
//  criado_por NVARCHAR2(50)
//)
//tablespace INFOMED_DADOS
//  pctfree 10
//  initrans 1
//  maxtrans 255;
//-- Create/Recreate primary, unique and foreign key constraints
//alter table UNIMEDLF.MENSAGEM_FILES
//  add constraint PK_ARQUIVOS primary key(ARQUIVO_ID)
//  using index 
//  tablespace INFOMED_DADOS
//  pctfree 10
//  initrans 2
//  maxtrans 255;

[Table("MENSAGEM_FILES", Schema = "UNIMEDLF")]
public class Arquivo
{
	[Column("ARQUIVO_ID")]
	[Key]
	public Guid Id { get; set; }
	[Column("NOME")]
	public string? Nome { get; set; }
	[Column("TIPO_ARQUIVO")]
	public string? Tipo { get; set; }
	[Column("ARQUIVO", TypeName = "BLOB")]
	public byte[] DataFiles { get; set; }

	[Column("CRIADO")]
	public DateTime CriadoEm { get; set; }
	[Column("CRIADO_POR")]
	public string? CriadoPor { get; set; }
}
