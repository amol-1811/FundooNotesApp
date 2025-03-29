using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRepo
    {
        public NotesEntity AddNotes(int UserId, NotesModel model);
        public List<NotesEntity> GetNotes();
        public List<NotesEntity> GetAllNotesUsingDescAndTitle(string title, string description);
        public int CountAllNotes();
    }
}
