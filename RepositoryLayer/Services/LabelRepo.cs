using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;

namespace RepositoryLayer.Services
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundooDBContext context;
        public LabelRepo(FundooDBContext context)
        {
            this.context = context;
        }
        public async Task<LabelEntity> AddLabel(int UserId, string name)
        {
            var label = new LabelEntity
            {
                UserId = UserId,
                LabelName = name
            };
            context.Labels.Add(label);
            await context.SaveChangesAsync();
            return label;
        }
        public async Task<List<LabelEntity>> GetAllLabels()
        {
            var labels = await context.Labels.ToListAsync();
            return labels;
        }

        public async Task<bool> AssignLabelToNote(int noteId, int labelId)
        {
            var label = await context.Labels.FindAsync(labelId);
            var note = await context.Notes.FindAsync(noteId);
            if (label != null && note != null)
            {
                context.NoteLabels.Add(new NoteLabelEntity { NoteId = noteId, LabelId = labelId});
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLabel(int labelId, int noteId)
        {
            var label = await context.NoteLabels.FindAsync(labelId, noteId);
            if (label != null)
            {
                context.NoteLabels.Remove(label);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
