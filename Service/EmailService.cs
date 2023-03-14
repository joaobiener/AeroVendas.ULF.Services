using Contracts;
using Entities.ConfigurationModels;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Service.Contracts;


namespace Service;

internal sealed class EmailService : IEmailService
{

	private readonly ILoggerManager _logger;
	private readonly IOptions<EmailConfiguration> _configuration;
	private readonly EmailConfiguration _emailConfiguration;


	public EmailService(ILoggerManager logger,
					IOptions<EmailConfiguration> configuration)
	{
		_logger = logger;
		_configuration = configuration;
		_emailConfiguration = _configuration.Value;

	}

	
	public void Send(string to, string subject, string html, string from = null)
	{
		
		var email = new MimeMessage();
		email.From.Add(MailboxAddress.Parse(from ?? _emailConfiguration.EmailFrom));
		email.To.Add(MailboxAddress.Parse(to));
		email.Subject = subject;
		email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = html };

		// send email
		using var smtp = new SmtpClient();
		
		smtp.Connect(_emailConfiguration.SmtpHost, _emailConfiguration.SmtpPort, SecureSocketOptions.StartTls);
		smtp.Authenticate(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPass);
		smtp.Send(email);
		smtp.Disconnect(true);
	}

	public string getLinkPaginaAceite()
	{
		return _emailConfiguration.UrlSitePaginaAceiteEmail;
	}

	public int getQtdEnvio()
	{
		return _emailConfiguration.QtdEnvio;
	}
	
}
