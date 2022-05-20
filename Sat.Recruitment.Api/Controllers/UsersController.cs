using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            // -- Use Dependency Injection
            _userService = userService;
        }

        [HttpPost]        
        public async Task<ResultDto> CreateUser(UserDto user)
        {
            var result = await _userService.CreateUser(user);
            return result;
        }

        [HttpGet]
        public async Task<IList<UserDto>> GetAllUser()
        {
            var result = await _userService.GetAll();
            return result;
        }
    }
}
