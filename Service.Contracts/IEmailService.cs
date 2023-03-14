namespace Service.Contracts;


public interface IEmailService
{
	void Send(string to, string subject, string html, string from = null);

	string getLinkPaginaAceite();

	int getQtdEnvio();

}
