using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TodoAPI.Models
{
    public partial class TodoContext : DbContext
    {
        public TodoContext()
        {
        }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbTodo> TbTodo { get; set; }
        public virtual DbSet<TbUser> TbUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TbTodo>(entity =>
            {
                entity.ToTable("tbTodo");

                entity.Property(e => e.Title).HasMaxLength(10);
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.ToTable("tbUser");
            });
        }
    }
}
