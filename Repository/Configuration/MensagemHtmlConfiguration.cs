using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class MensagemHtmlConfiguration : IEntityTypeConfiguration<MensagemHtml>
{
	public void Configure(EntityTypeBuilder<MensagemHtml> builder)
	{
		builder.HasData
		(
			new MensagemHtml
			{
				Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
				Titulo= "1 Titulo",
				TemplateEmailHtml = "<p>Primeiro template HTML</p>"
				
			},
			new MensagemHtml
			{
				Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
				Titulo = "2 Titulo",
				TemplateEmailHtml = "<p>Segundo template HTML</p>"
            }
		);
	}
}
