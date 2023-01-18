using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System.Reflection.Metadata;

namespace Repository;

public class RepositoryContext : DbContext
{
	public RepositoryContext(DbContextOptions options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AeroVendasConfiguration());

    }

	public DbSet<ViewContratoSemAeroVendas>? ViewLogsAeroVendas { get; set; }

}
