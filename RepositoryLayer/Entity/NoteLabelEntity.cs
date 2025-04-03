using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class NoteLabelEntity
    {
        public int NoteId { get; set; }
        public NotesEntity Notes { get; set; } = null!;

        public int LabelId { get; set; }
        public LabelEntity Labels { get; set; } = null!;
    }
}
