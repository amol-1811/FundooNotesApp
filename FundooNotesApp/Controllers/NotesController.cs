using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;
        private readonly IDistributedCache cache;
        private readonly FundooDBContext context;
        public NotesController(INotesManager notesManager, IDistributedCache cache, FundooDBContext context)
        {
            this.notesManager = notesManager;
            this.cache = cache;
            this.context = context;
        }

        [HttpPost]
        [Route("AddNotes")]

        public IActionResult AddNotes(NotesModel model)
        {
            try
            {
                //int UserId = int.Parse(User.FindFirst("UserID").Value);
                int UserId = (int)HttpContext.Session.GetInt32("UserId");

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

        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNote(int notesId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                bool result = notesManager.DeleteNote(notesId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Note Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Failed to Delete Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateNotes")]
        public IActionResult UpdateNotes(int notesId, UpdateModel model)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                NotesEntity result = notesManager.UpdateNotes(notesId, UserId, model);
                if (result != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note Updated Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Update Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("PinNotes")]
        public IActionResult PinNotes(int noteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                int result = notesManager.PinNotes(noteId, UserId);
                if (result != 0)
                {
                    return Ok(new ResponseModel<int> { Success = true, Message = "Note Pinned Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Pin Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("ArchiveNote")]
        public IActionResult ArchiveNote(int noteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                int result = notesManager.ArchiveNote(noteId, UserId);
                if (result != 0)
                {
                    return Ok(new ResponseModel<int> { Success = true, Message = "Note Archived Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Archive Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("TrashNotes")]
        public IActionResult TrashNotes(int noteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                int result = notesManager.TrashNotes(noteId, UserId);
                if (result != 0)
                {
                    return Ok(new ResponseModel<int> { Success = true, Message = "Note Trashed Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Trash Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("RestoreFromTrash")]
        public IActionResult RestoreFromTrash(int noteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                int result = notesManager.RestoreFromTrash(noteId, UserId);
                if (result != 0)
                {
                    return Ok(new ResponseModel<int> { Success = true, Message = "Note Restored Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Restore Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("AddColor")]
        public IActionResult AddColor(int noteId, string Colour)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                bool result = notesManager.AddColor(noteId, Colour, UserId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Color Added Successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Add Color" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut]
        [Route("AddReminder")]
        public IActionResult AddReminder(int noteId, DateTime reminder)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                bool result = notesManager.AddReminder(noteId, reminder, UserId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Reminder Added Successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Add Reminder" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("AddImage")]
        public IActionResult AddImage(int noteId, IFormFile Image)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                bool result = notesManager.AddImage(noteId, UserId, Image);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Image Added Successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Add Image" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(int noteId, string Email)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserID").Value);
                int result = notesManager.AddCollaborator(noteId, Email, UserId);
                if (result != 0)
                {
                    return Ok(new ResponseModel<int> { Success = true, Message = "Collaborator Added Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Add Collaborator" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetCollaborator")]
        public IActionResult GetCollaborator(int noteId)
        {
            try
            {
                List<CollaboratorEntity> result = notesManager.GetCollaborators(noteId);
                if (result != null)
                {
                    return Ok(new ResponseModel<CollaboratorEntity> { Success = true, Message = "Collaborator Retrieved Successfully", FullData = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Retrieve Collaborator" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("RemoveCollaborator")]
        public IActionResult RemoveCollaborator(int noteId, string Email)
        {
            try
            {
                bool result = notesManager.RemoveCollaborator(noteId, Email);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Collaborator Removed Successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Failed to Remove Collaborator" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllNotesUsingRedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "notesList";
            string serializedNotesList;
            var notesList = new List<NotesEntity>();
            byte[] redisNotesList = await cache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = System.Text.Encoding.UTF8.GetString(redisNotesList);
                notesList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                notesList = context.Notes.ToList();
                serializedNotesList = Newtonsoft.Json.JsonConvert.SerializeObject(notesList);
                redisNotesList = System.Text.Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1))
                    .SetSlidingExpiration(TimeSpan.FromHours(1));
                await cache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(notesList);
        }
    }
}
