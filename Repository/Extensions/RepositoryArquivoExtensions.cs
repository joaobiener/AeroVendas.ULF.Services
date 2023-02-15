using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions;	

public static class RepositoryArquivoExtensions
{


	public static IQueryable<Arquivo> Search(this IQueryable<Arquivo> arquivo, string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
			return arquivo;

		var lowerCaseTerm = searchTerm.Trim().ToLower();

		return arquivo.Where(e => e.CriadoPor.ToLower().Contains(lowerCaseTerm) || 
											  e.Nome.ToLower().Contains(lowerCaseTerm));	
	}

	public static IQueryable<Arquivo> Sort(this IQueryable<Arquivo> arquivo, string orderByQueryString)
	{
		if (string.IsNullOrWhiteSpace(orderByQueryString))
			return arquivo.OrderBy(e => e.CriadoEm);

		var orderQuery = OrderQueryBuilder.CreateOrderQuery<Arquivo>(orderByQueryString);

		if (string.IsNullOrWhiteSpace(orderQuery))
			return arquivo.OrderBy(e => e.CriadoEm);

		return arquivo.OrderBy(orderQuery);
	}
}
