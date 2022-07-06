using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentRecords.Models
{
    public partial class StudentContext : DbContext
    {
        public StudentContext()
        {
        }

        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Batch> Batches { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Student;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("batch");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Batch1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("batch");

                entity.Property(e => e.Year)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("year");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Course1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("course");

                entity.Property(e => e.Duration).HasColumnName("duration");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("registration");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BatchId).HasColumnName("batch_id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Nic)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nic");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_registration_batch");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_registration_course");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
