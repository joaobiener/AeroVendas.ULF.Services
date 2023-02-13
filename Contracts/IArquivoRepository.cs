using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Contracts;

public interface IArquivoRepository
{
	void CreateArquivo(Arquivo arquivo);

	//void PostMultiFileAsync(List<FileUploadModel> fileData, ArquivoForCreateDTO arqForCreate);

	Task<Arquivo> DownloadFileById(Guid Id, bool trackChanges);

}
