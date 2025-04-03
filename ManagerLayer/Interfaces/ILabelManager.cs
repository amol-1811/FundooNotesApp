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
        public Task<List<LabelEntity>> GetAllLabels();
        public Task<bool> AssignLabelToNote(int noteId, int labelId);
        public Task<bool> DeleteLabel(int labelId, int noteId);
    }
}
