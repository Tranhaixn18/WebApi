using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webMobile.ModelView;
using webMobile.Repositories;

namespace webMobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            var result= await _productRepository.GetAll(pageIndex, pageSize);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                var result = await _productRepository.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _productRepository.Add(request);
            if(product == 0)
            {
                return BadRequest();
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm]ProductUpdateRepuest request)
        {
            try
            {
                var result = await _productRepository.Update(id, request);
                if(result == 0)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result= await _productRepository.Delete(id);
                if(result == 0){
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}
