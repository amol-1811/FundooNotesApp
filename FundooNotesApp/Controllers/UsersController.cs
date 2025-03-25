using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager userManager;
        public UsersController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        //http//localhost:44306/api/Users/Reg
        [HttpPost]
        [Route("Reg")]
        public IActionResult Register(RegisterModel model)
        {
            var check = userManager.CheckEmail(model.Email);
            if (check)
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Email already exists" });
            }
            else
            {
                var result = userManager.Register(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Success = true, Message = "User registered successfully", Data = result });
                }
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "User registration failed", Data = result });
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var result = userManager.Login(loginModel);
            if (result != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Login successful", Data = result });
            }
            return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Login failed", Data = result });
        }
    }
}
