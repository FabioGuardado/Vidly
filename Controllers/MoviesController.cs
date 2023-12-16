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

        [Route("Movies/New")]
        public IActionResult New()
        {

            var genres = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel { Genres = genres };

            ViewBag.Action = "Create Movie";

            return View("MoviesForm", viewModel);
        }

        [Route("Movies/Edit/{Id}")]
        public IActionResult Edit(int Id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);

            if (movie == null)
            {
                return NotFound();
            }

            var viewModel = new MovieFormViewModel(movie) { Genres = _context.Genre.ToList() };

            ViewBag.Action = "Edit Movie";

            return View("MoviesForm", viewModel);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie) { Genres = _context.Genre.ToList() };
                return View("MoviesForm", viewModel);
            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            } else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Genre = movie.Genre;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
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
