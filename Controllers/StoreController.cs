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
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepo;
        public StoreController(IStoreRepository storeRepo)
        {
            _storeRepo = storeRepo;
        }

        [HttpGet]
        public async Task<object> GetAllAsync(string sortOrder, int pageIndex, int pageSize)
        {
            var store = await _storeRepo.ListAsync();
            var returnedResource = Mapper.Map<IEnumerable<Store>, IEnumerable<StoreReturnResource>>(store);

            var sortedResource = Sorting(returnedResource, String.IsNullOrEmpty(sortOrder) ? "default" : sortOrder.ToLower());
            if (pageSize == 0 || pageIndex == 0)
                return sortedResource;

            var paginatedResource = Helper<StoreReturnResource>.Paginating(sortedResource, pageIndex, pageSize);

            return new { data = paginatedResource, count = store.Count() };
        }


        private IEnumerable<StoreReturnResource> Sorting(IEnumerable<StoreReturnResource> resource, string sortOrder)
        {
            switch (sortOrder)
            {
                case "storeid_desc":
                    return resource.OrderByDescending(c => c.StoreId);
                case "name_desc":
                    return resource.OrderByDescending(c => c.Name);
                case "name":
                    return resource.OrderBy(c => c.Name);
                case "address_desc":
                    return resource.OrderByDescending(c => c.Address);
                case "address":
                    return resource.OrderBy(c => c.Address);
                default:
                    return resource.OrderBy(c => c.StoreId);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var store = await _storeRepo.FindByIdAsync(id);
            if (store == null)
                return NotFound();

            var returnedResource = Mapper.Map<Store, StoreReturnResource>(store);

            return Ok(returnedResource);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Store store)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            try
            {
                await _storeRepo.AddAsync(store);
                await _storeRepo.SaveChangeAsync();
                var returnedResource = Mapper.Map<Store, StoreReturnResource>(store);

                return Ok(returnedResource);
            }
            catch (Exception ex)
            {
                return BadRequest(new List<string> { ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] StoreUpdateResource resource)
        {
            var existingStore = await _storeRepo.FindByIdAsync(id);
            if (existingStore == null)
                return NotFound();
            var updatedStore = UpdateExistingResource(existingStore, resource);
            try
            {
                _storeRepo.Update(updatedStore);
                await _storeRepo.SaveChangeAsync();
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
            var exitstingStore = await _storeRepo.FindByIdAsync(id);
            if (exitstingStore == null)
                return NotFound();
            try
            {
                _storeRepo.Remove(exitstingStore);
                await _storeRepo.SaveChangeAsync();
                return Ok(new { message = "Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new List<string> { ex.Message });
            }
        }

        // Loop throught and update necessary fields
        private Store UpdateExistingResource(Store existingStore, StoreUpdateResource updatedStore)
        {
            var result = existingStore;
            foreach (var item in updatedStore.ToDictionary())
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