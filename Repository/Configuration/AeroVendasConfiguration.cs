using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class AeroVendasConfiguration : IEntityTypeConfiguration<ViewContratoSemAeroVendas>
{
	public void Configure(EntityTypeBuilder<ViewContratoSemAeroVendas> builder)
	{
	}
}
