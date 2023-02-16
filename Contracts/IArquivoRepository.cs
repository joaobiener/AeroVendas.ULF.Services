using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Contracts;

public interface IArquivoRepository
{
	Task<PagedList<Arquivo>> GetAllArquivosAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges);
	void CreateArquivo(Arquivo arquivo);




    Task<Arquivo> DownloadFileById(Guid Id, bool trackChanges);




}
