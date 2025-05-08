using accountmetrics.Models;
using accountmetrics.Repositories.Interfaces;
using accountmetrics.Context;
using Microsoft.EntityFrameworkCore;


namespace accountmetrics.Repositories;

public class CompraRepository : ICompraRepository
{
    private readonly AppDbContext _context;

    //Cria o contexto DBContext atraves DI (Dependency Injection)
    public CompraRepository (AppDbContext contexto)
    {
        _context = contexto;
    }

    public IEnumerable<Compra> Compras => _context.Compras;

    public Compra GetCompraById (int compraId)
    {
        return _context.Compras.FirstOrDefault(u => u.CompraId == compraId);
        
    }
}

