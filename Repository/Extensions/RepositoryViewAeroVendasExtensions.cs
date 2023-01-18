using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions;	

public static class RepositoryViewAeroVendasExtensions
{


	public static IQueryable<ViewContratoSemAeroVendas> Search(this IQueryable<ViewContratoSemAeroVendas> viewAeroVendass, string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
			return viewAeroVendass;

		var lowerCaseTerm = searchTerm.Trim().ToLower();

		return viewAeroVendass.Where(e => e.Contrato.ToLower().Contains(lowerCaseTerm) || 
											  e.CodigoBeneficiario.ToLower().Contains(lowerCaseTerm) || 
											  e.NomeBeneficiario.ToLower().Contains(lowerCaseTerm) || 
											  e.EmailBeneficiario.ToLower().Contains(lowerCaseTerm));	
	}

	public static IQueryable<ViewContratoSemAeroVendas> Sort(this IQueryable<ViewContratoSemAeroVendas> viewAeroVendass, string orderByQueryString)
	{
		if (string.IsNullOrWhiteSpace(orderByQueryString))
			return viewAeroVendass.OrderBy(e => e.Contrato);

		var orderQuery = OrderQueryBuilder.CreateOrderQuery<ViewContratoSemAeroVendas>(orderByQueryString);

		if (string.IsNullOrWhiteSpace(orderQuery))
			return viewAeroVendass.OrderBy(e => e.Contrato);

		return viewAeroVendass.OrderBy(orderQuery);
	}
}
