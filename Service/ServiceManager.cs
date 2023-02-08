using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Entities.ConfigurationModels;
using Microsoft.Extensions.Options;
using System.ComponentModel.Design;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
	private readonly Lazy<IViewAeroVendasService> _viewAeroVendasService;
	private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<IMessageHTMLService> _mensagemHtmlService;

    public ServiceManager(IRepositoryManager repositoryManager,
						ILoggerManager logger,
						IMapper mapper,
						UserManager<User> userManager,
						IOptions<JwtConfiguration> configuration)
	{
		_viewAeroVendasService = new Lazy<IViewAeroVendasService>(() =>
									new ViewAeroVendasService(repositoryManager, logger, mapper));
		_authenticationService = new Lazy<IAuthenticationService>(() => 
								new AuthenticationService(logger, mapper, userManager,configuration));
        _mensagemHtmlService = new Lazy<IMessageHTMLService>(() =>
                                new MessageHTMLService(repositoryManager, logger, mapper));
    }

	public IViewAeroVendasService ViewAeroVendasService => _viewAeroVendasService.Value;
	public IAuthenticationService AuthenticationService => _authenticationService.Value;

    public IMessageHTMLService MensagemHtmlService => _mensagemHtmlService.Value;
}
