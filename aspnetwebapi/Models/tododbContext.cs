using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aspnetwebapi.Models
{
    public partial class tododbContext : DbContext
    {
        public virtual DbSet<MyRoles> MyRoles { get; set; }
        public virtual DbSet<MyUserRoles> MyUserRoles { get; set; }
        public virtual DbSet<MyUsers> MyUsers { get; set; }
        public virtual DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer(@"");
            }
        }
public tododbContext(DbContextOptions options): base(options)
{
    
}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyRoles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<MyUserRoles>(entity =>
            {
                entity.HasIndex(e => e.MyRoleId);

                entity.HasIndex(e => e.MyUserId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MyRoleId).HasColumnName("MyRoleID");

                entity.Property(e => e.MyUserId).HasColumnName("MyUserID");

                entity.HasOne(d => d.MyRole)
                    .WithMany(p => p.MyUserRoles)
                    .HasForeignKey(d => d.MyRoleId);

                entity.HasOne(d => d.MyUser)
                    .WithMany(p => p.MyUserRoles)
                    .HasForeignKey(d => d.MyUserId);
            });

            modelBuilder.Entity<MyUsers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            // modelBuilder.Entity<TodoItems>(entity =>
            // {
            //     entity.Property(e => e.Id).HasColumnName("ID");
            // });
        }
    }
}
