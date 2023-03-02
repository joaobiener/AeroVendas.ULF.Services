using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions;	

public static class RepositoryAeroEnvioEmailExtensions
{


	public static IQueryable<AeroEnvioEmail> Search(this IQueryable<AeroEnvioEmail> aeroEnvioEmail, string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
			return aeroEnvioEmail;

		var lowerCaseTerm = searchTerm.Trim().ToLower();


		return aeroEnvioEmail.Where(e => e.Cidade.ToLower().Contains(lowerCaseTerm) || 
										  e.UltimoStatus.ToLower().Contains(lowerCaseTerm)
										  || e.Id != null);	
	}

	public static IQueryable<AeroEnvioEmail> Sort(this IQueryable<AeroEnvioEmail> aeroEnvioEmail, string orderByQueryString)
	{
		if (string.IsNullOrWhiteSpace(orderByQueryString))
			return aeroEnvioEmail.OrderByDescending(e => e.CriadoEm);

		var orderQuery = OrderQueryBuilder.CreateOrderQuery<AeroEnvioEmail>(orderByQueryString);

		if (string.IsNullOrWhiteSpace(orderQuery))
			return aeroEnvioEmail.OrderByDescending(e => e.Id);

		return aeroEnvioEmail.OrderBy(orderQuery);
	}
}
