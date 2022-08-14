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
    public class UsersController : ControllerBase
    {
        private IUsersRepository _UsersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            _UsersRepository = usersRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageSize,int pageIndex)
        {
            try
            {
                var list = await _UsersRepository.GetAll(pageIndex, pageSize);
                return Ok(new ApiRespone()
                {
                    Success = true,
                    Message = "get success",
                    Data=list
                });
            }
            catch
            {
                return BadRequest(new ApiRespone()
                {
                    Success = true,
                    Message = "get fail",
                   
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]UserCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _UsersRepository.Create(request);
                if(user== 0)
                {
                    return BadRequest();
                }
                return Ok(new ApiRespone()
                {
                    Success = true,
                    Message = "Create success",
                    Data = request
                });
            }
            catch
            {
                return BadRequest(new ApiRespone()
                {
                    Success = true,
                    Message = "get fail",

                });
            }
        }
    }
}
