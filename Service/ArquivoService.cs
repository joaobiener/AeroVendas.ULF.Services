using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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


	public async Task<(IEnumerable<ArquivoDto> arquivos, MetaData metaData)> GetAllArquivosAsync(ViewAeroVendasParameters viewAeroVendasParameters, bool trackChanges)
	{
		var arquivosWithMetaData = await _repository.arquivo.GetAllArquivosAsync(viewAeroVendasParameters, trackChanges);


		if (arquivosWithMetaData is null)
			throw new MensagemHtmlNotFoundExceptionAll();

		var ArquivoDto = _mapper.Map<IEnumerable<ArquivoDto>>(arquivosWithMetaData);

		return (arquivos: ArquivoDto, metaData: arquivosWithMetaData.MetaData);

	}

	public async Task<Arquivo> PostFileAsync(IFormFile fileData)
	{
	
		var fileDetails = new Arquivo()
		{	
			Nome = fileData.FileName,
			Tipo = "",
			//CriadoPor = fileData.CriadoPor

		};

		using (var stream = new MemoryStream())
		{
			fileData.CopyTo(stream);
			fileDetails.DataFiles = stream.ToArray();
		}

		var arquivoEntity = _mapper.Map<Arquivo>(fileDetails);
		arquivoEntity.CriadoEm = DateTime.Now;
		_repository.arquivo.CreateArquivo(fileDetails);
		await _repository.SaveAsync();
		return fileDetails;
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

	public async Task DeleteArquivoAsync(Guid arquivoId, bool trackChanges)
	{
		var arquivo = await _repository.arquivo.GetFileById(arquivoId, trackChanges);
		if (arquivo is null)
			throw new MensagemHtmlNotFoundException(arquivoId);

		_repository.arquivo.DeleteArquivo(arquivo);
		await _repository.SaveAsync();
	}


	public async Task<Arquivo> GetFileById(Guid ArquivoId, bool trackChanges)
	{
		var arquivo = await _repository.arquivo.GetFileById(ArquivoId, trackChanges);

		if (arquivo is null)
			throw new MensagemHtmlNotFoundException(ArquivoId);

		//var ar = _mapper.Map<MensagemHtmlDto>(arquivo);
		return arquivo;
	}

	public async Task<string> DownloadFileById(Guid Id, bool trackChanges)
	{
		try
		{

			var arquivo = await _repository.arquivo.GetFileById(Id, trackChanges);

			if (arquivo is null)
				throw new MensagemHtmlNotFoundException(Id);

			var content = new System.IO.MemoryStream(arquivo.DataFiles);
			var path = Path.Combine(
			   Directory.GetCurrentDirectory(), "StaticFiles", "Images",
			   arquivo.Id.ToString()+arquivo.Nome);

			await CopyStream(content, path);

			return string.Concat("StaticFiles/","Images/", arquivo.Id.ToString() + arquivo.Nome);
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
