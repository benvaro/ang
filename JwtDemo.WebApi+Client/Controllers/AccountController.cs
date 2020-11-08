using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.DataAccess;
using JwtDemo.DataAccess.Entities;
using JwtDemo.Domain.Abstraction;
using JwtDemo.DTO;
using JwtDemo.DTO.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.WebApi_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IJWTTokenService _jwtTokenService;

        public AccountController(
            ApplicationContext context,
        UserManager<User> userManager,
        IJWTTokenService jwtTokenService
            )
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<ResultDTO> Register([FromBody] UserRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ResultDTO
                {
                    StatusCode = 403,
                    Message = "Error",
                    //Errors = Todo..
                };
            }
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.Phone
            };

            var userInfo = new UserInfo
            {
                Address = model.Address,
                FullName = model.FullName,
                Id = user.Id
            };

            var identityResult = await _userManager.CreateAsync(user, model.Password);

            if (!identityResult.Succeeded)
                return new ResultDTO
                {
                    StatusCode = 500,
                    Message = "Registration Error",
                    //Errors = Todo..
                };
            //    var result = await _userManager.AddToRoleAsync(user, "User");
            _context.UserInfos.Add(userInfo);
            await _context.SaveChangesAsync();
            return new ResultDTO
            {
                StatusCode = 200,
                Message = "OK"
            };
        }
    }
}

// Result: statusCode, Message
// ResultError: List<string> Error
// SuccessResult: Token