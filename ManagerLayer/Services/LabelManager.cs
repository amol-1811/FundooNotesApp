using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepo labelRepo;
        public LabelManager(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }
        public Task<LabelEntity> AddLabel(int UserId, string name)
        {
            return labelRepo.AddLabel(UserId, name);
        }

        public Task<List<LabelEntity>> GetAllLabels()
        {
            return labelRepo.GetAllLabels();
        }

        public Task<bool> AssignLabelToNote(int noteId, int labelId)
        {
            return labelRepo.AssignLabelToNote(noteId, labelId);
        }

        public Task<bool> DeleteLabel(int labelId, int noteId)
        {
            return labelRepo.DeleteLabel(labelId, noteId);
        }
    }
}
