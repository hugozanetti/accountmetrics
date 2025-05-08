using accountmetrics.Models;

namespace accountmetrics.Repositories.Interfaces;

public interface ICompraRepository
{
    IEnumerable<Compra> Compras { get; }
    Compra GetCompraById(int compraId);
}