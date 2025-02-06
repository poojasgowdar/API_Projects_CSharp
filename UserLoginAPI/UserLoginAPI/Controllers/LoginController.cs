using Interfaces.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using System.Security.Claims;

namespace UserLoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IService _userService;
        public LoginController(IService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var x = HttpContext.User.FindFirstValue(ClaimTypes.Role);
                var xd = HttpContext.User.FindFirstValue(ClaimTypes.Name);

                var token = _userService.Login(loginDto);
                return Ok(new { Token = token });

            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Access denied. Please verify your credentials." });
            }
        }

        [HttpPost("Create New User")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser([FromBody] CreateUserDto userDto)
        {
            try
            {
                ResponseUserDto user = _userService.Add(userDto);
                return CreatedAtAction(nameof(CreateUser),
                    new { username = user.Username }, user);
                //return Ok();

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = "Failed to create the user. Please check the provided details and try again." });
            }
        }

        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(string username)
        {
            try
            {
                _userService.Delete(username);
                return Ok(new { Message = $"User '{username}' has been deleted successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"User '{username}' not found. Unable to delete." });
            }
        }


        [HttpGet("{username}")]
        [Authorize]
        public IActionResult GetUser(string username)
        {
            try
            {
                var user = _userService.GetUserByUsername(username);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "User not found" });
            }
        }

        //[HttpGet("{id}")]
        //[Authorize]
        // public IActionResult GetUserId(int id)
        // {
        //     try
        //     {
        //     var user = _userService.GetUserById(id);
        //     return Ok(user);
        //     }
        //     catch (KeyNotFoundException)
        //     {
        //     return NotFound(new { Message = "User not found" });
        //     }
        // }




        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            //List<ResponseUserDto> users = (List<ResponseUserDto>)_userService.GetAll();
            var users = _userService.GetAll();
            return Ok(users);
        }

    }
}
