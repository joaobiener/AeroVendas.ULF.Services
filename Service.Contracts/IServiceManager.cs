namespace Service.Contracts;

public interface IServiceManager
{
	IViewAeroVendasService ViewAeroVendasService { get; }
	IAuthenticationService AuthenticationService { get; }

    IMessageHTMLService MensagemHtmlService { get; }
}
