using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System.Xml;


namespace Repository;

public class RepositoryContext : IdentityDbContext<User>
{
	public RepositoryContext(DbContextOptions options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		modelBuilder.Entity<AeroAtendimentoEnvioEmail>()
		  .Property(e => e.NumProtocolo)
		  .HasDefaultValueSql("SEQ_COD_ETAPA.NEXTVAL");

		modelBuilder.HasDefaultSchema("UNIMEDLF");  //Usa o squema UNIMEDLF
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new AeroVendasConfiguration());
		modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new MensagemHtmlConfiguration());
    }

    public DbSet<MensagemHtml>? MensagensHtml { get; set; }
	public DbSet<Arquivo>? Arquivos { get; set; }
	public DbSet<ViewContratoSemAeroVendas>? ViewLogsAeroVendas { get; set; }
	public DbSet<AeroSolicitacaoEmail> AeroSolicitacaoEmail { get; set; }
	public DbSet<AeroEnvioEmail>? AeroEnvioEmail { get; set; }
	public DbSet<AeroStatusLogging>? AeroStatusLogging { get; set; }

	public DbSet<AeroAtendimentoEnvioEmail>? AeroAtendimentoEnvioEmails { get; set; }




}
