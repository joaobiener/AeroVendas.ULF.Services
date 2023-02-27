namespace Contracts;

public interface IRepositoryManager
{

    IViewAeroVendasRepository viewAeroVendas { get; }
    IMessageHTMLRepository mensagemHtml { get; }
	IArquivoRepository arquivo { get; }
	IAeroSolicitacaoRepository aeroSolicitacao{ get; }
	Task SaveAsync();
}
