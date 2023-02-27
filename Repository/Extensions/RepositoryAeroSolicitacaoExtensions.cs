using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions;	

public static class RepositoryAeroSolicitacaoExtensions
{


	public static IQueryable<AeroSolicitacaoEmail> Search(this IQueryable<AeroSolicitacaoEmail> aeroSolicitacao, string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
			return aeroSolicitacao;

		var lowerCaseTerm = searchTerm.Trim().ToLower();

		return aeroSolicitacao.Where(e => e.Cidade.ToLower().Contains(lowerCaseTerm) || 
										  e.UltimoStatus.ToLower().Contains(lowerCaseTerm));	
	}

	public static IQueryable<AeroSolicitacaoEmail> Sort(this IQueryable<AeroSolicitacaoEmail> aeroSolicitacao, string orderByQueryString)
	{
		if (string.IsNullOrWhiteSpace(orderByQueryString))
			return aeroSolicitacao.OrderByDescending(e => e.CriadoEm);

		var orderQuery = OrderQueryBuilder.CreateOrderQuery<AeroSolicitacaoEmail>(orderByQueryString);

		if (string.IsNullOrWhiteSpace(orderQuery))
			return aeroSolicitacao.OrderByDescending(e => e.Id);

		return aeroSolicitacao.OrderBy(orderQuery);
	}
}
