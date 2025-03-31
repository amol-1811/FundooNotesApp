using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepo notesRepo;

        public NotesManager(INotesRepo notesRepo)
        {
            this.notesRepo = notesRepo;
        }

        public NotesEntity AddNotes(int UserId, NotesModel model)
        {
            return notesRepo.AddNotes(UserId, model);
        }

        public List<NotesEntity> GetNotes()
        {
            return notesRepo.GetNotes();
        }
        public List<NotesEntity> GetAllNotesUsingDescAndTitle(string title,  string description)
        {
            return notesRepo.GetAllNotesUsingDescAndTitle(title, description);
        }

        public int CountAllNotes()
        {
            return notesRepo.CountAllNotes();
        }
        public bool DeleteNote(int notesId)
        {
            return notesRepo.DeleteNote(notesId);
        }
        public NotesEntity UpdateNotes(int notesId, int UserId, UpdateModel model)
        {
            return notesRepo.UpdateNotes(notesId, UserId, model);
        }
        public int PinNotes(int noteId, int UserId)
        {
            return notesRepo.PinNotes(noteId, UserId);
        }
        public int ArchiveNote(int noteId, int UserId)
        {
            return notesRepo.ArchiveNote(noteId, UserId);
        }
        public int TrashNotes(int noteId, int UserId)
        {
            return notesRepo.TrashNotes(noteId, UserId);
        }
        public int RestoreFromTrash(int noteId, int UserId)
        {
            return notesRepo.RestoreFromTrash(noteId, UserId);
        }
        public bool AddColor(int noteId, string Colour, int UserId)
        {
            return notesRepo.AddColor(noteId, Colour, UserId);
        }
        public bool AddReminder(int noteId, DateTime reminder, int UserId)
        {
            return notesRepo.AddReminder(noteId, reminder, UserId);
        }
    }
}
