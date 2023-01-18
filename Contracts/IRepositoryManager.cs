namespace Contracts;

public interface IRepositoryManager
{

    IViewAeroVendasRepository viewAeroVendas { get; }

    Task SaveAsync();
}
