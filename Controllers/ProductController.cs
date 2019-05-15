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
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<object> GetAllAsync(string sortOrder, int pageIndex, int pageSize)
        {
            var product = await _productRepo.ListAsync();
            var returnedResource = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductReturnResource>>(product);

            var sortedResource = Sorting(returnedResource, String.IsNullOrEmpty(sortOrder) ? "default" : sortOrder.ToLower());
            if (pageSize == 0 || pageIndex == 0)
                return sortedResource;

            var paginatedResource = Helper<ProductReturnResource>.Paginating(sortedResource, pageIndex, pageSize);

            return new { data = paginatedResource, count = product.Count() };

        }
        private IEnumerable<ProductReturnResource> Sorting(IEnumerable<ProductReturnResource> resource, string sortOrder)
        {
            switch (sortOrder)
            {
                case "productid_desc":
                    return resource.OrderByDescending(c => c.ProductId);
                case "name_desc":
                    return resource.OrderByDescending(c => c.Name);
                case "name":
                    return resource.OrderBy(c => c.Name);
                case "price_desc":
                    return resource.OrderByDescending(c => c.Price);
                case "price":
                    return resource.OrderBy(c => c.Price);
                default:
                    return resource.OrderBy(c => c.ProductId);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepo.FindByIdAsync(id);
            if (product == null)
                return NotFound();

            var returnedResource = Mapper.Map<Product, ProductReturnResource>(product);

            return Ok(returnedResource);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            try
            {
                await _productRepo.AddAsync(product);
                await _productRepo.SaveChangeAsync();
                var returnedResource = Mapper.Map<Product, ProductReturnResource>(product);

                return Ok(returnedResource);
            }
            catch (Exception ex)
            {
                return BadRequest(new List<string> { ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ProductUpdateResource resource)
        {
            var existingProduct = await _productRepo.FindByIdAsync(id);
            if (existingProduct == null)
                return NotFound();
            var updatedProduct = UpdateExistingResource(existingProduct, resource);
            try
            {
                _productRepo.Update(updatedProduct);
                await _productRepo.SaveChangeAsync();
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
            var exitstingProduct = await _productRepo.FindByIdAsync(id);
            if (exitstingProduct == null)
                return NotFound();
            try
            {
                _productRepo.Remove(exitstingProduct);
                await _productRepo.SaveChangeAsync();
                return Ok(new { message = "Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new List<string> { ex.Message });
            }
        }

        // Loop throught and update necessary fields
        private Product UpdateExistingResource(Product existingProduct, ProductUpdateResource updatedProduct)
        {
            var result = existingProduct;
            foreach (var item in updatedProduct.ToDictionary())
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