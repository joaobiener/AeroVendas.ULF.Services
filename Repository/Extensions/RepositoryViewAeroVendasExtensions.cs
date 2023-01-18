using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions;	

public static class RepositoryViewAeroVendasExtensions
{


	public static IQueryable<ViewContratoSemAeroVendas> Search(this IQueryable<ViewContratoSemAeroVendas> viewAeroVendas, string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
			return viewAeroVendas;

		var lowerCaseTerm = searchTerm.Trim().ToLower();

		return viewAeroVendas.Where(e => e.Contrato.ToLower().Contains(lowerCaseTerm) || 
											  e.CodigoBeneficiario.ToLower().Contains(lowerCaseTerm) || 
											  e.NomeBeneficiario.ToLower().Contains(lowerCaseTerm) || 
											  e.Cidade.ToLower().Contains(lowerCaseTerm));	
	}

	public static IQueryable<ViewContratoSemAeroVendas> Sort(this IQueryable<ViewContratoSemAeroVendas> viewAeroVendas, string orderByQueryString)
	{
		if (string.IsNullOrWhiteSpace(orderByQueryString))
			return viewAeroVendas.OrderBy(e => e.Contrato);

		var orderQuery = OrderQueryBuilder.CreateOrderQuery<ViewContratoSemAeroVendas>(orderByQueryString);

		if (string.IsNullOrWhiteSpace(orderQuery))
			return viewAeroVendas.OrderBy(e => e.Contrato);

		return viewAeroVendas.OrderBy(orderQuery);
	}
}
