using System;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApp.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]

    public class TimeZoneController : ControllerBase
    {
        private readonly ITimeZoneManager timeZoneManager;
        public TimeZoneController(ITimeZoneManager timeZoneManager)
        {
            this.timeZoneManager = timeZoneManager;
        }

        [HttpPost("converttime")]
        public IActionResult ConvertTime(TimeZoneRequestModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.FromCountry) || string.IsNullOrEmpty(model.ToCountry))
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Invalid input" });
            }
            var convertedTime = timeZoneManager.ConvertTime(model);
            return Ok(convertedTime);
        }
    }
}
