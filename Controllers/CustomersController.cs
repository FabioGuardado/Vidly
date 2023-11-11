using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        [Route("Customers")]
        public IActionResult Index()
        {
            var customers = new List<Customer> { new Customer() { Id = 1, Name = "Fabio Guardado" }, new Customer() { Id = 2, Name = "Paola de Guardado" } };
            var viewModel = new CustomersViewModel() { Customers = customers };
            return View(viewModel);
        }

        [Route("Customers/Details")]
        public IActionResult Details(int Id, string Name)
        {
            if (Id == 0)
            {
                Id = 1;
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = "New Customer";
            }

            var customer = new Customer() { Id = Id, Name = Name };
            var viewModel = new CustomerDetailsViewModel() { Customer = customer };

            return View(viewModel);
        }
    }
}
