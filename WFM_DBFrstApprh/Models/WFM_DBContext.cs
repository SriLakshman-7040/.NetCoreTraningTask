using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WFM_DBFrstApprh.Models
{
    public partial class WFM_DBContext : DbContext
    {
        public WFM_DBContext()
        {
        }

        public WFM_DBContext(DbContextOptions<WFM_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillMap> SkillMaps { get; set; }
        public virtual DbSet<Softlock> Softlocks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WFM_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Email).HasMaxLength(70);

                entity.Property(e => e.Experience).HasColumnType("decimal(8, 0)");

                entity.Property(e => e.LockStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Manager)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileId).HasColumnName("Profile_ID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WfmManager)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Wfm_Manager");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.SkillId)
                    .HasColumnType("decimal(8, 0)")
                    .HasColumnName("Skill_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SkillMap>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SkillMap");

                entity.Property(e => e.Skillid).HasColumnType("decimal(8, 0)");

                entity.HasOne(d => d.Employee)
                    .WithMany()
                    .HasForeignKey(d => d.Employeeid)
                    .HasConstraintName("FK_Employeeid");

                entity.HasOne(d => d.Skill)
                    .WithMany()
                    .HasForeignKey(d => d.Skillid)
                    .HasConstraintName("FK_Skillid");
            });

            modelBuilder.Entity<Softlock>(entity =>
            {
                entity.HasKey(e => e.LockId)
                    .HasName("PK__Softlock__E7C1E212A4C79E13");

                entity.ToTable("Softlock");

                entity.Property(e => e.LockId).HasColumnName("LockID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Manager)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerStatus)
                    .HasMaxLength(130)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A Waiting Approval')");

                entity.Property(e => e.ManagerStatusComment)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MgrLastUpdate).HasColumnType("datetime");

                entity.Property(e => e.ReqDate).HasColumnType("datetime");

                entity.Property(e => e.RequestMessage)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WfmRemark)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Wfm_Remark");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__536C85E5C918B5B6");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
