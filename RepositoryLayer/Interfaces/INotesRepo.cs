using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRepo
    {
        public NotesEntity AddNotes(int UserId, NotesModel model);
        public List<NotesEntity> GetNotes();
        public List<NotesEntity> GetAllNotesUsingDescAndTitle(string title, string description);
        public int CountAllNotes();
        public bool DeleteNote(int notesId);
        public NotesEntity UpdateNotes(int notesId, int UserId, UpdateModel model);
        public int PinNotes(int noteId, int UserId);
        public int ArchiveNote(int noteId, int UserId);
        public int TrashNotes(int noteId, int UserId);
        public int RestoreFromTrash(int noteId, int UserId);
        public bool AddColor(int noteId, string Colour, int UserId);
        public bool AddReminder(int noteId, DateTime reminder, int UserId);
        public bool AddImage(int noteId, int UserId, IFormFile Image);
        public int AddCollaborator(int noteId, string Email, int UserId);
        public List<CollaboratorEntity> GetCollaborators(int noteId);
        public bool RemoveCollaborator(int noteId, string Email);
    }
}
