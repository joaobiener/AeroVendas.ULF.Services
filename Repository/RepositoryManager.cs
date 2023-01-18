using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
	private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IViewAeroVendasRepository> _viewAeroVendasRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
	{
		_repositoryContext = repositoryContext;
		_viewAeroVendasRepository = new Lazy<IViewAeroVendasRepository>(() => new ViewAeroVendasRepository(repositoryContext));
        ;
	}


    public IViewAeroVendasRepository viewAeroVendas => _viewAeroVendasRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
 
}
