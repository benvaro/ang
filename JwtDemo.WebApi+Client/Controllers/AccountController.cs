using JwtDemo.DataAccess;
using JwtDemo.DataAccess.Entities;
using JwtDemo.Domain.Abstraction;
using JwtDemo.DTO;
using JwtDemo.DTO.Results;
using JwtDemo.WebApi_Client.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtDemo.WebApi_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTTokenService _jwtTokenService;

        public AccountController(
            ApplicationContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJWTTokenService jwtTokenService
            )
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ResultDTO> Register([FromBody] UserRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ErrorResultDTO
                {
                    StatusCode = 401,
                    Message = "Error",
                    Errors = CustomValidator.GetErrorByModel(ModelState)
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

            var identityResult = _userManager.CreateAsync(user, model.Password).Result;

            if (!identityResult.Succeeded)
                return new ErrorResultDTO
                {
                    StatusCode = 500,
                    Message = "Registration Error",
                    Errors = CustomValidator.GetErrorByModel(ModelState)
                };
            var result = await _userManager.AddToRoleAsync(user, "User");
            _context.UserInfos.Add(userInfo);
            await _context.SaveChangesAsync();
            return new ResultDTO
            {
                StatusCode = 200,
                Message = "OK"
            };
        }

        [HttpPost("login")]
        public async Task<ResultDTO> Login([FromBody] UserLoginDTO model)
        {
            if(!ModelState.IsValid)
            {
                return new ErrorResultDTO
                {
                    StatusCode = 401,
                    Message = "Login Error",
                    Errors = CustomValidator.GetErrorByModel(ModelState)
                };
            }

            // Перевірка на успіх логіну та паролю
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if(!result.Succeeded)
            {
                return new ErrorResultDTO {
                    StatusCode = 402,
                    Message = "Login failed",
                    Errors = new System.Collections.Generic.List<string>
                    {
                        "Login or password error"
                    }
                };
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                // вхід
                await _signInManager.SignInAsync(user, false);

                return new SuccessResultDTO
                {
                    StatusCode = 200,
                    Message = "Ok",
                    Token = _jwtTokenService.CreateToken(user)
                };
            }
        }
    }
}

// Result: statusCode, Message
// ResultError: List<string> Error
// SuccessResult: Token