using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnBoard.DataAccess;
using OnBoard.Extensions;
using OnBoard.Models;
using OnBoard.Repositories;
using OnBoard.Resources;
using OnBoard.Utils;

namespace OnBoard.Controllers
{
    [Route("/api/[controller]")]
    public class SaleController : Controller
    {

        private readonly ISaleRepository _saleRepo;
        private readonly IProductRepository _productRepo;
        private readonly IStoreRepository _storeRepo;
        private readonly ICustomerRepository _customerRepo;

        private readonly AppDbContext _dbContext;
        public SaleController(AppDbContext dbContext, ISaleRepository saleRepo, IProductRepository productRepo, IStoreRepository storeRepo, ICustomerRepository customerRepo)
        {
            _saleRepo = saleRepo;
            _productRepo = productRepo;
            _storeRepo = storeRepo;
            _customerRepo = customerRepo;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<object> GetAllAsync(string sortOrder, int pageIndex, int pageSize)
        {
            var sale = await _saleRepo.ListAsync();
            var returnedResource = sale.Select(s => new SaleReturnResource
            {
                SaleId = s.SaleId,
                DateSold = s.DateSold,
                Customer = s.customer.Name,
                Store = s.store.Name,
                Product = s.product.Name
            });

            var sortedResource = Sorting(returnedResource, String.IsNullOrEmpty(sortOrder) ? "default" : sortOrder.ToLower());
            if (pageSize == 0 || pageIndex == 0)
                return sortedResource;

            var paginatedResource = Helper<SaleReturnResource>.Paginating(sortedResource, pageIndex, pageSize);

            return new { data = paginatedResource, count = sale.Count() };
        }

        private IEnumerable<SaleReturnResource> Sorting(IEnumerable<SaleReturnResource> resource, string sortOrder)
        {
            switch (sortOrder)
            {
                case "saleid_desc":
                    return resource.OrderByDescending(c => c.SaleId);
                case "date_desc":
                    return resource.OrderByDescending(c => c.DateSold);
                case "date":
                    return resource.OrderBy(c => c.DateSold);
                case "customer_desc":
                    return resource.OrderByDescending(c => c.Customer);
                case "customer":
                    return resource.OrderBy(c => c.Customer);
                case "store_desc":
                    return resource.OrderByDescending(c => c.Store);
                case "store":
                    return resource.OrderBy(c => c.Store);
                case "product_desc":
                    return resource.OrderByDescending(c => c.Product);
                case "product":
                    return resource.OrderBy(c => c.Product);
                default:
                    return resource.OrderBy(c => c.SaleId);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sale = await _saleRepo.FindByIdAsync(id);
            if (sale == null)
                return NotFound(new List<string> { "Sale Not Found" });

            var returnedResource = Mapper.Map<Sale, SaleDetailResource>(sale);
            return Ok(sale);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Sale sale)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var existingCustomer = await _customerRepo.FindByIdAsync(sale.CustomerId);
            if (existingCustomer == null)
                return NotFound(new List<string> { "Customer Not Found" });

            var existingProduct = await _productRepo.FindByIdAsync(sale.ProductId);
            if (existingProduct == null)
                return NotFound(new List<string> { "Product Not Found" });

            var existingStore = await _storeRepo.FindByIdAsync(sale.StoreId);
            if (existingStore == null)
                return NotFound(new List<string> { "Store Not Found" });
            try
            {
                await _saleRepo.AddAsync(sale);
                await _saleRepo.SaveChangeAsync();
                // var returnedResource = Mapper.Map<Sale, SaleReturnResource>(sale);
                var returnedResource = new SaleReturnResource
                {
                    SaleId = sale.SaleId,
                    DateSold = sale.DateSold,
                    Customer = sale.customer.Name,
                    Store = sale.store.Name,
                    Product = sale.product.Name
                };
                return Ok(returnedResource);
            }
            catch (Exception ex)
            {
                return BadRequest(new List<string> { ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaleUpdateResource resource)
        {
            var existingSale = await _saleRepo.FindByIdAsync(id);
            if (existingSale == null)
                return NotFound(new List<string> { "Sale Not Found" });

            if (resource.CustomerId != null)
            {
                var existingCustomer = await _customerRepo.FindByIdAsync(Int32.Parse(resource.CustomerId));
                if (existingCustomer == null)
                    return NotFound(new List<string> { "Customer Not Found" });
                existingSale.CustomerId = Int32.Parse(resource.CustomerId);
                existingSale.customer = existingCustomer;
            }
            if (resource.StoreId != null)
            {
                var existingStore = await _storeRepo.FindByIdAsync(Int32.Parse(resource.StoreId));
                if (existingStore == null)
                    return NotFound(new List<string> { "Store Not Found" });
                existingSale.StoreId = Int32.Parse(resource.StoreId);
                existingSale.store = existingStore;

            }

            if (resource.ProductId != null)
            {
                var existingProduct = await _productRepo.FindByIdAsync(Int32.Parse(resource.ProductId));
                if (existingProduct == null)
                    return NotFound(new List<string> { "Product Not Found" });
                existingSale.ProductId = Int32.Parse(resource.ProductId);
                existingSale.product = existingProduct;

            }

            try
            {
                _saleRepo.Update(existingSale);
                await _saleRepo.SaveChangeAsync();
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
            var exitstingSale = await _saleRepo.FindByIdAsync(id);
            if (exitstingSale == null)
                return NotFound(new List<string> { "Sale Not Found" });
            try
            {
                _saleRepo.Remove(exitstingSale);
                await _saleRepo.SaveChangeAsync();
                return Ok(new { message = "Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new List<string> { ex.Message });
            }
        }


        // Populate random data for testing .....
        [HttpGet("/api/populate")]
        public async Task<IActionResult> Populate()
        {
            var customers = new List<Customer>
            {
                new Customer{ Name = "Rossi", Address = "6 Roslyn Street, Melbourne"},
                new Customer{ Name = "John", Address = "6 Roslyn Street, Sydney"},
                new Customer{ Name = "Marry", Address = "80 Devonshire Road, Melbourne"},
                new Customer{ Name = "Ray", Address = "82 Devonshire Road, Melbourne"},
                new Customer{ Name = "Justin", Address = "9/5 Camperdown Avenue, Melbourne"},
                new Customer{ Name = "Selena", Address = "6 Roslyn Street, Melbourne"}
            };

            var products = new List<Product>
            {
                new Product{Name = "Macbook", Price = 2800},
                new Product{Name = "Iphone", Price = 1000},
                new Product{Name = "Galaxy S10", Price = 900},
                new Product{Name = "Surface", Price = 2500},
                new Product{Name = "Apply Watch", Price = 600},
                new Product{Name = "Monitor", Price = 800}
            };
            var stores = new List<Store>
            {
                new Store{Name = "Melbourne Store", Address = "Melbourne CBD"},
                new Store{Name = "Sydney Store", Address = "Sydney CBD"}
            };

            try
            {
                await _dbContext.AddRangeAsync(customers);
                await _dbContext.AddRangeAsync(products);
                await _dbContext.AddRangeAsync(stores);

                _dbContext.SaveChanges();

                return Ok(new { message = "Populated" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Please try again later" });

            }
        }

    }
}