using System;
using System.Collections.Generic;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;
        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }

        [HttpPost]
        [Route("AddNotes")]

        public IActionResult AddNotes(NotesModel model)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);

                NotesEntity result = notesManager.AddNotes(UserId, model);
                if (result != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note Added Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Add Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetNotes")]

        public IActionResult GetNotes()
        {
            try
            {
                List<NotesEntity> getData = notesManager.GetNotes();
                if (getData != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Notes Retrieved Successfully", AllData = getData });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Retrieve Notes" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
