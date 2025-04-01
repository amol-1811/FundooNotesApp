using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;

        public NotesRepo(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
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
                note.Reminder = model.Reminder;
                note.IsPin = model.IsPin;
                note.IsArchive = model.IsArchive;
                note.IsTrash = model.IsTrash;
                note.Color = model.Color;
                note.Image = model.Image;
                context.SaveChanges();
                return note;
            }
            return null;
        }

        public int PinNotes(int noteId, int UserId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(x => x.NotesId == noteId && x.UserId == UserId);
            if (note != null)
            {
                if (note.IsPin)
                {
                    note.IsPin = false;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    note.IsPin = true;
                    context.SaveChanges();
                    return 2;
                }
            }
            return 3;
        }


        public int ArchiveNote(int noteId, int UserId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(x => x.NotesId == noteId && x.UserId == UserId);
            if (note != null)
            {
                if (note.IsArchive)
                {
                    note.IsArchive = false;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    note.IsArchive = true;
                    context.SaveChanges();
                    return 2;
                }
            }
            return 3;
        }

        public int TrashNotes(int noteId, int UserId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(x => x.NotesId == noteId && x.UserId == UserId);
            if (note != null)
            {
                if (note.IsTrash)
                {
                    note.IsTrash = false;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    note.IsTrash = true;
                    context.SaveChanges();
                    return 2;
                }
            }
            return 3;
        }

        public int RestoreFromTrash(int noteId, int UserId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(x => x.NotesId == noteId && x.UserId == UserId);
            if (note != null)
            {
                if (note.IsTrash)
                {
                    note.IsTrash = false;
                    context.SaveChanges();
                    return 1;
                }
            }
            return 3;
        }
        public bool AddColor(int noteId, string Colour, int UserId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(x => x.NotesId == noteId && x.UserId == UserId);
            if (note != null)
            {
                note.Color = Colour;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddReminder(int noteId, DateTime Reminder, int UserId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(x => x.NotesId == noteId && x.UserId == UserId);
            if (note != null)
            {
                note.Reminder = Reminder;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddImage(int noteId, int UserId, IFormFile Image)

        {
            NotesEntity note = context.Notes.ToList().Find(x => x.NotesId == noteId && x.UserId == UserId);
            if (note != null)
            {
                Account account = new Account(
                    configuration["CloudinarySettings:CloudName"],
                    configuration["CloudinarySettings:ApiKey"],
                    configuration["CloudinarySettings:ApiSecret"]
                );
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(Image.FileName, Image.OpenReadStream())
                };

                var uploadResult = cloudinary.Upload(uploadParams);
                string ImageUrl = uploadResult.Url.ToString();
                note.Image = ImageUrl;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public int AddCollaborator(int noteId, string Email, int UserId)
        {
            NotesEntity note = context.Notes.FirstOrDefault(x => x.NotesId == noteId && x.UserId == UserId);
            if (note != null)
            {
                CollaboratorEntity collaborator = new CollaboratorEntity();
                collaborator.Email = Email;
                collaborator.NotesId = noteId;
                collaborator.UserId = UserId;
                context.Collaborators.Add(collaborator);
                context.SaveChanges();
                return 1;
            }
            return 2;
        }

        public List<CollaboratorEntity> GetCollaborators(int noteId)
        {
            List<CollaboratorEntity> collaborators = context.Collaborators.Where(x => x.NotesId == noteId).ToList();
            return collaborators;
        }

        public bool RemoveCollaborator(int noteId, string Email)
        {
            CollaboratorEntity collaborator = context.Collaborators.FirstOrDefault(x => x.NotesId == noteId && x.Email == Email);
            if (collaborator != null)
            {
                context.Collaborators.Remove(collaborator);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}


