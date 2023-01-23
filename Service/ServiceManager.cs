using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
	private readonly Lazy<IViewAeroVendasService> _viewAeroVendasService;
	private readonly Lazy<IAuthenticationService> _authenticationService;

	public ServiceManager(IRepositoryManager repositoryManager,
						ILoggerManager logger,
						IMapper mapper,
						UserManager<User> userManager,
						IConfiguration configuration)
	{
		_viewAeroVendasService = new Lazy<IViewAeroVendasService>(() =>
									new ViewAeroVendasService(repositoryManager, logger, mapper));
		_authenticationService = new Lazy<IAuthenticationService>(() => 
								new AuthenticationService(logger, mapper, userManager,configuration));
	}

	public IViewAeroVendasService ViewAeroVendasService => _viewAeroVendasService.Value;
	public IAuthenticationService AuthenticationService => _authenticationService.Value;
}
