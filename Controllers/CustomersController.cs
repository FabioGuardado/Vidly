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

        [Route("Customers/Edit/{Id}")]
        public IActionResult Edit(int Id)
        {

            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == Id);

            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel() { 
                Customer = customer,
                MembershipTypes = _context.MembershipType.ToList()
            };

            ViewBag.Action = "Edit Customer";

            return View("CustomerForm", viewModel);
        }

        [Route("Customers/New")]
        public IActionResult New() 
        { 
            var membershipTypes = _context.MembershipType.ToList();
            var viewModel = new CustomerFormViewModel() { Customer = new Customer(), MembershipTypes = membershipTypes };

            ViewBag.Action = "Create Customer";

            return View("CustomerForm", viewModel); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel() {  Customer = customer, MembershipTypes = _context.MembershipType.ToList() };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            } else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}
