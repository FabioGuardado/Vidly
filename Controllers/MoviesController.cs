using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private VidlyContext _context;

        public MoviesController()
        {
            _context = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer> { new Customer { Name = "Customer 1" }, new Customer { Name = "Customer 2" } };

            var viewModel = new RandomMovieViewModel { Movie = movie, Customers = customers };
            return View(viewModel);
        }

        [Route("movies/released/{year}/{month:regex(^\\d{{2}}$):range(1, 12)}")]
        public IActionResult ByReleaseDate(int year, int month) 
        {
            return Content(year + "/" + month);
        }

        [Route("Movies")]
        public IActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            var viewModel = new MoviesViewModel { Movies = movies };
            return View(viewModel);
        }

        [Route("Movies/Details/{Id}")]
        public IActionResult Details(int Id) 
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
            var viewModel = new MovieDetailsViewModel { Movie = movie };
            return View(viewModel);
        }
    }
}
