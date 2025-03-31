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

        public bool DeleteNote(int notesId)
        {
            NotesEntity note = context.Notes.Where(x => x.NotesId == notesId).FirstOrDefault();
            if (note != null)
            {
                context.Notes.Remove(note);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public NotesEntity UpdateNotes(int notesId, int UserId, UpdateModel model)
        {
            NotesEntity note = context.Notes.Where(x => x.NotesId == notesId && x.UserId == UserId).FirstOrDefault();
            if (note != null)
            {
                note.Title = model.Title;
                note.Description = model.Description;
                context.SaveChanges();
                return note;
            }
            return null;
        }
    }
}
