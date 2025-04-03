using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface ILabelManager
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
