using Bookstore.Data;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class GenresController : Controller
    {
        private readonly BookstoreContext _context;

        public GenresController(BookstoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Genres.ToList());
        }
    }
}
