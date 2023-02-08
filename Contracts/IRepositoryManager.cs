namespace Contracts;

public interface IRepositoryManager
{

    IViewAeroVendasRepository viewAeroVendas { get; }
    IMessageHTMLRepository mensagemHtml { get; }

    Task SaveAsync();
}
