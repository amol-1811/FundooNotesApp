using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        [HttpPost("AddLabel")]
        public async Task<IActionResult> AddLabel(string name)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                var result = await labelManager.AddLabel(UserId, name);
                if (result != null)
                {
                    return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label added successfully", Data = result });
                }
                return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Failed to add label" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllLabels")]
        public async Task<IActionResult> GetAllLabels()
        {
            try
            {
                var result = await labelManager.GetAllLabels();
                if (result != null)
                {
                    return Ok(new ResponseModel<List<LabelEntity>> { Success = true, Message = "Labels retrieved successfully", Data = result });
                }
                return BadRequest(new ResponseModel<List<LabelEntity>> { Success = false, Message = "Failed to retrieve labels" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("AssignLabelToNote")]
        public async Task<IActionResult> AssignLabelToNote(int noteId, int labelId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                var result = await labelManager.AssignLabelToNote(noteId, labelId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Label assigned to note successfully", Data = result });
                }
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Failed to assign label to note" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteLabel")]
        public async Task<IActionResult> DeleteLabel(int labelId, int noteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                var result = await labelManager.DeleteLabel(labelId, noteId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Label deleted successfully", Data = result });
                }
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Failed to delete label" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

