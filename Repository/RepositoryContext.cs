﻿using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;


namespace Repository;

public class RepositoryContext : IdentityDbContext<User>
{
	public RepositoryContext(DbContextOptions options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("UNIMEDLF");  //Usa o squema UNIMEDLF
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new AeroVendasConfiguration());
		modelBuilder.ApplyConfiguration(new RoleConfiguration());
	}

	

	//public DbSet<ViewContratoSemAeroVendas>? ViewLogsAeroVendas { get; set; }

}
