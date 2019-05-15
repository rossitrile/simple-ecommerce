using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnBoard.Extensions;
using OnBoard.Models;
using OnBoard.Repositories;
using OnBoard.Resources;
using OnBoard.Utils;

namespace OnBoard.Controllers
{
    [Route("/api/[controller]")]
    public class CustomerController : Controller
    {

        private readonly ICustomerRepository _customerRepo;
        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<object> GetAllAsync(string sortOrder, int pageIndex, int pageSize)
        {
            var customer = await _customerRepo.ListAsync();
            var returnedResource = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerReturnResource>>(customer);

            var sortedResource = Sorting(returnedResource, String.IsNullOrEmpty(sortOrder) ? "default" : sortOrder.ToLower());
            if (pageSize == 0 || pageIndex == 0)
                return sortedResource;

            var paginatedResource = Helper<CustomerReturnResource>.Paginating(sortedResource, pageIndex, pageSize);


            return new { data = paginatedResource, count = customer.Count() };
        }

        private IEnumerable<CustomerReturnResource> Sorting(IEnumerable<CustomerReturnResource> resource, string sortOrder)
        {
            switch (sortOrder)
            {
                case "customerid_desc":
                    return resource.OrderByDescending(c => c.CustomerId);
                case "name_desc":
                    return resource.OrderByDescending(c => c.Name);
                case "name":
                    return resource.OrderBy(c => c.Name);
                case "address_desc":
                    return resource.OrderByDescending(c => c.Address);
                case "address":
                    return resource.OrderBy(c => c.Address);
                default:
                    return resource.OrderBy(c => c.CustomerId);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _customerRepo.FindByIdAsync(id);
            if (customer == null)
                return NotFound();

            var returnedResource = Mapper.Map<Customer, CustomerReturnResource>(customer);

            return Ok(returnedResource);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            try
            {
                await _customerRepo.AddAsync(customer);
                await _customerRepo.SaveChangeAsync();
                var returnedResource = Mapper.Map<Customer, CustomerReturnResource>(customer);

                return Ok(returnedResource);
            }
            catch (Exception ex)
            {
                return BadRequest(new List<string> { ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CustomerUpdateResource resource)
        {
            var exitstingCustomer = await _customerRepo.FindByIdAsync(id);
            if (exitstingCustomer == null)
                return NotFound();
            var updatedCustomer = UpdateExistingResource(exitstingCustomer, resource);
            try
            {
                _customerRepo.Update(updatedCustomer);
                await _customerRepo.SaveChangeAsync();
                return Ok(new { message = "Updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new List<string> { ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exitstingCustomer = await _customerRepo.FindByIdAsync(id);
            if (exitstingCustomer == null)
                return NotFound();
            try
            {
                _customerRepo.Remove(exitstingCustomer);
                await _customerRepo.SaveChangeAsync();
                return Ok(new { message = "Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new List<string> { ex.Message });
            }
        }

        // Loop throught and update necessary fields
        private Customer UpdateExistingResource(Customer existingCustomer, CustomerUpdateResource updatedCustomer)
        {
            var result = existingCustomer;
            foreach (var item in updatedCustomer.ToDictionary())
            {
                var key = item.Key;
                var value = item.Value;
                if (value is bool == false && value != null && value is int == false)
                    result.GetType().GetProperty(key).SetValue(result, value, null);
            }
            return result;
        }


    }
}