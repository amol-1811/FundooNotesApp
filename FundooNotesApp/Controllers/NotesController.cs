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
                    return Ok(new ResponseModel<List<NotesEntity>> { Success = true, Message = "Notes Retrieved Successfully", Data = getData });
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

        [HttpGet]
        [Route("GetAllNotesUsingDescAndTitle")]
        public IActionResult GetNotesByTitleAndDis(string Title, string description)
        {
            try
            {
                List<NotesEntity> notes = notesManager.GetAllNotesUsingDescAndTitle(Title, description);

                if (notes == null)
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = true, Message = "get notes successfully" });
                }
                return Ok(notes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("CountAllNotes")]

        public IActionResult CountAllNotes()
        {
            var result = notesManager.CountAllNotes();
            if (result != null)
            {
                return Ok(new ResponseModel<int> { Success = true, Message = "Counting successfully", Data = result });
            }
            return BadRequest(new ResponseModel<int> { Success = false, Message = "Failed to count users" });
        }
    }
}
