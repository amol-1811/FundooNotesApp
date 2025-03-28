using System;
using System.Collections.Generic;
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


    }
}
