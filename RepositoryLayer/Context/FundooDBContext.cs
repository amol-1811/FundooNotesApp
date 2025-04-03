using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Context
{
    public class FundooDBContext : DbContext
    {
        public FundooDBContext(DbContextOptions option) : base(option) { }
        
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<NotesEntity> Notes { get; set; }
        public DbSet<CollaboratorEntity> Collaborators { get; set; }
        public DbSet<LabelEntity> Labels { get; set; }
        public DbSet<NoteLabelEntity> NoteLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NoteLabelEntity>()
                .HasKey(nl => new { nl.NoteId, nl.LabelId });

            modelBuilder.Entity<NoteLabelEntity>()
                .HasOne(nl => nl.Notes)
                .WithMany(n => n.NoteLabels)
                .HasForeignKey(nl => nl.NoteId);

            modelBuilder.Entity<NoteLabelEntity>()
                .HasOne(nl => nl.Labels)
                .WithMany(l => l.NoteLabels)
                .HasForeignKey(nl => nl.LabelId);
        }

    }
}
