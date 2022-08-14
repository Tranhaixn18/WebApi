using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var list = await _categoryRepository.GetAll(pageIndex,pageSize);
                return Ok(new ApiRespone {
                    Success = true,
                    Message = "Get Success",
                    Data=list
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var record = await _categoryRepository.GetById(id);
                if(record == null)
                {
                    return NotFound();
                }
                return Ok(record);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoriesRecord model)
        {
            try
            {
                await _categoryRepository.Add(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,CategoriesRecord model)
        {
            try
            {
                await _categoryRepository.Update(id, model);
                return Ok();
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
                await _categoryRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
