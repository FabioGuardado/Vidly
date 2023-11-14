using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private VidlyContext _context;

        public CustomersController()
        {
            _context = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Customers")]
        public IActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            var viewModel = new CustomersViewModel() { Customers = customers };
            return View(viewModel);
        }

        [Route("Customers/Details/{Id}")]
        public IActionResult Details(int Id)
        {

            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == Id);

            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerDetailsViewModel() { Customer = customer };

            return View(viewModel);
        }
    }
}
