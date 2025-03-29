using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface INotesManager
    {
        public NotesEntity AddNotes(int UserId, NotesModel model);
        public List<NotesEntity> GetNotes();
        public List<NotesEntity> GetAllNotesUsingDescAndTitle(string title, string description);
        public int CountAllNotes();
    }
}
