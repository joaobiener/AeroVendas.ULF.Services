using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Contracts;


namespace Service;

public sealed class EmailManager : IEmailManager
{

	private readonly Lazy<IEmailService> _emailService;


	public EmailManager(ILoggerManager logger,
						IOptions<EmailConfiguration> configurationEmail)
	{
	
		_emailService  = new Lazy<IEmailService>(() => 
						new EmailService(logger, configurationEmail));

	}

	
	public IEmailService EmailService => _emailService.Value;
}
