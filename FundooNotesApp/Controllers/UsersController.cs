using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using MassTransit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly IBus bus;
        public UsersController(IUserManager userManager, IBus bus)
        {
            this.userManager = userManager;
            this.bus = bus;
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
                return Ok(new ResponseModel<string> { Success = true, Message = "Login successful", Data = result });
            }
            return BadRequest(new ResponseModel<string> { Success = false, Message = "Login failed", Data = result });
        }

        [HttpPost]
        [Route("ForgotPassword")]
        

        public async Task<IActionResult> ForgotPassowod(string Email)
        {
            try
            {
                if (userManager.CheckEmail(Email))
                {
                    SendingMail sendEmail = new SendingMail();

                    ForgotPasswordModel forgot = userManager.ForgotPasswordModel(Email);
                    sendEmail.SendEmail(forgot.Email, forgot.Token);
                    Uri uri = new Uri("rabbitmq://localhost/FundooNoteSendEmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);

                    await endPoint.Send(forgot);

                    return Ok(new ResponseModel<string> { Success = true, Message = "Mail sent successfully", Data = forgot.ToString() });

                }
                else
                {
                    return Ok(new ResponseModel<string> { Success = false, Message = "Mail failed " });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]

        public ActionResult ResetPassword(ResetPasswordModel reset)
        {
            try
            {
                string Email = User.FindFirst("EmailId").Value;

                if (userManager.ResetPassword(Email, reset))
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Password Changed successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "Password Changed to failed " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var result = userManager.GetAllUsers();
            if (result != null)
            {
                return Ok(new ResponseModel<List<UserEntity>> { Success = true, Message = "Users retrieved successfully", Data = result });
            }
            return BadRequest(new ResponseModel<List<UserEntity>> { Success = false, Message = "Failed to retrieve users", Data = result });
        }

        [HttpGet]
        [Route("GetUserById")]
        public IActionResult GetUserById(int userId)
        {
            var result = userManager.GetUserById(userId);
            if (result != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "User retrieved successfully", Data = result });
            }
            return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Failed to retrieve user", Data = result });
        }

        [HttpGet]
        [Route("GetUserByFirstLetter")]
        public IActionResult GetUserByFirsttLetter(string letter)
        {
            var result = userManager.GetUserByFirstLetter(letter);
            if (result != null)
            {
                return Ok(new ResponseModel<List<UserEntity>> { Success = true, Message = "Users retrieved successfully", Data = result });
            }
            return BadRequest(new ResponseModel<List<UserEntity>> { Success = false, Message = "Failed to retrieve users", Data = result });
        }

        [HttpGet]
        [Route("CountUsers")]
        public IActionResult CountUsers()
        {
            var result = userManager.CountUsers();
            if (result != null)
            {
                return Ok(new ResponseModel<int> { Success = true, Message = "Counting successfully", Data = result });
            }
            return BadRequest(new ResponseModel<int> { Success = false, Message = "Failed to count users"});
        }

        [HttpGet]
        [Route("GetUsersByOrder")]
        public IActionResult GetUsersByOrder(bool ascending)
        {
            var result = userManager.GetUsersByOrder(ascending);
            if (result != null)
            {
                return Ok(new ResponseModel<List<UserEntity>> { Success = true, Message = "Users retrieved successfully", Data = result });
            }
            return BadRequest(new ResponseModel<List<UserEntity>> { Success = false, Message = "Failed to retrieve users", Data = result });
        }

        [HttpGet]
        [Route("GetAverageAge")]
        public IActionResult GetAverageAge()
        {
            var result = userManager.GetAverageAge();
            if (result != null)
            {
                return Ok(new ResponseModel<double> { Success = true, Message = "Average age retrieved successfully", Data = result });
            }
            return BadRequest(new ResponseModel<double> { Success = false, Message = "Failed to retrieve average age", Data = result });
        }

        [HttpGet]
        [Route("GetYoungestAge")]
        public IActionResult GetYoungestAge()
        {
            var result = userManager.GetYoungestAge();
            if (result != null)
            {
                return Ok(new ResponseModel<int> { Success = true, Message = "Youngest age retrieved successfully", Data = result });
            }
            return BadRequest(new ResponseModel<int> { Success = false, Message = "Failed to retrieve youngest age", Data = result });
        }
        [HttpGet]
        [Route("GetOldestAge")]
        public IActionResult GetOldestAge()
        {
            var result = userManager.GetOldestAge();
            if (result != null)
            {
                return Ok(new ResponseModel<int> { Success = true, Message = "Oldest age retrieved successfully", Data = result });
            }
            return BadRequest(new ResponseModel<int> { Success = false, Message = "Failed to retrieve oldest age", Data = result });
        }
    }
}
