using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vidly.Models;
using Vidly.Dtos;
using Microsoft.AspNetCore.Http.Extensions;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private VidlyContext _context;
        private readonly IMapper _mapper;

        public CustomersController(IMapper mapper)
        {
            _context = new VidlyContext();
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers.ToList().Select(_mapper.Map<Customer, CustomerDto>);

            return Ok(customers);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        public IActionResult PostCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.GetDisplayUrl() + "/" + customer.Id), customerDto);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult PutCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                return NotFound();
            }

            _mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
