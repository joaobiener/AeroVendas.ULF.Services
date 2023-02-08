using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
	private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IViewAeroVendasRepository> _viewAeroVendasRepository;
    private readonly Lazy<IMessageHTMLRepository> _mensagemHTMLRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
	{
		_repositoryContext = repositoryContext;
		_viewAeroVendasRepository = new Lazy<IViewAeroVendasRepository>(() => new ViewAeroVendasRepository(repositoryContext));
        _mensagemHTMLRepository = new Lazy<IMessageHTMLRepository>(() => new MessageHTMLRepository(repositoryContext));
    }

    public IMessageHTMLRepository mensagemHtml => _mensagemHTMLRepository.Value;
    public IViewAeroVendasRepository viewAeroVendas => _viewAeroVendasRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
 

}
