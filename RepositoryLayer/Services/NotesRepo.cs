using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooDBContext context;
        //private readonly IConfiguration configuration;

        public NotesRepo(FundooDBContext context)
        {
            this.context = context;
        }

        public NotesEntity AddNotes(int UserId, NotesModel model)
        {
            NotesEntity note = new NotesEntity();
            note.Title = model.Title;
            note.Description = model.Description;
            note.UserId = UserId;
            this.context.Notes.Add(note);
            context.SaveChanges();
            return note;
        }

        public List<NotesEntity> GetNotes()
        {
            List<NotesEntity> notes = context.Notes.ToList();
            return notes;
        }

        public List<NotesEntity> GetAllNotesUsingDescAndTitle(string title, string description)
        {
            List<NotesEntity> notes = context.Notes.Where(x => x.Title == title && x.Description == description).ToList();
            return notes;
        }

        public int CountAllNotes()
        {
            return context.Notes.Count();
        }
    }
}
