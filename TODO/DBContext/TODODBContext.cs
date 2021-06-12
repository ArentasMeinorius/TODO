using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using TODO.DBSchemas;

namespace TODO.DBContext
{
    public class TODODBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<TodoTask> Tasks { get; set; }
        public DbSet<TodoList> Lists { get; set; }

        public TODODBContext(DbContextOptions<TODODBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entities to tables  
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<TodoTask>().ToTable("Tasks");
            modelBuilder.Entity<TodoList>().ToTable("Lists");

            // Configure Primary Keys  
            modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("PK_Users");
            modelBuilder.Entity<TodoTask>().HasKey(t => t.Id).HasName("PK_Tasks");
            modelBuilder.Entity<TodoList>().HasKey(l => l.Id).HasName("PK_Lists");

            // Configure columns  
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<User>().Property(u => u.UserName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PassWord).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.CreationDateTime).HasColumnType("datetime").IsRequired().HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<User>().Property(u => u.ListId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<TodoTask>().Property(t => t.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<TodoTask>().Property(t => t.Name).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<TodoTask>().Property(t => t.Description).HasColumnType("nvarchar(100)").IsRequired(false);
            modelBuilder.Entity<TodoTask>().Property(t => t.Completed).HasColumnType("tinyint").IsRequired().HasDefaultValue(0);
            modelBuilder.Entity<TodoTask>().Property(t => t.ListId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<TodoList>().Property(l => l.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<TodoList>().Property(l => l.Name).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<TodoList>().Property(l => l.Description).HasColumnType("nvarchar(100)").IsRequired(false);
            modelBuilder.Entity<TodoList>().Property(l => l.UserId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<TodoList>()
                .HasMany(x => x.Tasks)
                .WithOne(x => x.List);
            
            modelBuilder.Entity<User>()
                .HasOne(x => x.List)
                .WithOne(x => x.User)
                .HasForeignKey<TodoList>(x => x.UserId);
        }
    }
}
