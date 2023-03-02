using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
	private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IViewAeroVendasRepository> _viewAeroVendasRepository;
    private readonly Lazy<IMessageHTMLRepository> _mensagemHTMLRepository;
	private readonly Lazy<IArquivoRepository> _arquivo;
	private readonly Lazy<IAeroSolicitacaoRepository> _aeroSolicitacaoRepository;
	private readonly Lazy<IAeroEnvioEmailRepository> _aeroEnvioEmailRepository;
	private readonly Lazy<IAeroStatusLoggingRepository> _aeroStatusLoggingRepository;

	public RepositoryManager(RepositoryContext repositoryContext)
	{
		_repositoryContext = repositoryContext;
		_viewAeroVendasRepository = new Lazy<IViewAeroVendasRepository>(() => new ViewAeroVendasRepository(repositoryContext));
        _mensagemHTMLRepository = new Lazy<IMessageHTMLRepository>(() => new MessageHTMLRepository(repositoryContext));
		_arquivo = new Lazy<IArquivoRepository>(() => new ArquivoRepository(repositoryContext));
		_aeroSolicitacaoRepository = new Lazy<IAeroSolicitacaoRepository>(() => new AeroSolicitacaoRepository(repositoryContext));
		_aeroEnvioEmailRepository = new Lazy<IAeroEnvioEmailRepository>(() => new AeroEnvioEmailRepository(repositoryContext));
		_aeroStatusLoggingRepository= new Lazy<IAeroStatusLoggingRepository>(() => new AeroStatusLoggingRepository(repositoryContext));
	}

	public IMessageHTMLRepository mensagemHtml => _mensagemHTMLRepository.Value;
    public IViewAeroVendasRepository viewAeroVendas => _viewAeroVendasRepository.Value;

	public IArquivoRepository arquivo => _arquivo.Value;

	public IAeroSolicitacaoRepository aeroSolicitacao => _aeroSolicitacaoRepository.Value;

	public IAeroEnvioEmailRepository aeroEnvioEmail => _aeroEnvioEmailRepository.Value;
	public IAeroStatusLoggingRepository aeroStatus => _aeroStatusLoggingRepository.Value;

	public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
 

}
