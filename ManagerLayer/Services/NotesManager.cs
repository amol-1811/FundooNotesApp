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
    }
}
