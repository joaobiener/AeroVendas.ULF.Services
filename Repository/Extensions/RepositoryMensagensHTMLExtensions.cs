using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions;	

public static class RepositoryMensagensHTMLExtensions
{


	public static IQueryable<MensagemHtml> Search(this IQueryable<MensagemHtml> mensagemsHTML, string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
			return mensagemsHTML;

		var lowerCaseTerm = searchTerm.Trim().ToLower();

		return mensagemsHTML.Where(e => e.CriadoPor.ToLower().Contains(lowerCaseTerm) || 
											  e.TemplateEmailHtml.ToLower().Contains(lowerCaseTerm));	
	}

	public static IQueryable<MensagemHtml> Sort(this IQueryable<MensagemHtml> mensagemsHTML, string orderByQueryString)
	{
		if (string.IsNullOrWhiteSpace(orderByQueryString))
			return mensagemsHTML.OrderBy(e => e.CriadoEm);

		var orderQuery = OrderQueryBuilder.CreateOrderQuery<ViewContratoSemAeroVendas>(orderByQueryString);

		if (string.IsNullOrWhiteSpace(orderQuery))
			return mensagemsHTML.OrderBy(e => e.CriadoEm);

		return mensagemsHTML.OrderBy(orderQuery);
	}
}
