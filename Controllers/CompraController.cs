using Microsoft.AspNetCore.Mvc;
using accountmetrics.Repositories.Interfaces;

namespace accountmetrics.Controllers
{
    public class CompraController : Controller
    {
        
        private readonly ICompraRepository _compraRepository;

        public CompraController(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;

        }

        public ActionResult List()
        {
            var compras = _compraRepository.Compras;
            return View(compras);
        }

    }
}
