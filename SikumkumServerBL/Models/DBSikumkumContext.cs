﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class DBSikumkumContext : DbContext
    {
        public DBSikumkumContext()
        {
        }

        public DBSikumkumContext(DbContextOptions<DBSikumkumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<FileType> FileTypes { get; set; }
        public virtual DbSet<SikumFile> SikumFiles { get; set; }
        public virtual DbSet<StudyYear> StudyYears { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserMessage> UserMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=DBSikumkum;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(e => e.ChatBoxId)
                    .HasName("chats_chatboxid_primary");

                entity.Property(e => e.ChatBoxId).HasColumnName("ChatBoxID");

                entity.Property(e => e.ChatDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ChatTitle)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<FileType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("filetypes_typeid_primary");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SikumFile>(entity =>
            {
                entity.HasKey(e => e.FileId)
                    .HasName("sikumfiles_fileid_primary");

                entity.HasIndex(e => e.TypeId, "sikumfiles_typeid_unique")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "sikumfiles_username_unique")
                    .IsUnique();

                entity.HasIndex(e => e.YearId, "sikumfiles_yearid_unique")
                    .IsUnique();

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.ChatBoxId).HasColumnName("ChatBoxID");

                entity.Property(e => e.Headline)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TextDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("URL");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.YearId).HasColumnName("YearID");

                entity.HasOne(d => d.ChatBox)
                    .WithMany(p => p.SikumFiles)
                    .HasForeignKey(d => d.ChatBoxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sikumfiles_chatboxid_foreign");

                entity.HasOne(d => d.Type)
                    .WithOne(p => p.SikumFile)
                    .HasForeignKey<SikumFile>(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sikumfiles_typeid_foreign");

                entity.HasOne(d => d.Year)
                    .WithOne(p => p.SikumFile)
                    .HasForeignKey<SikumFile>(d => d.YearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sikumfiles_yearid_foreign");
            });

            modelBuilder.Entity<StudyYear>(entity =>
            {
                entity.HasKey(e => e.YearId)
                    .HasName("studyyear_yearid_primary");

                entity.ToTable("StudyYear");

                entity.Property(e => e.YearId)
                    .ValueGeneratedNever()
                    .HasColumnName("YearID");

                entity.Property(e => e.YearName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.Subject1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Subject");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "users_email_unique")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "users_username_unique")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("usermessages_messageid_primary");

                entity.HasIndex(e => e.ChatBoxId, "usermessages_chatboxid_unique")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "usermessages_username_unique")
                    .IsUnique();

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ChatBoxId).HasColumnName("ChatBoxID");

                entity.Property(e => e.TextMessage)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.ChatBox)
                    .WithOne(p => p.UserMessage)
                    .HasForeignKey<UserMessage>(d => d.ChatBoxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usermessages_chatboxid_foreign");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
