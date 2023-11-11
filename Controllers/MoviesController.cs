using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
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
            var movies = new List<Movie>() { new Movie { Id = 1, Name = "Shrek"}, new Movie { Id = 2, Name = "Avengers" } };
            var viewModel = new MoviesViewModel { Movies = movies };
            return View(viewModel);
        }

        [Route("Movies/Details")]
        public IActionResult Details(int Id, string Name) 
        {
            var movie = new Movie() { Id = Id, Name = Name};
            var viewModel = new MovieDetailsViewModel { Movie = movie };
            return View(viewModel);
        }
    }
}
