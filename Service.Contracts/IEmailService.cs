using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IArquivoService
{

	Task<(IEnumerable<ArquivoDto> arquivos, MetaData metaData)> GetAllArquivosAsync(
		ViewAeroVendasParameters viewAeroVendasParameters,
		bool trackChanges);
	Task<Arquivo> PostFileAsync(IFormFile fileData);

	Task PostMultiFileAsync(List<FileUploadModel> fileData);

	Task<string> DownloadFileById(Guid Id, bool trackChanges);


	Task<Arquivo> GetFileById(Guid ArquivoId, bool trackChanges);

//	Task GetFileById(Guid Id, bool trackChanges);

	Task DeleteArquivoAsync(Guid arquivoId, bool trackChanges);




}
