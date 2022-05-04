using System;
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

        public virtual DbSet<FileType> FileTypes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<SikumFile> SikumFiles { get; set; }
        public virtual DbSet<StudyYear> StudyYears { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<User> Users { get; set; }

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

            modelBuilder.Entity<FileType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("filetypes_typeid_primary");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.Message1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Message");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.File)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("message_fileid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("message_userid_foreign");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.RatingId).HasColumnName("RatingID");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.Rating1).HasColumnName("Rating");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rating_fileid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rating_userid_foreign");
            });

            modelBuilder.Entity<SikumFile>(entity =>
            {
                entity.HasKey(e => e.FileId)
                    .HasName("sikumfiles_fileid_primary");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.Headline)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.TextDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("URL");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.YearId).HasColumnName("YearID");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SikumFiles)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sikumfiles_subjectid_foreign");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SikumFiles)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sikumfiles_typeid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SikumFiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sikumfiles_userid_foreign");

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.SikumFiles)
                    .HasForeignKey(d => d.YearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sikumfiles_yearid_foreign");
            });

            modelBuilder.Entity<StudyYear>(entity =>
            {
                entity.HasKey(e => e.YearId)
                    .HasName("studyyear_yearid_primary");

                entity.ToTable("StudyYear");

                entity.Property(e => e.YearId).HasColumnName("YearID");

                entity.Property(e => e.YearName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(255);
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
