using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entity;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRepo
    {
        public Task<LabelEntity> AddLabel(int UserId, string name);
        // Task<LabelEntity> UpdateLabel(int labelId, LabelEntity label);
        // Task<bool> DeleteLabel(int labelId);
        public Task<List<LabelEntity>> GetAllLabels();
        public Task<bool> AssignLabelToNote(int noteId, int labelId);
        public Task<bool> DeleteLabel(int labelId, int noteId);
        // Task<LabelEntity> GetLabelById(int labelId);
        // Task<List<LabelEntity>> GetLabelsByNoteId(int noteId);
    }
}
