using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using RepositoryLayer.Migrations;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int LabelId { get; set; }
        public string LabelName { get; set; } = string.Empty;

        public int UserId { get; set; }
        public UserEntity Users { get; set; } = null!;
        public ICollection<NoteLabelEntity> NoteLabels { get; set; } = new List<NoteLabelEntity>();
    }
}
