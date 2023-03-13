namespace Service.Contracts;

public interface IServiceManager
{
	IViewAeroVendasService ViewAeroVendasService { get; }
	IAuthenticationService AuthenticationService { get; }

    IMessageHTMLService MensagemHtmlService { get; }
	IArquivoService ArquivoService { get; }

	IAeroSolicitacaoService AeroSolicitacaoService{ get; }

	IAeroEnvioEmailService AeroEnvioEmailService  { get; }

	IAeroStatusLoggingService AeroStatusLoggingService { get; }

	IEmailService EmailService { get; }


}
