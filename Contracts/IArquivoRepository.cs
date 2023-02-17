using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Contracts;

public interface IArquivoRepository
{
	Task<PagedList<Arquivo>> GetAllArquivosAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges);
	void CreateArquivo(Arquivo arquivo);

	Task<Arquivo> GetFileById(Guid Id, bool trackChanges);

	public void DeleteArquivo(Arquivo arquivo);


}
