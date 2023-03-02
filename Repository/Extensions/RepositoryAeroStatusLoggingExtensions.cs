using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions;	

public static class RepositoryAeroStatusLoggingExtensions
{


	public static IQueryable<AeroStatusLogging> Search(this IQueryable<AeroStatusLogging> aeroStatusLogging, string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
			return aeroStatusLogging;

		var lowerCaseTerm = searchTerm.Trim().ToLower();


		return aeroStatusLogging.Where(e => e.Status.ToLower().Contains(lowerCaseTerm) );	
	}

	public static IQueryable<AeroStatusLogging> Sort(this IQueryable<AeroStatusLogging> aeroStatusLogging, string orderByQueryString)
	{
		if (string.IsNullOrWhiteSpace(orderByQueryString))
			return aeroStatusLogging.OrderByDescending(e => e.CriadoEm);

		var orderQuery = OrderQueryBuilder.CreateOrderQuery<AeroEnvioEmail>(orderByQueryString);

		if (string.IsNullOrWhiteSpace(orderQuery))
			return aeroStatusLogging.OrderByDescending(e => e.Id);

		return aeroStatusLogging.OrderBy(orderQuery);
	}
}
