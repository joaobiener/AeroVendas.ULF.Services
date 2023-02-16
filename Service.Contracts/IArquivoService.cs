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
	Task<Arquivo> PostFileAsync(FileUploadModel fileData);

	Task PostMultiFileAsync(List<FileUploadModel> fileData);

	Task DownloadFileById(Guid Id, bool trackChanges);




}
