using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IArquivoService
{

	Task PostFileAsync(FileUploadModel fileData);

	Task PostMultiFileAsync(List<FileUploadModel> fileData);

	Task DownloadFileById(Guid Id, bool trackChanges);




}
