using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using Service.Contracts;



namespace Service;

internal sealed class ArquivoService : IArquivoService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
	private readonly IMapper _mapper;

	public ArquivoService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
	{
		_repository = repository;
		_logger = logger;
		_mapper = mapper;
	}


	public async Task PostFileAsync(FileUploadModel fileData)
	{
	
		var fileDetails = new Arquivo()
		{	
			Nome = fileData.FileDetails.FileName,
			Tipo = fileData.FileType,
			CriadoPor = fileData.CriadoPor

		};

		using (var stream = new MemoryStream())
		{
			fileData.FileDetails.CopyTo(stream);
			fileDetails.DataFiles = stream.ToArray();
		}

		var arquivoEntity = _mapper.Map<Arquivo>(fileDetails);
		arquivoEntity.CriadoEm = DateTime.Now;
		_repository.arquivo.CreateArquivo(fileDetails);
		await _repository.SaveAsync();
		
	}


	public async Task PostMultiFileAsync(List<FileUploadModel> fileData)
	{
		
		foreach (FileUploadModel file in fileData)
		{
			var fileDetails = new Arquivo()
			{
				Nome = file.FileDetails.FileName,
				Tipo = file.FileType,
				CriadoPor = file.CriadoPor
			};

			using (var stream = new MemoryStream())
			{
				file.FileDetails.CopyTo(stream);
				fileDetails.DataFiles = stream.ToArray();
			}

			_repository.arquivo.CreateArquivo(fileDetails);
		}
		await _repository.SaveAsync();
		
	}



	public async Task DownloadFileById(Guid Id, bool trackChanges)
	{
		try
		{

			var arquivo = await _repository.arquivo.DownloadFileById(Id, trackChanges);

			if (arquivo is null)
				throw new MensagemHtmlNotFoundException(Id);

			var content = new System.IO.MemoryStream(arquivo.DataFiles);
			var path = Path.Combine(
			   Directory.GetCurrentDirectory(), "FileDownloaded",
			   arquivo.Nome);

			await CopyStream(content, path);
		}
		catch (Exception)
		{
			throw;
		}
	}
	public async Task CopyStream(Stream stream, string downloadPath)
	{
		using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
		{
			await stream.CopyToAsync(fileStream);
		}
	}
}
