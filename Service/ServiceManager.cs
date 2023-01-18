using AutoMapper;
using Contracts;
using Service.Contracts;


namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IViewAeroVendasService> _viewAeroVendasService;


    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
	{
        _viewAeroVendasService = new Lazy<IViewAeroVendasService>(() => new ViewAeroVendasService(repositoryManager, logger, mapper));

    }

    public IViewAeroVendasService ViewAeroVendasService => _viewAeroVendasService.Value;

}
